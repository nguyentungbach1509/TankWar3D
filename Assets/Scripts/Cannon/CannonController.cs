using BulletsManager;
using ICharacter;
using Level;
using Tank;
using UnityEngine;

namespace Cannon
{
    public class CannonController : EnemyBase
    {
        [SerializeField] Transform firePoint;
        [SerializeField] float minRangeAttack;
        [SerializeField] float maxRangeAttack;
        [SerializeField] float attackCoolDown;
        [SerializeField] float rotateSpeed;
        PlayerController player;
        float currentTime;

        private void Start()
        {
            CurrentHealth = MaxHealth;
            player = LevelManager.Instance.playerController;
        }

        private void Update()
        {
            if (player != null)
            {
                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (distance > minRangeAttack && distance < maxRangeAttack)
                {
                    LookToPlayer();
                    if (currentTime >= attackCoolDown)
                    {
                        Shoot();
                    }
                    else
                    {
                        currentTime += Time.deltaTime;
                    }
                }
            }
        }

        public override void Shoot()
        {
            currentTime = 0;
            CannonBullet bullet = BulletsPooling.Instance.GetCannonBullet(firePoint.position);
            bullet.MoveTime = 0;
            bullet.Damage = Damage;
            bullet.FirePoint = firePoint.position;
            bullet.FireTarget = player.transform.position;
        }

        void LookToPlayer()
        {
            if (player.transform != null)
            {
                Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maxRangeAttack);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, minRangeAttack);
        }
    }
}