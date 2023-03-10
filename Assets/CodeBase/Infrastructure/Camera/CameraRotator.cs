using UnityEngine;

namespace CodeBase.Infrastructure.Camera
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private float speed;
        private void Update() => 
            RotateCamera();
        private void RotateCamera() => 
            transform.Rotate(0f,speed * Time.deltaTime,0);
    }
}
