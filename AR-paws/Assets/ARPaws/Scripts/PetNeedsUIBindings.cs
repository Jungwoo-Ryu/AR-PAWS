using UnityEngine;
using UnityEngine.UI;

namespace ARPaws.UI
{
    /// <summary>
    /// Wire Canvas sliders / labels to <see cref="Core.PetNeedsController"/>.
    /// </summary>
    public class PetNeedsUIBindings : MonoBehaviour
    {
        [SerializeField] Core.PetNeedsController needs;
        [SerializeField] Slider hungerSlider;
        [SerializeField] Slider cleanlinessSlider;
        [SerializeField] Slider happinessSlider;
        [SerializeField] Slider playfulnessSlider;
        [SerializeField] Text moodLabel;

        void OnEnable()
        {
            if (needs != null)
                needs.OnStatsChanged += Apply;
        }

        void OnDisable()
        {
            if (needs != null)
                needs.OnStatsChanged -= Apply;
        }

        void Start()
        {
            if (needs != null)
                Apply(needs.Stats);
        }

        void Apply(Core.PetNeedStats s)
        {
            if (hungerSlider != null) hungerSlider.value = s.hunger;
            if (cleanlinessSlider != null) cleanlinessSlider.value = s.cleanliness;
            if (happinessSlider != null) happinessSlider.value = s.happiness;
            if (playfulnessSlider != null) playfulnessSlider.value = s.playfulness;
            if (moodLabel != null) moodLabel.text = needs != null ? needs.CurrentMood.ToString() : string.Empty;
        }
    }
}
