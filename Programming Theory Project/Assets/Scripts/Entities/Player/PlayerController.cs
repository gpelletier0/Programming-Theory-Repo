using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(CharacterController), typeof(AudioSource))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Controls")] 
        public float speed = 8f;
        public float gravity = -20f;
        public float jumpHeight = 2f;

        [Header("Ground Controls")] 
        public float groundDistance = 1f;
        public LayerMask groundMask;

        private Vector3 _velocity;
        private CharacterController _controller;
        private AudioSource[] _audioSources;
        private bool _isGrounded;
        private bool _isMoving;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            _controller = GetComponent<CharacterController>();
            _audioSources = GetComponents<AudioSource>();
        }

        private void Update()
        {
            _isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            _controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                _audioSources[1].Play();
            }
            
            if (_isGrounded && _velocity.y < 0)
                _velocity.y = -2f;
            
            _velocity.y += gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);

            PlayWalkingSound();
        }

        /// <summary>
        /// Play walking sound if player is moving and on the ground
        /// </summary>
        private void PlayWalkingSound()
        {
            
            _isMoving = (Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") > 0) ||
                        (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) && 
                        _isGrounded;

            if (_isMoving && !_audioSources[0].isPlaying)
                _audioSources[0].Play();
            if (!_isMoving)
                _audioSources[0].Stop();
        }

        /// <summary>
        /// Draws Grounded gizmo
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            Gizmos.color = _isGrounded ? transparentGreen : transparentRed;
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundDistance, transform.position.z), 0.2f);
        }
    }
}