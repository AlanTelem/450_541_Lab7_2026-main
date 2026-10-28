// using System.Runtime.ConstrainedExecution;
// using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

// Moves the player with a Rigidbody: 
// A/D or Left/Right arrows to ROTATE, W/S or Up/Down to move Forward/Backward, Space to jump.
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;      // units per second
    [SerializeField] private float turnSpeed = 150f;    // degrees per second
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float jumpForce = 5f;      // strength of the jump
    [SerializeField] private float doubleJumpForce = 4f;
    [SerializeField] private float maxJumpHoldTime = 3f;
    [SerializeField] private float maxJumpMultiplier = 1.5f;

    [Header("Ground Check Settings")]
    [SerializeField] private float groundDistance = 0.5f; // ray length
    [SerializeField] private LayerMask groundMask;        // ground layers

    private Rigidbody rb;               // the physics body we move
    private Vector3 moveDirection;      // worked out from input in Update()
    private float turnInput;            // horizontal input for rotation
    private bool isGrounded;            // true when standing on the ground
    private bool jumpRequested = false; // set in Update(), used later
    private bool doubleJumpRequested = false; // set in TryDoubleJump()
    private bool hasDoubleJumped = false;     // one per jump
    private float jumpHoldTime = 0f;
    private bool isHoldingJump = false;
    private Vector3 spawnPoint1;
    private Quaternion spawnRotation1;
    private float currentSpeed;

    public bool IsGrounded => isGrounded;
    public bool HasDoubleJumpPowerUp { get; set; } = false;
    public float SpeedPowerUpMultiplier { get; set; } = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        spawnPoint1 = rb.transform.position;
        spawnRotation1 = rb.transform.rotation;
    }

    private void Update()
    {
        // 1. Ground Check
        isGrounded = Physics.Raycast(transform.position + transform.up * groundDistance / 2, -transform.up, groundDistance, groundMask);

        // Back on the ground: the double jump is available again
        if (isGrounded)
        {
            hasDoubleJumped = false;
        }


        // 2. Read Inputs
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right turn input
        float moveZ = Input.GetAxisRaw("Vertical");  // W/S or Up/Down forward/backward
        currentSpeed = Input.GetButton("Run") ? moveSpeed * sprintMultiplier : moveSpeed;


        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        // Rotate the player transform in Update for responsive turning visuals
        // Calculate forward movement relative to current facing direction
        moveDirection = cameraForward * moveZ + cameraRight * moveX;

        if (moveDirection.magnitude > 0.0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime);
        }

        // 3. Handle Jump Input
        if (Input.GetButtonDown("Jump") && isGrounded)
            isHoldingJump = true;

        if (Input.GetButton("Jump") && isHoldingJump && isGrounded)
        {
            jumpHoldTime += Time.deltaTime;
            jumpHoldTime = Mathf.Clamp(jumpHoldTime, 0f, maxJumpHoldTime);
            jumpRequested = jumpHoldTime >= maxJumpHoldTime;
        }
        if (isGrounded && isHoldingJump && Input.GetButtonUp("Jump")) jumpRequested = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (jumpRequested)
        {
            Jump(jumpForce, jumpHoldTime);
            isHoldingJump = false;
            jumpRequested = false;
            jumpHoldTime = 0f;
        }

        // NEW: the second jump, in mid-air
        if (doubleJumpRequested)
        {
            Jump(doubleJumpForce);
            doubleJumpRequested = false;
        }
    }

    private void Jump(float force, float chargeTime = 0f)
    {
        float chargePercent = chargeTime / maxJumpHoldTime; //from 0 to 100 percent of how long player could hold jump
        float forceMultiplier = Mathf.Lerp(1f, maxJumpMultiplier, chargePercent); //creates multiplier from 1 to maxJumpMultiplier based on the chargePercent
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f,
                                        rb.linearVelocity.z);
        rb.AddForce(Vector3.up * (force * forceMultiplier), ForceMode.VelocityChange);
    }


    private void MovePlayer()
    {
        // Calculate velocity based on current forward vector
        Vector3 targetVelocity = moveDirection * currentSpeed * SpeedPowerUpMultiplier;

        // Apply movement while preserving vertical velocity (gravity/jumping)
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    // Tries to jump a second time in mid-air.
    // Returns true if the double jump really happened.
    public bool TryDoubleJump()
    {
        // Not allowed on the ground, or if it was already used
        if (!HasDoubleJumpPowerUp || isGrounded || hasDoubleJumped) return false;

        doubleJumpRequested = true;
        hasDoubleJumped = true;
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathPlane"))
        {
            if (ScoreManager.Instance != null) ScoreManager.Instance.RestartLevelScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}