using Gold;
using Others;
using Tank;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        public PlayerController playerController;

        public UiWin uiWin;
        public UiFail uiFail;


        private void NextLevel()
        {
            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            if (levelIndex < 5)
            {
                int goldTemp = PlayerPrefs.GetInt("PlayerGold");
                Debug.Log(goldTemp);
                PlayerPrefs.SetInt("PlayerGold", GoldController.Instance.Amount + goldTemp);
                SceneManager.LoadSceneAsync(levelIndex + 1);
                Time.timeScale = 1;
            }
            else
            {
                Home();
            }
        }

        public void Home()
        {
            SceneManager.LoadScene("SceneUI");
        }

        public void OnClickNextLevel()
        {
            NextLevel();
        }

        public void ReStart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}