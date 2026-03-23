using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ARPaws.AR
{
    /// <summary>
    /// First tap on a detected plane spawns the pet prefab at the hit pose.
    /// Requires ARRaycastManager + ARPlaneManager on the scene (AR Session setup).
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class ARPetsSurfacePlacer : MonoBehaviour
    {
        [SerializeField] GameObject petPrefab;
        [SerializeField] bool allowReposition;
        [SerializeField] float minReselectSeconds = 0.35f;

        ARRaycastManager _raycasts;
        GameObject _spawned;
        float _lastPlaceTime;

        void Awake()
        {
            _raycasts = GetComponent<ARRaycastManager>();
        }

        void Update()
        {
            if (petPrefab == null || _raycasts == null)
                return;

            if (!TryGetPointerDown(out var screenPoint))
                return;

            var hits = new List<ARRaycastHit>();
            if (!_raycasts.Raycast(screenPoint, hits, TrackableType.PlaneWithinPolygon))
                return;

            var pose = hits[0].pose;
            if (_spawned == null || allowReposition)
            {
                if (Time.unscaledTime - _lastPlaceTime < minReselectSeconds)
                    return;
                _lastPlaceTime = Time.unscaledTime;
                if (_spawned != null)
                    Destroy(_spawned);
                _spawned = Instantiate(petPrefab, pose.position, pose.rotation);
            }
        }

        static bool TryGetPointerDown(out Vector2 screenPoint)
        {
            screenPoint = default;
            if (Input.touchCount > 0)
            {
                var t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    screenPoint = t.position;
                    return true;
                }
                return false;
            }

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                screenPoint = Input.mousePosition;
                return true;
            }
#endif
            return false;
        }
    }
}
