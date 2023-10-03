using UnityEngine;

public class WaypointMovementWithDoubleJumpAndObstacle : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints.
    public float moveSpeed = 1.0f; // Movement speed.
    public float jumpForce = 10.0f; // Jump force.
    public float jumpCooldown = 1.0f; // Cooldown between jumps in seconds.
    public float maxSpeed = 10.0f; // Maximum movement speed.
    public float speedIncreaseAmount = 2.0f; // Amount to increase speed by.
    public float speedDecreaseAmount = 2.0f;
    public LayerMask obstacleLayer; // Layer mask to detect obstacles.
    public Transform startingPosition; // Starting position of the object.

    public float rotationSpeed = 5.0f;
    private int currentWaypoint = 0;
    private bool gameStarted = false;
    private Rigidbody rb;
    private bool isGrounded = true;
    private int jumpsRemaining = 2;
    private float lastJumpTime = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!gameStarted)
        {
            // Check for the "Right Arrow" key to start the game.
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameStarted = true;
            }
        }
        else if (currentWaypoint < waypoints.Length)
        {
            // Calculate the direction to the current waypoint.
            Vector3 targetPosition = waypoints[currentWaypoint].position;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveSpeed = Mathf.Min(moveSpeed + speedIncreaseAmount, maxSpeed);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveSpeed -= 2.0f;
            }
            moveSpeed = Mathf.Max(moveSpeed, 0.0f);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypoint++;
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 0.1f, obstacleLayer))
            {
                transform.position = startingPosition.position;
                currentWaypoint = 0;
                moveSpeed = 0f;
            }
        }
        else
        {
        }
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpsRemaining > 0) && Time.time - lastJumpTime >= jumpCooldown)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Apply an upward force to simulate jumping.
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        jumpsRemaining--;
        lastJumpTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsRemaining = 2;
        }
    }
}