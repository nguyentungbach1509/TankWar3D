using System.Collections;
using Gold;
using UnityEngine;

namespace ICharacter
{
    public class EnemyBase : CharacterBase
    {
        protected override void Die()
        {
            StartCoroutine(DelayAfterDie());
        }

        public override void Movement()
        {
        }

        public override void Shoot()
        {
        }

        IEnumerator DelayAfterDie()
        {
            for (int i = 0; i < Random.Range(1, 7); i++)
            {
                yield return new WaitForSeconds(0.4f);
                GoldController.Instance.SpawnGold(transform.position);
            }

            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }
}