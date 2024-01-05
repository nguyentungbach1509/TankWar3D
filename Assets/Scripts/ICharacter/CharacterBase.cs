using UnityEngine;

namespace ICharacter
{
    public abstract class CharacterBase : Stats
    {
        public float moveSpeed;
        protected Animator animator;
        private string animName = "";
        public abstract void Movement();
        public abstract void Shoot();

        protected void ChangeAnim(string animNameType)
        {
            if (animName != animNameType)
            {
                animator.ResetTrigger(animName);
                animName = animNameType;
                animator.SetTrigger(animName);
            }
        }
    }
}