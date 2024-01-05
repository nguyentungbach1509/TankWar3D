using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UiStart : MonoBehaviour
    {
        [SerializeField] private GameObject load;
        [SerializeField] private UIMapSwipeController uiSwipe;

        public void OnClickStart()
        {
            Time.timeScale = 1.0f;
            int currentEnergy = PlayerPrefs.GetInt("PlayerEnergy");
            PlayerPrefs.SetInt("PlayerEnergy", currentEnergy - 5);
            if (PlayerPrefs.HasKey("PlayerCurrentHealth"))
            {
                PlayerPrefs.DeleteKey("PlayerCurrentHealth");
            }
            if(!PlayerPrefs.HasKey("PlayerGold")) PlayerPrefs.SetInt("PlayerGold", 0);
            LoadScene();
        }

        private void LoadScene()
        {
            int levelIndex = uiSwipe.MapIndex + 2; 
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelIndex);
            if (!asyncLoad.isDone)
            {
                load.gameObject.SetActive(true);
            }
        }
    }
}