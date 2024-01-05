using Level;
using UnityEngine;

namespace Gold
{
    public class GoldCoin : MonoBehaviour
    {
        [SerializeField] int amount;
        [SerializeField] float flySpeed;
        Transform playerTransform;
        Transform coinTransform;

        private void Start()
        {
            playerTransform = LevelManager.Instance.playerController.transform;
            coinTransform = transform;
        }

        private void Update()
        {
            Vector3 savePosition = coinTransform.position;
            if(Vector3.Distance(coinTransform.position, playerTransform.position) >= 1f)
            {
                Vector3 direction = (playerTransform.position - coinTransform.position).normalized;
                Vector3 newPos = savePosition + direction * flySpeed * Time.deltaTime;
                newPos.y = savePosition.y;
                coinTransform.position = newPos;   
            }
            else
            {
                GoldController.Instance.UpgradeGoldUI(amount);
                gameObject.SetActive(false);
            }
        }
    }
}
