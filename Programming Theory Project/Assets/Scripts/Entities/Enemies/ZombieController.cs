using System.Collections;
using UnityEngine;

namespace Entities.Enemies
{
    public sealed class ZombieController : EnemyController
    {
        public float bulletSlowDown = 2f;

        private SphereCollider _sc;
        private Coroutine _restoreSpeedCoroutine;
        private Coroutine _meleePlayerCoroutine;
        
        public override void Start()
        {
            base.Start();

            _sc = GetComponent<SphereCollider>();
            
            _restoreSpeedCoroutine = null;
            _meleePlayerCoroutine = null;
        }

        private void FixedUpdate()
        {
            if (PlayerTransform && Agent.isActiveAndEnabled)
            {
                transform.LookAt(PlayerTransform);

                if (Agent.destination != PlayerTransform.position)
                    Agent.destination = PlayerTransform.position;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            Debug.Log($"{nameof(OnTriggerEnter)} {other.transform.name}");   
            
            var damageable = other.GetComponent<Damageable>();
            if (damageable)
            {
                RunSingleCoroutine(ref _meleePlayerCoroutine, MeleePlayerCoroutine(damageable));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            Debug.Log($"{nameof(OnTriggerExit)} {other.transform.name}");
            StopCoroutine(_meleePlayerCoroutine);
        }

        /// <summary>
        /// Slows down zombie when damaged
        /// </summary>
        /// <param name="damage">damage amount</param>
        protected override void OnDamaged(float damage)
        {
            if (Agent.speed >= OriginalSpeed)
                Agent.speed -= bulletSlowDown;

            RunSingleCoroutine(ref _restoreSpeedCoroutine, RestoreSpeedCoroutine(bulletSlowDown));
        }
        
        protected override void OnDeath()
        {
            Rb.isKinematic = false;
            _sc.enabled = false;
            
            StopAllCoroutines();
            _restoreSpeedCoroutine = null;
            _meleePlayerCoroutine = null;
            
            base.OnDeath();
        }
        public override void Spawn(Vector3 position)
        {
            _sc.enabled = true;
            base.Spawn(position);
        }
        
        /// <summary>
        /// Restores original speed after time
        /// </summary>
        /// <param name="time">time in seconds</param>
        private IEnumerator RestoreSpeedCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            Agent.speed += bulletSlowDown;
        }
        
        /// <summary>
        /// Melee damage coroutine
        /// </summary>
        /// <param name="damageable">damageable component</param>
        private IEnumerator MeleePlayerCoroutine(Damageable damageable)
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(1f);
                damageable.InflictDamage(dps, gameObject.name);   
            }
        }
    }
}