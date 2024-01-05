using Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiWin : MonoBehaviour
    {
        [SerializeField] private Button btnNextLevel;
        [SerializeField] private TextMeshProUGUI textCoin;

        private void Start()
        {
            btnNextLevel.onClick.AddListener(LevelManager.Instance.OnClickNextLevel);
        }

        public void SetCoin(int coin)
        {
            textCoin.SetText(coin.ToString());
        }
    }
}