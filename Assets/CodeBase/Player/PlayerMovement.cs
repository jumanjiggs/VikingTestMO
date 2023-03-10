using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const string MouseX = "Mouse X";
        
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float speed = 5;
        [SerializeField] private float mouseSens = 250f;
        [SerializeField] private PlayerAnimator playerAnimator;

        [Inject]private IInputService _inputService;

        private Vector3 _currentDirection;
        private EventsHolder EventsHolder => EventsHolder.Instance;

        private void Start() => 
            HideCursor();

        private void OnEnable() =>
            EventsHolder.PlayerDie += DisableMovement;
        
        private void OnDisable() => 
            EventsHolder.PlayerDie -= DisableMovement;

        private void Update()
        {
            _currentDirection = GetAxis();
            _currentDirection = UpdateDirection(_currentDirection);
            Move(_currentDirection);
            RotateCamera();

            if (_currentDirection.x == 0 && _currentDirection.z == 0)
                playerAnimator.PlayIdle();
            else
            {
                if (_inputService.Axis.z > 0)
                    playerAnimator.PlayRun();
                else
                    playerAnimator.PlayRunBack();
            }
        }
        private Vector3 GetAxis()
        {
            var direction = new Vector3(_inputService.Axis.x, 0, _inputService.Axis.z);
            return direction;
        }
        private Vector3 UpdateDirection(Vector3 dir)
        {
            dir = transform.TransformDirection(dir);
            dir.y += Physics.gravity.y;
            return dir;
        }
        private void RotateCamera()
        {
            var mouseX = Input.GetAxis(MouseX) * mouseSens * Time.deltaTime;
            transform.Rotate(Vector3.up, mouseX);
        }
        private void Move(Vector3 dir) => characterController.Move(dir * speed * Time.deltaTime);

        private static void HideCursor() =>
            Cursor.lockState = CursorLockMode.Locked;
        private void DisableMovement()
        {
            characterController.enabled = false;
            enabled = false;
        }
    }
}