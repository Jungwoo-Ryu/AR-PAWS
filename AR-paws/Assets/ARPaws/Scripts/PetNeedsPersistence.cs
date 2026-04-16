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
        const int SaveVersion = 2;

        static string FilePath => Path.Combine(Application.persistentDataPath, FileName);

        public static bool TryLoad(out PetNeedStats stats)
        {
            return TryLoad(out stats, out _);
        }

        public static bool TryLoad(out PetNeedStats stats, out float elapsedSeconds)
        {
            stats = PetNeedStats.CreateDefault();
            elapsedSeconds = 0f;
            try
            {
                if (!File.Exists(FilePath))
                    return false;
                var json = File.ReadAllText(FilePath);
                var snapshot = JsonUtility.FromJson<PetNeedSnapshot>(json);
                if (snapshot == null)
                    return false;

                stats = snapshot.ToStats();
                stats.Clamp01();

                if (snapshot.savedUnixTimeSeconds > 0)
                {
                    var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    elapsedSeconds = Mathf.Max(0f, now - snapshot.savedUnixTimeSeconds);
                }
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
            public int saveVersion;
            public long savedUnixTimeSeconds;
            public float hunger;
            public float cleanliness;
            public float happiness;
            public float playfulness;

            public static PetNeedSnapshot FromStats(in PetNeedStats s)
            {
                return new PetNeedSnapshot
                {
                    saveVersion = SaveVersion,
                    savedUnixTimeSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
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
