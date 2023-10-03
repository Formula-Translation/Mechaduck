using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    // public float speed = 5.0f;
    // public float jumpForce = 5;
    public float jumpForce = 80.0f;           // The force applied for jumping.
    public float groundCheckDistance = 0.3f; // Distance to check if the player is grounded.
    private bool isGrounded;                 // Is the player on the ground?

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    void Update()
    {
        // Move the character forward
        // transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Check for jump input (Spacebar)
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Apply an upward force to simulate jumping
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Implement OnCollisionEnter to detect when the character is grounded
    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //     }
    // }

}
