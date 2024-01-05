using UnityEngine;

namespace Others
{
    public struct PlayerStats
    {
        public float level;
        public float health;
        public float dmg;
        public float def;
    }

    public static class PlayerStatsSaveLoad 
    {
        public static void SavePlayerStats(PlayerStats playerStats)
        {
       
            string dataJson = JsonUtility.ToJson(playerStats);      
            PlayerPrefs.SetString("PlayerStats", dataJson);
            PlayerPrefs.Save();
        }

        public static PlayerStats LoadPlayerStats()
        {
            string dataJson = PlayerPrefs.GetString("PlayerStats");
            PlayerStats playerStats = JsonUtility.FromJson<PlayerStats>(dataJson);
            return playerStats;
        }
    }
}