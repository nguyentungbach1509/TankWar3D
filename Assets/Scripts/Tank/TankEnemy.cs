using BulletsManager;
using ICharacter;
using UnityEngine;
using UnityEngine.AI;
namespace Tank
{
    public class TankEnemy : EnemyBase
    {
        public PlayerController playerController;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private GameObject head, spawnBullet;
        [SerializeField] private Level.Level currentLevel;
        private Vector3 destination;
        private float timerDelayAttack;
        private bool isShoot;

        private void Start()
        {
            CurrentHealth = MaxHealth;
            SetDestination(currentLevel.GetRandomPosition());
        }

        void Update()
        {
            if (!IsDeadth)
            {
                if (playerController == null)
                {
                    timerDelayAttack = 0;
                    isShoot = false;
                    head.transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (IsDestination)
                    {
                        SetDestination(currentLevel.GetRandomPosition());
                    }
                }
                else
                {
                    SetDestination(transform.position);
                    if (!isShoot)
                    {
                        head.transform.LookAt(playerController.transform);
                    }

                    Shoot();
                }
            }
        }

        private void SetDestination(Vector3 position)
        {
            destination = position;
            destination.y = 0;
            navMeshAgent.SetDestination(position);
        }

        private bool IsDestination => Vector3.Distance(destination,
            Vector3.right * transform.position.x + Vector3.forward * transform.position.z) < 0.1f;

        public override void Shoot()
        {
            timerDelayAttack += Time.deltaTime;
            isShoot = true;
            if (timerDelayAttack >= 1f)
            {
                TankEnemyBullet bullet = BulletsPooling.Instance.GetTankEnemyBullet();
                bullet.transform.position = spawnBullet.transform.position;
                bullet.OnInit(head.transform.forward, head, Damage);
                bullet.gameObject.SetActive(true);
                timerDelayAttack = 0f;
                isShoot = false;
            }
        }

        public override void Movement()
        {
        }
    }
}