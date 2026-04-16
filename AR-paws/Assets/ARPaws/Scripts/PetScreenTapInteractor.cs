using UnityEngine;

namespace ARPaws.Core
{
    /// <summary>
    /// Simple fallback interaction bridge.
    /// Touch or click an object with PetInteractionTarget to invoke Interact().
    /// </summary>
    public class PetScreenTapInteractor : MonoBehaviour
    {
        [SerializeField] Camera interactionCamera;
        [SerializeField] LayerMask hitMask = ~0;
        [SerializeField] float maxDistance = 30f;

        void Awake()
        {
            if (interactionCamera == null)
                interactionCamera = Camera.main;
        }

        void Update()
        {
            if (interactionCamera == null)
                return;

            if (!TryGetPointerDown(out var point))
                return;

            var ray = interactionCamera.ScreenPointToRay(point);
            if (!Physics.Raycast(ray, out var hit, maxDistance, hitMask, QueryTriggerInteraction.Collide))
                return;

            var target = hit.collider.GetComponentInParent<PetInteractionTarget>();
            if (target != null)
                target.Interact();
        }

        static bool TryGetPointerDown(out Vector2 screenPoint)
        {
            screenPoint = default;
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    screenPoint = touch.position;
                    return true;
                }
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
