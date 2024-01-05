using Others;
using UnityEngine;

namespace Tank
{
    public class RangeEnemyTank : MonoBehaviour
    {
        [SerializeField] private TankEnemy tankEnemy;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constant.TAG_PLAYER))
            {
                PlayerController playerController = other.GetComponent<PlayerController>();
                tankEnemy.playerController = playerController;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Constant.TAG_PLAYER))
            {
                tankEnemy.playerController = null;
            }
        }
    }
}