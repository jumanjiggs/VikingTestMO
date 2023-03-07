using UnityEngine;

namespace CodeBase
{ 
    public enum MovementState
    {
        Walking,
        Air
    }
    public class PlayerMovement : MonoBehaviour
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        
        [SerializeField] private PlayerAnimator playerAnimator;
        [SerializeField] private Transform orientation;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private int rotationSpeed;
        
        [Header("Movement")]
        [SerializeField] private float walkSpeed;
        [SerializeField] private float groundDrag;
        private float _moveSpeed;

        [Header("Ground Check")]
        [SerializeField] private float playerHeight;
        [SerializeField] private LayerMask whatIsGround;
        private bool _grounded;
        
        [Header("Slope Handling")]
        [SerializeField] private float maxSlopeAngle;
        private RaycastHit _slopeHit;
        private bool _exitingSlope;
        private float _horizontalInput;
        private float _verticalInput;
        private bool _inputOn;
        private Vector3 _moveDirection;
        private MovementState _state;

        private void Start()
        {
            HideCursor();
        }
        private void Update()
        {
            GroundCheck();
            MyInput();
            Rotate();
            SpeedControl();
            StateHandler();
            if (_grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }
        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void Rotate() 
        {
            if (Input.GetKey(KeyCode.A)) 
                transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.D)) 
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        private void MyInput()
        {
            _horizontalInput = Input.GetAxisRaw(Horizontal);
            _verticalInput = Input.GetAxisRaw(Vertical);
            _inputOn = _horizontalInput != 0 || _verticalInput != 0;
        }

        private void StateHandler()
        {
            if (_grounded)
            {
                _state = MovementState.Walking;
                _moveSpeed = walkSpeed;
            }
            else
                _state = MovementState.Air;
        }

        private void MovePlayer() 
        {
            _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
            if (OnSlope() && !_exitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection() * _moveSpeed * 20f, ForceMode.Force);
                if (rb.velocity.y > 0)
                    rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
            else if (_grounded) 
                rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
            rb.useGravity = !OnSlope();
            SetAnimations();
        }


        private void SetAnimations()
        {
            if (_inputOn)
            {
                playerAnimator.PlayRun();
                playerAnimator.ResetIdle();
            }
            else
            {
                playerAnimator.PlayIdle();
                playerAnimator.ResetRun();
                ResetVelocity();
            }
        }

        private void SpeedControl()
        {
            if (OnSlope() && !_exitingSlope)
            {
                if (rb.velocity.magnitude > _moveSpeed)
                    rb.velocity = rb.velocity.normalized * _moveSpeed;
            }
            else
            {
                Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                if (flatVel.magnitude > _moveSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized * _moveSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
                }
            }
        }

        private bool OnSlope()
        {
            if(Physics.Raycast(transform.position, Vector3.down, out _slopeHit, playerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }
            return false;
        }

        private Vector3 GetSlopeMoveDirection() =>
            Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal).normalized;
        private void GroundCheck() => 
            _grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        private void ResetVelocity() => 
            rb.velocity = Vector3.zero;
        private static void HideCursor() => 
            Cursor.lockState = CursorLockMode.Locked;
    }
}