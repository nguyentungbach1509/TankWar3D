using System.Collections.Generic;
using BulletsManager;
using ICharacter;
using Level;
using UnityEngine; 

namespace Tank
{
    public class PlayerController : CharacterBase
    {
        [SerializeField] private Rigidbody rigidBody;
        public FloatingJoystick joystick;
        [SerializeField] private GameObject head, spawnBullet;
        [SerializeField] private float rotationSpeed = 15;
        private Vector3 direction;
        private float timerDelayAttack;
        private bool isAttack;
        private readonly List<CharacterBase> listCharacterBases = new List<CharacterBase>();

        private void Start()
        {

            if (PlayerPrefs.HasKey("PlayerCurrentHealth"))
            {
                CurrentHealth = PlayerPrefs.GetFloat("PlayerCurrentHealth");
                ShowSaveHealth();
            }
            else
            {
                CurrentHealth = MaxHealth;
            }
        }
        
        private void Update()
        {
            if (!IsDeadth)
            {
                Vector3 cameraRight = Camera.main.transform.right;
                Vector3 cameraUp = Camera.main.transform.up;
                cameraUp.y = 0f;
                cameraRight.y = 0f;
                cameraUp.Normalize();
                cameraRight.Normalize();
                direction = cameraUp * joystick.Vertical + cameraRight * joystick.Horizontal;
                if (listCharacterBases.Count > 0)
                {
                    for (int i = 0; i < listCharacterBases.Count; i++)
                    {
                        if (listCharacterBases[i] == null)
                        {
                            listCharacterBases.RemoveAt(i);
                        }
                    }
                }


                if (GetTargetNearest() != null)
                {
                    head.transform.LookAt(GetTargetNearest().gameObject.transform);
                }
                else
                {
                    head.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

                Movement();
                if (joystick.Horizontal != 0 || joystick.Vertical != 0)
                {
                    RotateCharacter();
                    timerDelayAttack = 0f;
                }
                else
                {
                    if (GetTargetNearest() != null)
                    {
                        Shoot();
                    }
                    else
                    {
                        timerDelayAttack = 0f;
                    }
                }
            }
        }

        public override void Movement()
        {
            if (direction != Vector3.zero)
            {
                Vector3 movement = direction.normalized * 5f;
                rigidBody.velocity = movement;
            }
            else
            {
                rigidBody.velocity = Vector3.zero;
            }
        }

        private void RotateCharacter()
        {
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                rigidBody.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation,
                    Time.fixedDeltaTime * rotationSpeed));
            }
        }

        private CharacterBase GetTargetNearest()
        {
            CharacterBase nearestEnemy = null;
            float minDistance = float.PositiveInfinity;
            foreach (var t in listCharacterBases)
            {
                float distance = Vector3.Distance(t.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = t;
                }
            }

            return nearestEnemy;
        }

        public void RemoveList(CharacterBase characterBase)
        {
            listCharacterBases.Remove(characterBase);
        }

        public void AddList(CharacterBase characterBase)
        {
            listCharacterBases.Add(characterBase);
        }

        public override void Shoot()
        {
            timerDelayAttack += Time.deltaTime;
            if (timerDelayAttack >= 0.5f)
            {
                PlayerBullet bullet = BulletsPooling.Instance.GetPlayerBullet();
                bullet.transform.position = spawnBullet.transform.position;
                bullet.OnInit(head.transform.forward, head, Damage);
                bullet.gameObject.SetActive(true);
                timerDelayAttack = 0f;
            }
        }

        protected override void Die()
        {
            IsDeadth = true;
            Time.timeScale = 0;
            LevelManager.Instance.uiFail.gameObject.SetActive(true);
            joystick.gameObject.SetActive(false);
        }
    }
}