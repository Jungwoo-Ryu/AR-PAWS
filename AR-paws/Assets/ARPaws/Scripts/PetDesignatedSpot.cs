using UnityEngine;

namespace ARPaws.Core
{
    public enum PetSpotType
    {
        Bed,
        Food,
        Play,
        Custom
    }

    /// <summary>
    /// Optional world marker for behaviors such as sleeping, eating, or play.
    /// </summary>
    public class PetDesignatedSpot : MonoBehaviour
    {
        [SerializeField] PetSpotType spotType = PetSpotType.Custom;
        [SerializeField] Transform spotCenter;
        [SerializeField] float randomRadius = 0.2f;

        public PetSpotType SpotType => spotType;

        public Vector3 GetPoint()
        {
            var center = spotCenter != null ? spotCenter.position : transform.position;
            if (randomRadius <= 0f)
                return center;

            var offset2D = Random.insideUnitCircle * randomRadius;
            return new Vector3(center.x + offset2D.x, center.y, center.z + offset2D.y);
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            var center = spotCenter != null ? spotCenter.position : transform.position;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(center, randomRadius);
        }
#endif
    }
}
