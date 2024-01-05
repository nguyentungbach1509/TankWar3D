using BulletsManager;
using ICharacter;
using Level;
using Others;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class SoldierMovement : EnemyBase
    {
        private NavMeshAgent agent;
        [SerializeField] private float idleTime;
        [SerializeField] private Transform[] waypoint;

        [SerializeField] private Transform pointSpawn;

        // RangeTarget 
        [SerializeField] private float rangeAttack;
        [SerializeField] private float rangeTargetPlayer;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float coolDownTime;
        
        private float timeReload ;
        private int currentIndex;
        private float countDownTime;
        private Transform playerTransform;
        private Transform soldierTransfrom;
        private bool hasPlayer = false;
        private bool patrolSoldier;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();

            soldierTransfrom = transform;
            playerTransform = LevelManager.Instance.playerController.transform;
            agent.stoppingDistance = Random.Range(0f, 3f);
            CurrentHealth = MaxHealth;
            countDownTime = idleTime;

            if (waypoint.Length == 0)
            {
                patrolSoldier = false;
                agent.speed = 0;
            }
            else
            {
                patrolSoldier = true;
                agent.speed = moveSpeed;
            }

            IdlePatrol();
        }

        private void Update()
        {
            Movement();
        }

        public override void Movement()
        {
            float distance = Vector3.Distance(playerTransform.position, soldierTransfrom.position);
            if (patrolSoldier && distance >= rangeTargetPlayer && !hasPlayer)
            {
                ChangePositionMove();
            }

            if (distance <= rangeTargetPlayer)
            {
                hasPlayer = true;
                PlayerInsideRangeTarget(distance);
            }

            if(patrolSoldier && hasPlayer)
            {
                PlayerInsideRangeTarget(distance);
            }
        }

        private void PlayerInsideRangeTarget(float distance)
        {
            LookToPlayer();
            if (distance <= rangeAttack)
            {
                agent.speed = 0;
                Shoot();
            }
            else if(patrolSoldier)
            {
                agent.speed = moveSpeed;
                agent.SetDestination(playerTransform.position);
                ChangeAnim(Constant.ANIM_RUN);
            }
            else
            {
                IdlePatrol();
            }
        }

        /// <summary>
        /// Soldier have patrolSoldier= true move to position patrol
        /// </summary>
        private void ChangePositionMove()
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                countDownTime -= Time.deltaTime;
                if (countDownTime <= 0)
                {
                    currentIndex = (currentIndex + 1) % waypoint.Length;
                    agent.SetDestination(waypoint[currentIndex].position);
                    countDownTime = idleTime;
                }

                IdlePatrol();
            }
            else
            {
                ChangeAnim(Constant.ANIM_RUN);
            }
        }

        public override void Shoot()
        {
            timeReload += Time.deltaTime;
            if (timeReload > coolDownTime)
            {
                EnemyBullet bullet = BulletsPooling.Instance.GetEnemyBullet();
                bullet.OnInit(gameObject.transform.forward, gameObject, Damage);
                bullet.transform.position = pointSpawn.position;
                bullet.gameObject.SetActive(true);

                ChangeAnim(Constant.ANIM_ATTACK);
                timeReload = 0;
            }
            
        }

        private void LookToPlayer()
        {
            Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - soldierTransfrom.position);
            soldierTransfrom.rotation = Quaternion.Lerp(soldierTransfrom.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        private void IdlePatrol()
        {
            ChangeAnim(Constant.ANIM_IDLE);
            transform.rotation = Quaternion.Lerp(soldierTransfrom.rotation, Quaternion.Euler(0, 180, 0), rotateSpeed * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangeAttack);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangeTargetPlayer);
        }
    }
}
