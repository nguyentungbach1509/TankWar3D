using ICharacter;
using Others;
using UnityEngine;

namespace Tank
{
    public class RangePlayer : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constant.TAG_ENEMY))
            {
                CharacterBase characterBase = other.GetComponent<CharacterBase>();
                if (!characterBase.IsDeadth)
                {
                    playerController.AddList(characterBase);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Constant.TAG_ENEMY))
            {
                CharacterBase characterBase = other.GetComponent<CharacterBase>();
                playerController.RemoveList(characterBase);
            }
        }
    }
}