using UnityEngine;

namespace ICharacter
{
    public abstract class Stats : MonoBehaviour
    {
        [SerializeField] float maxHealth;
        public float MaxHealth => maxHealth;
        [SerializeField] float currentHealth;

        protected float CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }

        [SerializeField] float damage;
        public float Damage => damage;

        public delegate void OnHealthChange(float health);

        public event OnHealthChange onHealthChange;
        public bool IsDeadth { get; protected set; }

        public void TakenDamage(float damageTaken)
        {
            CurrentHealth -= damageTaken;
            if (onHealthChange != null) onHealthChange(CurrentHealth / MaxHealth);
            if (CurrentHealth <= 0)
            {
                IsDeadth = true;
                Die();
            }
        }

        protected void ShowSaveHealth()
        {
            if (onHealthChange != null) onHealthChange(CurrentHealth / MaxHealth);
        }

        protected abstract void Die();
    }
}