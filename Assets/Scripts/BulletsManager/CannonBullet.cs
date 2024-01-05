using ICharacter;
using Others;
using UnityEngine;

namespace BulletsManager
{
    public class CannonBullet : Bullets
    {
        [SerializeField] float raycastDistance;
        public Vector3 FirePoint { get; set; }
        public Vector3 FireTarget { get; set; }
        Vector3 middle;
        public float MoveTime { get; set; }
        private string tagGameObject;

        protected override void BulletFly()
        {
            MoveTime += Time.deltaTime * Speed;
            transform.position = EvaluateShoot(MoveTime);
            transform.forward = EvaluateShoot(MoveTime + 0.001f) - transform.position;

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                tagGameObject = hit.collider.tag;
            }
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
        }

        Vector3 EvaluateShoot(float time)
        {
            Vector3 fireTargetTemp = new Vector3(FireTarget.x, FireTarget.y - 0.6f, FireTarget.z);
            middle = Vector3.Lerp(FirePoint, fireTargetTemp, 0.5f);
            middle = new Vector3(middle.x, middle.y + 8, middle.z);
            Vector3 startToMiddle = Vector3.Lerp(FirePoint, middle, time);
            Vector3 middleToTarget = Vector3.Lerp(middle, fireTargetTemp, time);
            return Vector3.Lerp(startToMiddle, middleToTarget, time);
        }

        private void OnTriggerEnter(Collider other)
        {
            ExplodePooling.Instance.Explode(transform.position);
            if (other.CompareTag(Constant.TAG_BRICK) || tagGameObject == Constant.TAG_BRICK)
            {
                Destroy(other.gameObject);
                gameObject.SetActive(false);
            }
            else if (other.CompareTag(Constant.TAG_WALL) || tagGameObject == Constant.TAG_WALL)
            {
                gameObject.SetActive(false);
            }
            else
            {
                CharacterBase character = other.GetComponent<CharacterBase>();
                if (!character.IsDeadth)
                {
                    character.TakenDamage(Damage);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}