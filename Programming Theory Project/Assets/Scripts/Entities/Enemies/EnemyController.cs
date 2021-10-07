using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Entities.Enemies
{
    [Serializable]
    [RequireComponent(typeof(NavMeshAgent), typeof(CapsuleCollider),typeof(Rigidbody))]
    public abstract class EnemyController : MonoBehaviour, IPoolable
    {
        public int points;
        public float dps;
        public float timeToDeSpawn = 2f;
        
        public Health health;
        protected NavMeshAgent Agent;
        protected Rigidbody Rb;
        protected Transform PlayerTransform;
        
        protected float OriginalSpeed;

        public UnityAction<int> OnAddPoints;
        
        public virtual void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
            Rb = GetComponent<Rigidbody>();
            PlayerTransform = GameObject.FindWithTag("Player").transform;
            health = GetComponent<Health>();
            health.OnDamaged += OnDamaged;
            health.OnDeath += OnDeath;

            OriginalSpeed = Agent.speed;
            
            gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Damage Logic
        /// </summary>
        /// <param name="damage"></param>
        protected virtual void OnDamaged(float damage) { }
        
        /// <summary>
        /// Death logic
        /// </summary>
        protected virtual void OnDeath()
        {
            Agent.enabled = false;
            OnAddPoints?.Invoke(points);
            StartCoroutine(DeSpawnCoroutine(timeToDeSpawn));
        }
        
        /// <summary>
        /// Sets the transform to starting values
        /// </summary>
        private void SetTransformStartValues(Vector3 position)
        {
            transform.position = position;
            transform.rotation = Quaternion.identity;
        }

        /// <summary>
        /// Sets the rigidbody to stating values
        /// </summary>
        private void SetRigidBodyStartValues()
        {
            Rb.velocity = Vector3.zero;
            Rb.angularVelocity = Vector3.zero;
            Rb.isKinematic = true;
        }

        /// <summary>
        /// Sets nav agent to starting values
        /// </summary>
        private void SetAgentStartValues()
        {
            Agent.enabled = true;
            Agent.speed = OriginalSpeed;
        }
        
        /// <summary>
        /// Resets Game object to starting values and enables it
        /// </summary>
        public virtual void Spawn(Vector3 position)
        {
            health.SetStartValues();
            SetTransformStartValues(position);
            SetRigidBodyStartValues();
            SetAgentStartValues();

            gameObject.SetActive(true);
        }
        
        /// <summary>
        /// Delays de spawning of gameObject
        /// </summary>
        /// <returns></returns>
        public IEnumerator DeSpawnCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Verifies that only one coroutine is running at a time
        /// </summary>
        /// <param name="coroutine">reference coroutine</param>
        /// <param name="coroutineToRun">coroutine to run</param>
        protected void RunSingleCoroutine(ref Coroutine coroutine, IEnumerator coroutineToRun)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(coroutineToRun);
        }
    }
}