using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform orientationTransform;

    [SerializeField] private float MovementSpeed;

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
    }

    private void SetPlayerMovement()
    {
        MovementDirection = orientationTransform.forward * 
            verticalInput + orientationTransform.right * horizontalInput;

        playerRigidbody.AddForce(MovementDirection * MovementSpeed, ForceMode.Force);
    }
}
