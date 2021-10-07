using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour
    {
        public float maxHealth = 10f;
        
        public UnityAction<float> OnDamaged;
        public UnityAction<float> OnHealed;
        public UnityAction OnDeath;
        
        public float CurrentHealth { get; private set; }

        private bool _isDead;

        private void Start()
        {
            SetStartValues();   
        }

        /// <summary>
        /// Sets the class members to start values
        /// </summary>
        public void SetStartValues()
        {
            CurrentHealth = maxHealth;
            _isDead = false;
        }
        
        /// <summary>
        /// Add health to current health
        /// </summary>
        /// <param name="amount">amount to add</param>
        public void Heal(float amount)
        {
            CurrentHealth += amount;
            ClampHealth();
            
            OnHealed?.Invoke(amount);
        }

        /// <summary>
        /// Removes health from current health
        /// </summary>
        /// <param name="damage">amount to remove</param>
        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            ClampHealth();
            
            OnDamaged?.Invoke(damage);
            
            DeathHandler();
        }

        /// <summary>
        /// Keeps the CurrentHealth value within 0 and maxHealth
        /// </summary>
        private void ClampHealth() => CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);   
        
        /// <summary>
        /// Sets CurrentHealth to zero and calls the death handler
        /// </summary>
        public void Kill()
        {
            CurrentHealth = 0f;
            DeathHandler();
        }

        /// <summary>
        /// Invokes OnDeath and sets _isDead to true when CurrentHealth is smaller or equal to zero
        /// </summary>
        private void DeathHandler()
        {
            if (_isDead) return;
            
            if (CurrentHealth <= 0f)
            {
                _isDead = true;
                OnDeath?.Invoke();
            }
        }
    }
}