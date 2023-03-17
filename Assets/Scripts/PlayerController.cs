using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 6f;
    public float walkingSpeed = 2f;
    public float runSpeedMultiplier = 2f;
           float speedMultiplier = 1f;
    [Range(0f,1f)]
    public float onAirMovementDecrement = 0.5f;
    public LayerMask groundMask;
    public float groundRayCast = 1.5f;
    public bool isOnTheGround = true;
    public bool isWalking = false;
    public float idleVelocity = 0.15f;

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string STATE_WALKING = "isWalking";
    const string STATE_RUNNING = "isRunning";

    Rigidbody2D playerRigidbody;
    Animator playerAnimator;

    void Awake() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() {
        playerAnimator.SetBool(STATE_ALIVE, true);
        playerAnimator.SetBool(STATE_ON_THE_GROUND, true);
        playerAnimator.SetBool(STATE_WALKING, false);
    }

    // Update is called once per frame
    void Update() {
        isOnTheGround = IsTouchingTheGround();
        isWalking = IsWalking();
        playerAnimator.SetBool(STATE_ON_THE_GROUND, isOnTheGround);
        playerAnimator.SetBool(STATE_WALKING, isWalking);
        Debug.DrawRay(transform.position, Vector2.down * groundRayCast, Color.red);
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            playerAnimator.SetBool(STATE_RUNNING, true);  
            speedMultiplier = runSpeedMultiplier;
        } else {
            playerAnimator.SetBool(STATE_RUNNING, false);
            speedMultiplier = 1f;
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            MoveRight();
        }
    }

    // Check if the player is touching the ground
    bool IsTouchingTheGround() {
        GameManager.sharedInstance.currentGameState = GameState.inGame;
        if (Physics2D.Raycast(this.transform.position, Vector2.down, groundRayCast, groundMask)) {
            return true;
        } else {
            return false;
        }
    }

    bool IsWalking() {
        if (playerRigidbody.velocity.x > idleVelocity || playerRigidbody.velocity.x < -idleVelocity) {
            return true;
        } else {
            return false;
        }
    }
    private void MoveLeft() {
        transform.localScale = new Vector3(-1, 1, 1);
        if (isOnTheGround) {
            playerRigidbody.velocity = new Vector2(-walkingSpeed * speedMultiplier, playerRigidbody.velocity.y);
        } else {
            if (playerRigidbody.velocity.x > 0) {
                playerRigidbody.AddForce(Vector2.left * onAirMovementDecrement, ForceMode2D.Impulse);
            } else if (playerRigidbody.velocity.x == 0) {
                playerRigidbody.velocity = new Vector2(-walkingSpeed * onAirMovementDecrement, playerRigidbody.velocity.y);
            } 
        }
    }

    private void MoveRight() {
        transform.localScale = new Vector3(1, 1, 1);
        if (isOnTheGround) {
            playerRigidbody.velocity = new Vector2(walkingSpeed * speedMultiplier, playerRigidbody.velocity.y);
        } else {
            if (playerRigidbody.velocity.x < 0) {
                playerRigidbody.AddForce(Vector2.right * onAirMovementDecrement, ForceMode2D.Impulse);
            } else if (playerRigidbody.velocity.x == 0) {
                playerRigidbody.velocity = new Vector2(walkingSpeed * onAirMovementDecrement, playerRigidbody.velocity.y);
            }
        }
    }

    // Makes the player jump
    void Jump() {
        if (isOnTheGround) {
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
