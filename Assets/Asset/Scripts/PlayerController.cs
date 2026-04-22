using UnityEngine;
using UnityEngine.Rendering;

namespace BYK
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform orientationTransform;

        [Header("Movement Settings")]
        [SerializeField] private float MovementSpeed;

        [Header("Jump Settings")]
        [SerializeField] private KeyCode jumpKey;
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCooldown;
        [SerializeField] private bool canJump;

        [Header("Ground Check Settings")]
        [SerializeField] private float playerHeight;
        [SerializeField] private LayerMask groundLayer;


        private Rigidbody playerRigidbody;

        private float horizontalInput, verticalInput;

        private Vector3 MovementDirection;

        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            playerRigidbody.freezeRotation = true;
        }

        private void Update()
        {
            SetInput();
        }

        private void FixedUpdate()
        {
            SetPlayerMovement();
        }

        private void SetInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            
            if (Input.GetKey(jumpKey)&& canJump && IsGrounded())
            {
                canJump = false;
                SetPlayerJumping();
                Invoke(nameof(ResetJumping), jumpCooldown);
            }
        }

        private void SetPlayerMovement()
        {
            MovementDirection = orientationTransform.forward *
                verticalInput + orientationTransform.right * horizontalInput;

            playerRigidbody.AddForce(MovementDirection.normalized * MovementSpeed, ForceMode.Force);
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
    }
}   