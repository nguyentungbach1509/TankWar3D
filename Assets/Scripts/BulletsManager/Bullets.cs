using ICharacter;
using Others;
using UnityEngine;

namespace BulletsManager
{
    public class Bullets : MonoBehaviour
    {
        [SerializeField] float speed;

        protected float Speed
        {
            get { return speed; }
        }

        private Vector3 direction;
        [SerializeField] private GameObject imageBullet;
        public float Damage { get; set; }
        private float timer;

        public void OnInit(Vector3 driect, GameObject gun, float dmg)
        {
            direction = driect;
            Damage = dmg;
            timer = 0;

            imageBullet.transform.rotation = gun.transform.rotation;
        }

        private void Update()
        {
            BulletFly();
        }

        protected virtual void BulletFly()
        {
            timer += Time.deltaTime;
            transform.Translate(direction * speed * Time.deltaTime);
            if (timer >= 5f)
            {
                DestroyBullet();
            }
        }

        private void DestroyBullet()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            ExplodePooling.Instance.Explode(transform.position);
            if (other.CompareTag(Constant.TAG_BRICK))
            {
                Destroy(other.gameObject);
                DestroyBullet();
            }
            else if (other.CompareTag(Constant.TAG_WALL))
            {
                DestroyBullet();
            }
            else
            {
                CharacterBase character = other.GetComponent<CharacterBase>();
                if (!character.IsDeadth)
                {
                    character.TakenDamage(Damage);
                    DestroyBullet();
                }
            }
        }
    }
}