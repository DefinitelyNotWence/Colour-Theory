using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject finishUI;
    public UIController uiController;

    public ChangeColour colour;

    public Camera cam;

    private bool inTrigger;

    private Rigidbody2D rb;
    private TrailRenderer tr;

    private float restartX;
    private float restartY;
    private float minY;

    public int level;
    public int failCounter = 0;
    public int collectables = 0;
    public bool diamond = false;
    public bool coin = false;
    public bool clover = false;
    public bool mushroom = false;

    public Animator BodyAnim;
    public Animator EyesAnim;

    public float terminalVelocity;
    public float moveSpeed = 5f;
    public float jumpForce;
    public float dirX;

    private bool facingRight = true;

    public bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;
    
    // These are variables, the different types are boolean for true or false, integer for whole numbers, and float for decimal numbers
    
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    
    public LayerMask platformLayer;

    private bool isWallSliding;
    public float wallSlidingSpeed;
    public Transform wallCheck;

    private bool isWallJumping;
    private float wallJumpingDirection;
    [SerializeField] private float wallJumpingTime;
    private float wallJumpingCounter;
    [SerializeField] private float wallJumpingDuration;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    private float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;

    // Start is called before the first frame update
    private void Start()
    {
        level = 1;
        gameUI.SetActive(true);
        finishUI.SetActive(false);
        inTrigger = false;
        restartX = 0;
        restartY = 0;
        minY = -8;
        colour.colour = 1;
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
    }

    // FixedUpdate is called independently of the framerate at fixed intervals relative to the unity physics engine
    private void FixedUpdate()
    {
        if (isDashing == true)
        {
            return;
        }


        if (!isWallJumping)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            Flip();
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (transform.position.y < minY) // This is an if statement which will run the code inside if the condition(s) are met
        {
            RestartLevel(); // This is an example of calling another function within a function
        }

        if (isDashing == true)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.W) && inTrigger == true)
        {
            NextLevel();
        }

        if (isGrounded == false && isWallSliding == false)
        {
            BodyAnim.SetBool("jumping", true); // This is where I control the parameters in the animator to make an animation transition to another
            EyesAnim.SetBool("jumping", true);
        }
        else
        {
            BodyAnim.SetBool("jumping", false);
            EyesAnim.SetBool("jumping", false);
        }

        Jump();
        WallSlide();
        WallJump();
        SenseDash();
    }

    private void OnTriggerEnter2D(Collider2D other) // This function checks when the GameObject with this script attached to it has collided with another box collider that is set to be a trigger
    {
        if (other.gameObject.tag == "Door")
        {
            inTrigger = true;
        }
        else if (other.gameObject.tag == "Diamond") // An else if statement only runs when the previous if or else if statement's condition(s) are not met
        {
            Destroy(other.gameObject);
            collectables += 1;
            diamond = true;
        }
        else if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            collectables += 1;
            coin = true;
        }
        else if (other.gameObject.tag == "Clover")
        {
            Destroy(other.gameObject);
            collectables += 1;
            clover = true;
        }
        else if (other.gameObject.tag == "Mushroom")
        {
            Destroy(other.gameObject);
            collectables += 1;
            mushroom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) // This function checks whether the gameobject has stopped colliding with another box collider that is set to be a trigger
    {
        if (other.gameObject.tag == "Door")
        {
            inTrigger = false;
        }
    }

    // This function will run when you die, it just resets your position to the starting position of the level
    private void RestartLevel() 
    {
        gameObject.transform.position = new Vector2(restartX, restartY);
        cam.transform.position = new Vector2(restartX, restartY);
        if (level == 1)
        {
            colour.colour = 1;
        }
        else if (level == 2)
        {
            colour.colour = 1;
        }
        else if (level == 3)
        {
            colour.colour = 1;
        }
        else if (level == 4)
        {
            colour.colour = 4;
        }
        else if (level == 5)
        {
            colour.colour = 3;
        }
        else if (level == 6)
        {
            colour.colour = 2;
        }
        else if (level == 7)
        {
            colour.colour = 2;
        }
        else if (level == 8)
        {
            colour.colour = 4;
        }
        else if (level == 9)
        {
            colour.colour = 1;
        }
        else if (level == 10)
        {
            colour.colour = 3;
        }
        else if (level == 11)
        {
            colour.colour = 4;
        }
        else if (level == 12)
        {
            colour.colour = 2;
        }

        failCounter += 1;
    }

    // This function changes the starting position and colour of the player and also changes the minimum y coordinate to correspond with the new position of the level
    private void NextLevel()
    {
        int previousLevel = level;
        level += 1;
        if (previousLevel == 1)
        {
            minY = -48;
            restartX = 0;
            restartY = -40;
            colour.colour = 1;
            gameObject.transform.position = new Vector2(restartX, restartY);
            cam.transform.position = new Vector2(restartX, restartY);
            
        }
        else if (previousLevel == 2)
        {
            minY = -8;
            restartX = 80;
            restartY = 0;
            colour.colour = 1;
            gameObject.transform.position = new Vector2(restartX, restartY);
            cam.transform.position = new Vector2(restartX, restartY);
        }
        else if (previousLevel == 3)
        {
            minY = -48;
            restartX = 80;
            restartY = -30;
            colour.colour = 4;
            gameObject.transform.position = new Vector2(restartX, restartY);
            cam.transform.position = new Vector2(restartX, restartY);
        }
        else if (previousLevel == 4)
        {
            minY = -8;
            restartX = 200;
            restartY = 4;
            colour.colour = 3;
            gameObject.transform.position = new Vector2(restartX, restartY);
            cam.transform.position = new Vector2(restartX, restartY);
        }
        else if (previousLevel == 5)
        {
            minY = -48;
            restartX = 200;
            restartY = -40;
            colour.colour = 2;
            gameObject.transform.position = new Vector2(restartX, restartY);
            cam.transform.position = new Vector2(restartX, restartY);
        }
        else if (previousLevel == 6)
        {
            minY = -128;
            restartX = 0;
            restartY = -120;
            colour.colour = 2;
            gameObject.transform.position = new Vector2(restartX, restartY);
            cam.transform.position = new Vector2(restartX, restartY);
        }
        else if (previousLevel == 7)
        {
            minY = -128;
            restartX = 80;
            restartY = -120;
            colour.colour = 4;
            gameObject.transform.position = new Vector2(restartX, restartY);
            cam.transform.position = new Vector2(restartX, restartY);
        }
        else if (previousLevel == 8)
        {
            minY = -130;
            restartX = 200;
            restartY = -100;
            colour.colour = 1;
            gameObject.transform.position = new Vector2(restartX, restartY);
            cam.transform.position = new Vector2(restartX, restartY);
        }
        else if (previousLevel == 9)
        {
            minY = -208;
            restartX = 0;
            restartY = -200;
            colour.colour = 3;
            gameObject.transform.position = new Vector2(restartX, restartY);
            cam.transform.position = new Vector2(restartX, restartY);
        }
        else if (previousLevel == 10)
        {
            minY = -208;
            restartX = 80;
            restartY = -200;
            colour.colour = 4;
            gameObject.transform.position = new Vector2(restartX, restartY);
            cam.transform.position = new Vector2(restartX, restartY);
        }
        else if (previousLevel == 11)
        {
            minY = -208;
            restartX = 200;
            restartY = -200;
            colour.colour = 2;
            gameObject.transform.position = new Vector2(restartX, restartY);
            cam.transform.position = new Vector2(restartX, restartY);
        }
        else if (previousLevel == 12)
        {
            FinishGame();
            Destroy(gameObject);
        }
    }

    // This function disables the in game ui and enables the end screen ui which includes your stats, collectables, and the restart button
    private void FinishGame()
    {
        gameUI.SetActive(false);
        finishUI.SetActive(true);
        uiController.GameFinish();
    }

    // This function controls the jump functionality of the player
    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, platformLayer);

        if (isGrounded == true && Input.GetButtonDown("Jump")) // Input.GetButtonDown checks if the set hotkey for the specified function has been pushed down once
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping == true) // Input.GetButton checks if the set hotkey for the speicifed function is being held down
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }

        if (Input.GetButtonUp("Jump")) // Input.GetButtonUp checks if the set hotkey for the specified function has been released
        {
            isJumping = false;
        }
    }

    // This function controls the physics for when the player is not on the ground and is in contact with the side of the wall
    private void WallSlide()
    {
        isWallSliding = Physics2D.OverlapCircle(wallCheck.position, checkRadius, platformLayer);

        if (rb.velocity.y <= 0 && isWallSliding == true && isGrounded == false)
        {
            
            rb.velocity = new Vector2(0, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    // This function controls the jumping functionality for when the player is making contact with the side of a wall
    private void WallJump()
    {
        if (isWallSliding == true)
        {
            BodyAnim.SetBool("jumping", false);
            EyesAnim.SetBool("jumping", false);
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            BodyAnim.SetBool("jumping", true);
            EyesAnim.SetBool("jumping", true);
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                facingRight = !facingRight;
                Vector2 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    // This function stops the wall jump function and changes the animation cycle 
    private void StopWallJumping()
    {
        BodyAnim.SetBool("jumping", false);
        EyesAnim.SetBool("jumping", false);
        isWallJumping = false;
    }

    // This function flips the player script horizontally to change the direction it is facing
    private void Flip()
    {
        if (dirX > 0 && facingRight == false)
        {
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            facingRight = true;
        }
        else if (dirX < 0 && facingRight == true)
        {
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            facingRight = false;
        }
    }
    
    // This function checks if you have pressed the dash button (E) and starts the IEnumerator Dash
    private void SenseDash()
    {
        if (Input.GetButtonDown("Dash") && canDash == true)
        {
            StartCoroutine(Dash());
        }
    }
    
    // This is an IEnumerator which allows me to add time delays which is useful for controlling the duration of the dash
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingTime);
        canDash = true;
    }
}
