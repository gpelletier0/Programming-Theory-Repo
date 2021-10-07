using UnityEngine;

namespace Entities
{
    public class Damageable : MonoBehaviour
    {
        private Health Health { get; set; }

        private void Awake()
        {
            Health = GetComponent<Health>();
            if (!Health)
                Health = GetComponentInParent<Health>();
        }

        /// <summary>
        /// Removes hp from health
        /// </summary>
        /// <param name="damage">amount of damage to remove</param>
        /// <param name="damageSourceName">name of damage source</param>
        public void InflictDamage(float damage, string damageSourceName)
        {
            if (!Health) return;

            Debug.Log($"{nameof(InflictDamage)}() {damageSourceName} damages {gameObject.name} for {damage}");
            Health.TakeDamage(damage);
        }
    }
}
