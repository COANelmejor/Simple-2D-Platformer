using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Player movement variables
    public float jumpForce = 6f;
    public float moveSpeed = 5f;
    public LayerMask groundMask;
    public float groundRayCast = 1.5f;

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    Rigidbody2D playerRigidbody;
    Animator animator;


    void Awake() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Jump();
        }
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        Debug.DrawRay(transform.position, Vector2.down * groundRayCast, Color.red);
    }

    // Makes the player jump
    void Jump() {
        if (IsTouchingTheGround()) { 
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Check if the player is touching the ground
    bool IsTouchingTheGround() {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, groundRayCast, groundMask)) { 
            return true;
        } else {
            return false;
        }
    }
}
