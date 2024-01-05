using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIPause : MonoBehaviour
    {
        public void PauseButtonHandler()
        {
            Time.timeScale = 0;
        }

        public void ContinueButtonHandler()
        {
            Time.timeScale = 1;
        }

        public void QuitButtonHandler()
        {
            Application.Quit();
        }

        public void HomeButtonHandler()
        {
            SceneManager.LoadScene(1);
        }
    }
}