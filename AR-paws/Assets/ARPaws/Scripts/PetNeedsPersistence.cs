using System;
using System.IO;
using UnityEngine;

namespace ARPaws.Core
{
    /// <summary>
    /// Saves pet stats to persistentDataPath as JSON (survives app restarts).
    /// </summary>
    public static class PetNeedsPersistence
    {
        const string FileName = "arpaws_pet_needs.json";

        static string FilePath => Path.Combine(Application.persistentDataPath, FileName);

        public static bool TryLoad(out PetNeedStats stats)
        {
            stats = PetNeedStats.CreateDefault();
            try
            {
                if (!File.Exists(FilePath))
                    return false;
                var json = File.ReadAllText(FilePath);
                stats = JsonUtility.FromJson<PetNeedSnapshot>(json).ToStats();
                stats.Clamp01();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[ARPaws] Load failed: {e.Message}");
                return false;
            }
        }

        public static void Save(in PetNeedStats stats)
        {
            try
            {
                var snap = PetNeedSnapshot.FromStats(stats);
                var json = JsonUtility.ToJson(snap, true);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[ARPaws] Save failed: {e.Message}");
            }
        }

        [Serializable]
        class PetNeedSnapshot
        {
            public float hunger;
            public float cleanliness;
            public float happiness;
            public float playfulness;

            public static PetNeedSnapshot FromStats(in PetNeedStats s)
            {
                return new PetNeedSnapshot
                {
                    hunger = s.hunger,
                    cleanliness = s.cleanliness,
                    happiness = s.happiness,
                    playfulness = s.playfulness
                };
            }

            public PetNeedStats ToStats()
            {
                return new PetNeedStats
                {
                    hunger = hunger,
                    cleanliness = cleanliness,
                    happiness = happiness,
                    playfulness = playfulness
                };
            }
        }
    }
}
