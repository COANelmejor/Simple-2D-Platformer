using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Player movement variables
    public float jumpForce = 6f;
    public float moveSpeed = 5f;
    public LayerMask groundMask;

    Rigidbody2D playerRigidbody;
    Animator animator;


    void Awake() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Jump();
        }
        Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.red);
    }

    // Makes the player jump
    void Jump() {
        if (IsTouchingTheGround()) { 
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Check if the player is touching the ground
    bool IsTouchingTheGround() {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.5f, groundMask)) { 
            return true;
        }

        return false;
    }
}
