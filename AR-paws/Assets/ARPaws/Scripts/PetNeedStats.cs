using System;
using UnityEngine;

namespace ARPaws.Core
{
    /// <summary>
    /// Serializable snapshot of Tamagotchi-style needs (0 = empty/bad, 1 = full/good).
    /// </summary>
    [Serializable]
    public struct PetNeedStats
    {
        [Range(0f, 1f)] public float hunger;
        [Range(0f, 1f)] public float cleanliness;
        [Range(0f, 1f)] public float happiness;
        [Range(0f, 1f)] public float playfulness;

        public static PetNeedStats CreateDefault()
        {
            return new PetNeedStats
            {
                hunger = 0.75f,
                cleanliness = 0.8f,
                happiness = 0.7f,
                playfulness = 0.65f
            };
        }

        public void Clamp01()
        {
            hunger = Mathf.Clamp01(hunger);
            cleanliness = Mathf.Clamp01(cleanliness);
            happiness = Mathf.Clamp01(happiness);
            playfulness = Mathf.Clamp01(playfulness);
        }
    }
}
