using UnityEngine;
using UnityEngine.Rendering;

namespace BYK
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform orientationTransform;

        [Header("Movement Settings")]
        [SerializeField] private KeyCode movementKey;
        [SerializeField] private float MovementSpeed;

        [Header("Jump Settings")]
        [SerializeField] private KeyCode jumpKey;
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCooldown;
        [SerializeField] private float airMultiplier;
        [SerializeField] private float airDrag;
        [SerializeField] private bool canJump;

        [Header("Slide Settings")]
        [SerializeField] private KeyCode slideKey;
        [SerializeField] private float slideMultiplier;
        [SerializeField] private float slideDrag;

        [Header("Ground Check Settings")]
        [SerializeField] private float playerHeight;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundDrag;

        private StateController stateController;

        private Rigidbody playerRigidbody;

        private float horizontalInput, verticalInput;

        private Vector3 MovementDirection;

        private bool isSliding;

        private void Awake()
        {
            stateController = GetComponent<StateController>();
            playerRigidbody = GetComponent<Rigidbody>();
            playerRigidbody.freezeRotation = true;
        }

        private void Update()
        {
            SetInput();
            SetStates();
            SetPlayerDrag();
            LimitPlayerSpeed(); 
        }

        private void FixedUpdate()
        {
            SetPlayerMovement();
        }

        private void SetInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(slideKey))
            {
                isSliding = true;
            }
            else if (Input.GetKeyDown(movementKey))
            {  
                isSliding = false;
            }
            else if (Input.GetKey(jumpKey) && canJump && IsGrounded())
            {
                canJump = false;
                SetPlayerJumping();
                Invoke(nameof(ResetJumping), jumpCooldown);
            }
        }

        private void SetStates()
        {
            var MovementDirection = GetMovementDirection();
            var isGrouded = IsGrounded();
            var isSliding = IsSliding();
            var currentState = stateController.GetCurrentState();

            var newState = currentState switch
            {
                _ when MovementDirection == Vector3.zero && isGrouded && !isSliding => PlayerState.Idle,
                _ when MovementDirection != Vector3.zero && isGrouded && !isSliding => PlayerState.Move,
                _ when MovementDirection != Vector3.zero && isGrouded && isSliding => PlayerState.Slide,
                _ when MovementDirection == Vector3.zero && isGrouded && isSliding => PlayerState.SlideIdle,
                _ when !canJump && !isGrouded => PlayerState.Jump,
                _ => currentState
            };
            if (newState != currentState)
            {
                stateController.ChangeState(newState);
            } 
        }

        private void SetPlayerMovement()
        {
            MovementDirection = orientationTransform.forward *
                verticalInput + orientationTransform.right * horizontalInput;

            float forceMultiplier = stateController.GetCurrentState() switch
            {
                PlayerState.Move => 1f,
                PlayerState.Slide => slideMultiplier,
                PlayerState.Jump => airMultiplier,
                _ => 1f
            };

            playerRigidbody.AddForce(MovementDirection.normalized * MovementSpeed * forceMultiplier, ForceMode.Force);
        }

        private void SetPlayerDrag()
        {
            playerRigidbody.linearDamping = stateController.GetCurrentState() switch
            {
                PlayerState.Move => groundDrag,
                PlayerState.Slide => slideDrag,
                PlayerState.Jump => airDrag,
                _ => playerRigidbody.linearDamping
            };
        }

        private void LimitPlayerSpeed()
        {
            Vector3 flatVelocity = new Vector3(playerRigidbody.linearVelocity.x,0f, playerRigidbody.linearVelocity.z);
            
            if (flatVelocity.magnitude > MovementSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * MovementSpeed;
                playerRigidbody.linearVelocity = new Vector3
                    (limitedVelocity.x, playerRigidbody.linearVelocity.y, limitedVelocity.z);
            }
        }

        private void SetPlayerJumping()
        {
            playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
            playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJumping()
        {
            canJump = true;
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        }

        private Vector3 GetMovementDirection()
        {
            return MovementDirection.normalized;
        }

        private bool IsSliding()
        {
            return isSliding;
        }
    }
}   