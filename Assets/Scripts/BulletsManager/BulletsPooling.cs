using System.Collections.Generic;
using Others;
using UnityEngine;

namespace BulletsManager
{
    public class BulletsPooling : Singleton<BulletsPooling>
    {
        [SerializeField] Bullets[] bulletsPrefab;
        [SerializeField] Transform firePoint;
        readonly Dictionary<string, Bullets[]> bullets = new Dictionary<string, Bullets[]>();

        // Start is called before the first frame update
        void Start()
        {
            bullets["PlayerBullets"] = new Bullets[50];
            bullets["EnemyBullets"] = new Bullets[50];
            bullets["CannonBullets"] = new Bullets[50];
            bullets["TankEnemyBullets"] = new Bullets[50];
            //Quaternion desiredRotation = Quaternion.Euler(90f, 0f, 0f);
            for (int i = 0; i < 100; i++)
            {
                if (i < 50)
                {
                    bullets["PlayerBullets"][i] =
                        Instantiate(bulletsPrefab[0], firePoint.position, Quaternion.identity);
                    bullets["PlayerBullets"][i].transform.parent = transform;
                    bullets["PlayerBullets"][i].gameObject.SetActive(false);
                    bullets["EnemyBullets"][i] = Instantiate(bulletsPrefab[1], firePoint.position, Quaternion.identity);
                    bullets["EnemyBullets"][i].transform.parent = transform;
                    bullets["EnemyBullets"][i].gameObject.SetActive(false);
                    bullets["TankEnemyBullets"][i] =
                        Instantiate(bulletsPrefab[2], firePoint.position, Quaternion.identity);
                    bullets["TankEnemyBullets"][i].transform.parent = transform;
                    bullets["TankEnemyBullets"][i].gameObject.SetActive(false);
                }
                else
                {
                    bullets["CannonBullets"][i - 50] =
                        Instantiate(bulletsPrefab[3], firePoint.position, Quaternion.identity);
                    bullets["CannonBullets"][i - 50].transform.parent = transform;
                    bullets["CannonBullets"][i - 50].gameObject.SetActive(false);
                }
            }
        }

        public PlayerBullet GetPlayerBullet()
        {
            for (int i = 0; i < bullets["PlayerBullets"].Length; i++)
            {
                if (!bullets["PlayerBullets"][i].gameObject.activeInHierarchy)
                {
                    return (PlayerBullet)bullets["PlayerBullets"][i];
                }
            }

            return null;
        }

        public void DenableBullet()
        {
            for (int i = 0; i < bullets["PlayerBullets"].Length; i++)
            {
                if (bullets["PlayerBullets"][i].gameObject.activeSelf)
                {
                    bullets["PlayerBullets"][i].gameObject.SetActive(false);
                }
            }
        }


        public EnemyBullet GetEnemyBullet()
        {
            for (int i = 0; i < bullets["EnemyBullets"].Length; i++)
            {
                if (!bullets["EnemyBullets"][i].gameObject.activeInHierarchy)
                {
                    return (EnemyBullet)bullets["EnemyBullets"][i];
                }
            }

            return null;
        }

        public CannonBullet GetCannonBullet(Vector3 position)
        {
            for (int i = 0; i < bullets["CannonBullets"].Length; i++)
            {
                if (!bullets["CannonBullets"][i].gameObject.activeInHierarchy)
                {
                    bullets["CannonBullets"][i].transform.position = position;
                    bullets["CannonBullets"][i].gameObject.SetActive(true);
                    return (CannonBullet)bullets["CannonBullets"][i];
                }
            }

            return null;
        }

        public TankEnemyBullet GetTankEnemyBullet()
        {
            for (int i = 0; i < bullets["TankEnemyBullets"].Length; i++)
            {
                if (!bullets["TankEnemyBullets"][i].gameObject.activeInHierarchy)
                {
                    return (TankEnemyBullet)bullets["TankEnemyBullets"][i];
                }
            }

            return null;
        }
    }
}