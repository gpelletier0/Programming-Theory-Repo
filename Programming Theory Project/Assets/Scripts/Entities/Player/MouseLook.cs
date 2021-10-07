using UnityEngine;

namespace Entities.Player
{
    public class MouseLook : MonoBehaviour
    {
        public float mouseSensitivity = 100.0f;
        public Transform playerBody;
        public Transform playerCamera;
    
        private float _xRotation;
    
        private void LateUpdate()
        {
            var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
            playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}