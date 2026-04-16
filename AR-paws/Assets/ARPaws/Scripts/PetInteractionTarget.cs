using UnityEngine;

namespace ARPaws.Core
{
    public enum PetInteractionAction
    {
        Feed,
        Clean,
        Pet,
        Play
    }

    /// <summary>
    /// Place on interactable world objects (toy/food/brush).
    /// Calling Interact() forwards to the matching need action.
    /// </summary>
    public class PetInteractionTarget : MonoBehaviour
    {
        [SerializeField] PetNeedsController needs;
        [SerializeField] PetInteractionAction action = PetInteractionAction.Pet;
        [SerializeField] bool allowMouseClickInEditor = true;

        public void Interact()
        {
            if (needs == null)
                return;

            switch (action)
            {
                case PetInteractionAction.Feed:
                    needs.Feed();
                    break;
                case PetInteractionAction.Clean:
                    needs.Clean();
                    break;
                case PetInteractionAction.Pet:
                    needs.Petting();
                    break;
                case PetInteractionAction.Play:
                    needs.PlayMiniGame();
                    break;
            }
        }

        void OnMouseDown()
        {
#if UNITY_EDITOR
            if (allowMouseClickInEditor)
                Interact();
#endif
        }
    }
}
