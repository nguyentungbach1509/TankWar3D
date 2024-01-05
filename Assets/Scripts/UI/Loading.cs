using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace UI
{
    public class Loading : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI textPercent;
        private float timer;
        private int percent;

        private void Start()
        {
            PlayerPrefs.SetInt("PlayerEnergy", 20);
            PlayerPrefs.Save();
            timer = Random.Range(1, 4f);
            image.DOFillAmount(1, timer);
        }

        private void Update()
        {
            percent = (int)(image.fillAmount * 100);
            textPercent.SetText(percent.ToString());
            if (percent == 90)
            {
                SceneManager.LoadSceneAsync("SceneUI");
            }
        }
    }
}