using BulletsManager;
using Gold;
using ICharacter;
using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enemies
{
    public class BaseEnemy : CharacterBase
    {
        public override void Movement()
        {
        }

        public override void Shoot()
        {
        }

        protected override void Die()
        {
            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            BulletsPooling.Instance.DenableBullet();
            Time.timeScale = 0;
            if(levelIndex <4)
            {
                LevelManager.Instance.uiWin.gameObject.SetActive(true);
                LevelManager.Instance.uiWin.SetCoin(GoldController.Instance.Amount);
            }
            else
            {
                LevelManager.Instance.Home();
            }
        }
    }
}