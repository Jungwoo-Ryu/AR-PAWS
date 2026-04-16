using System;
using UnityEngine;

namespace ARPaws.Core
{
    public enum PetInteractionType
    {
        Feed,
        Clean,
        Pet,
        Play
    }

    public enum PetMood
    {
        Content,
        Needy,
        Grumpy,
        Playful
    }

    /// <summary>
    /// Core loop: needs decay over time; interactions push values up.
    /// Raises <see cref="OnStatsChanged"/> for UI and AR reactions.
    /// </summary>
    public class PetNeedsController : MonoBehaviour
    {
        [Header("Decay per real-time second (0–1 scale)")]
        [SerializeField] float hungerDecay = 0.015f / 3600f;
        [SerializeField] float cleanlinessDecay = 0.01f / 3600f;
        [SerializeField] float happinessDecay = 0.012f / 3600f;
        [SerializeField] float playfulnessDecay = 0.02f / 3600f;

        [Header("Interaction deltas")]
        [SerializeField] float feedAmount = 0.35f;
        [SerializeField] float cleanAmount = 0.4f;
        [SerializeField] float pettingAmount = 0.12f;
        [SerializeField] float playAmount = 0.25f;

        PetNeedStats _stats;
        PetMood _lastMood = PetMood.Content;
        float _uiBroadcastTimer;

        public PetNeedStats Stats => _stats;
        public PetMood CurrentMood { get; private set; } = PetMood.Content;

        public event Action<PetNeedStats> OnStatsChanged;
        public event Action<PetMood> OnMoodChanged;
        public event Action<PetInteractionType> OnInteraction;

        void Awake()
        {
            if (PetNeedsPersistence.TryLoad(out var loaded, out var elapsedSeconds))
            {
                _stats = loaded;
                if (elapsedSeconds > 0f)
                    ApplyDecay(elapsedSeconds);
            }
            else
                _stats = PetNeedStats.CreateDefault();
            RecalculateMood();
            _lastMood = CurrentMood;
            OnStatsChanged?.Invoke(_stats);
        }

        void Update()
        {
            var dt = Time.unscaledDeltaTime;
            ApplyDecay(dt);

            _uiBroadcastTimer += dt;
            if (_uiBroadcastTimer >= 0.2f || CurrentMood != _lastMood)
            {
                _uiBroadcastTimer = 0f;
                if (CurrentMood != _lastMood)
                    OnMoodChanged?.Invoke(CurrentMood);
                _lastMood = CurrentMood;
                OnStatsChanged?.Invoke(_stats);
            }
        }

        void OnApplicationPause(bool pause)
        {
            if (pause)
                PetNeedsPersistence.Save(_stats);
        }

        void OnApplicationQuit()
        {
            PetNeedsPersistence.Save(_stats);
        }

        public void Feed()
        {
            _stats.hunger = Mathf.Clamp01(_stats.hunger + feedAmount);
            _stats.happiness = Mathf.Clamp01(_stats.happiness + feedAmount * 0.15f);
            BumpChanged(PetInteractionType.Feed);
        }

        public void Clean()
        {
            _stats.cleanliness = Mathf.Clamp01(_stats.cleanliness + cleanAmount);
            _stats.happiness = Mathf.Clamp01(_stats.happiness + 0.08f);
            BumpChanged(PetInteractionType.Clean);
        }

        public void Petting()
        {
            _stats.happiness = Mathf.Clamp01(_stats.happiness + pettingAmount);
            _stats.playfulness = Mathf.Clamp01(_stats.playfulness + pettingAmount * 0.5f);
            BumpChanged(PetInteractionType.Pet);
        }

        public void PlayMiniGame()
        {
            _stats.playfulness = Mathf.Clamp01(_stats.playfulness + playAmount);
            _stats.happiness = Mathf.Clamp01(_stats.happiness + playAmount * 0.6f);
            _stats.hunger = Mathf.Clamp01(_stats.hunger - playAmount * 0.08f);
            BumpChanged(PetInteractionType.Play);
        }

        void ApplyDecay(float deltaSeconds)
        {
            _stats.hunger = Mathf.Clamp01(_stats.hunger - hungerDecay * deltaSeconds);
            _stats.cleanliness = Mathf.Clamp01(_stats.cleanliness - cleanlinessDecay * deltaSeconds);
            _stats.happiness = Mathf.Clamp01(_stats.happiness - happinessDecay * deltaSeconds);
            _stats.playfulness = Mathf.Clamp01(_stats.playfulness - playfulnessDecay * deltaSeconds);
            RecalculateMood();
        }

        void BumpChanged(PetInteractionType interaction)
        {
            var prevMood = CurrentMood;
            RecalculateMood();
            if (CurrentMood != prevMood)
                OnMoodChanged?.Invoke(CurrentMood);
            _lastMood = CurrentMood;
            _uiBroadcastTimer = 0f;
            OnStatsChanged?.Invoke(_stats);
            OnInteraction?.Invoke(interaction);
            PetNeedsPersistence.Save(_stats);
        }

        void RecalculateMood()
        {
            if (_stats.hunger < 0.25f || _stats.cleanliness < 0.25f)
                CurrentMood = PetMood.Grumpy;
            else if (_stats.happiness < 0.35f || _stats.playfulness < 0.3f)
                CurrentMood = PetMood.Needy;
            else if (_stats.playfulness > 0.75f)
                CurrentMood = PetMood.Playful;
            else
                CurrentMood = PetMood.Content;
        }
    }
}
