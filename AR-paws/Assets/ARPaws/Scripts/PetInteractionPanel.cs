using UnityEngine;
using UnityEngine.UI;

namespace ARPaws.UI
{
    /// <summary>
    /// Optional UI buttons calling interaction methods on the pet controller.
    /// </summary>
    public class PetInteractionPanel : MonoBehaviour
    {
        [SerializeField] Core.PetNeedsController needs;
        [SerializeField] Button feedButton;
        [SerializeField] Button cleanButton;
        [SerializeField] Button petButton;
        [SerializeField] Button playButton;

        void Awake()
        {
            if (feedButton != null) feedButton.onClick.AddListener(() => needs?.Feed());
            if (cleanButton != null) cleanButton.onClick.AddListener(() => needs?.Clean());
            if (petButton != null) petButton.onClick.AddListener(() => needs?.Petting());
            if (playButton != null) playButton.onClick.AddListener(() => needs?.PlayMiniGame());
        }
    }
}
