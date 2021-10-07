using Entities;
using Managers;
using UnityEngine;

namespace Guns
{
    [RequireComponent(typeof(AudioSource))]
    public class DefaultGun : MonoBehaviour
    {
        public float damage = 5f;
        public float range = 100f;
        public float fireRate = 0.3f;
        public float impactForce = 30f;
        public Camera playerCamera;
        public AudioSource audioSource;
        public GameObject muzzleFlash;
        public LayerMask ignoreMask;
    
        private float _nextTimeToFire;
        
        private void Update()
        {
            if (Input.GetButtonDown("Fire1") && _nextTimeToFire <= 0)
            {
                _nextTimeToFire = fireRate;
                Shoot();
            }
            else
            {
                _nextTimeToFire -= Time.deltaTime;
            }
        }

        /// <summary>
        /// Gun shooting logic
        /// </summary>
        private void Shoot()
        {
            PlayMuzzleFlash();
            PlayGunSound();
            
            if (Physics.Raycast(playerCamera.transform.position, 
                              playerCamera.transform.forward,
                                     out var hit, 
                                     range,
                                     ~ignoreMask))
            {
                DamageOther(hit);
                PlayImpactEffect(hit);
                AddForceToRigidBody(hit);
            }
        }

        /// <summary>
        /// Play gun shoot sound
        /// </summary>
        private void PlayGunSound()
        {
            if (audioSource == null) return;
            
            audioSource.Play();
        }

        /// <summary>
        /// Adds force to the rigid body of the raycast hit target
        /// </summary>
        /// <param name="raycastHit">raycast hit target</param>
        private void AddForceToRigidBody(RaycastHit raycastHit)
        {
            if (raycastHit.rigidbody != null)
                raycastHit.rigidbody.AddForce(-raycastHit.normal * impactForce);
        }

        /// <summary>
        /// Plays the particle effect on raycast hit target
        /// </summary>
        /// <param name="raycastHit">raycast hit target</param>
        private void PlayImpactEffect(RaycastHit raycastHit)
        {
            var impactGo = raycastHit.transform.gameObject.layer switch
            {
                6 => ObjectPoolManager.Instance.GetFirst(Constants.BulletHole),
                9 => ObjectPoolManager.Instance.GetFirst(Constants.BloodEffect),
                _ => null
            };
            
            if (impactGo == null) return;
            
            impactGo.transform.position = raycastHit.point;
            impactGo.transform.rotation = Quaternion.LookRotation(raycastHit.normal);
            impactGo.gameObject.SetActive(true);
            
            //Instantiate(impactGo, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
        }

        /// <summary>
        /// Damages raycast hit target
        /// </summary>
        /// <param name="raycastHit"></param>
        private void DamageOther(RaycastHit raycastHit)
        {
            var target = raycastHit.collider.GetComponent<Damageable>();
            if (target)
                target.InflictDamage(damage, gameObject.name);
        }

        /// <summary>
        /// Play muzzle flash particle effect
        /// </summary>
        private void PlayMuzzleFlash()
        {
            if (muzzleFlash == null) return;
            muzzleFlash.SetActive(true);
        }
    }
}