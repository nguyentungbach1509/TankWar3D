using Others;
using Tank;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIUpgradeTank : MonoBehaviour
    {
        [SerializeField] PlayerController playerController;
        [SerializeField] float upgradeConstValue;
        [SerializeField] Text level;
        [SerializeField] Text textHealthCurrent;
        [SerializeField] Text textAtkCurrent;
        [SerializeField] Text textDefCurrent;
        [SerializeField] Text textHealthAdd;
        [SerializeField] Text textAtkAdd;
        [SerializeField] Text textDefAdd;
        PlayerStats playerStats;

        private void Update()
        {
            if (gameObject.activeInHierarchy)
            {
                StatsShow();
            }
        }

        void StatsShow()
        {
            if (!PlayerPrefs.HasKey("PlayerStats"))
            {
                playerStats.level = 1;
                playerStats.health = playerController.MaxHealth;
                playerStats.dmg = playerController.Damage;
                playerStats.def = 10;
                PlayerStatsSaveLoad.SavePlayerStats(playerStats);
            }

            playerStats = PlayerStatsSaveLoad.LoadPlayerStats();
            StatsOnUI();
        }

        void StatsOnUI()
        {
            level.text = "Lv." + playerStats.level;
            textHealthCurrent.text = playerStats.health.ToString();
            textAtkCurrent.text = playerStats.dmg.ToString();
            textDefCurrent.text = playerStats.def.ToString();
            textHealthAdd.text = "(+" + upgradeConstValue.ToString() + ")";
            textAtkAdd.text = "(+" + upgradeConstValue.ToString() + ")";
            textDefAdd.text = "(+" + upgradeConstValue.ToString() + ")";
        }

        public void UpgradeStats()
        {
            if (ReduceGold())
            {
                playerStats.health += upgradeConstValue;
                playerStats.dmg += upgradeConstValue;
                playerStats.def += upgradeConstValue;
                upgradeConstValue += 10;
                playerStats.level++;
                PlayerStatsSaveLoad.SavePlayerStats(playerStats);
            }
            
        }

        private bool ReduceGold()
        {
            int goldPlayer = PlayerPrefs.GetInt("PlayerGold");
            if (goldPlayer >= 300)
            {
                PlayerPrefs.SetInt("PlayerGold", goldPlayer - 300);
                PlayerPrefs.Save();
                return true;
            }

            return false;
        }
    }
}