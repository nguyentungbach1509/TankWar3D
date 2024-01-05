using Level;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiFail : MonoBehaviour
    {
        [SerializeField] private Button btnHome, btnReStart;

        private void Start()
        {
            btnReStart.onClick.AddListener(LevelManager.Instance.ReStart);
            btnHome.onClick.AddListener(LevelManager.Instance.Home);
        }
    }
}