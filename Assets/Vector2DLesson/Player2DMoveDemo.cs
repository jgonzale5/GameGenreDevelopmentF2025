using UnityEngine;

public class Player2DMoveDemo : MonoBehaviour
{
    /// <summary>
    /// The speed of this object when it moves, in units per second
    /// </summary>
    [SerializeField]
    private float speed;
    /// <summary>
    /// The force applied to the player when jumping
    /// </summary>
    [SerializeField]
    private float jumpForce;

    /// <summary>
    /// The start position of the center groundCheck raycast in respect to this transform, in local-space
    /// </summary>
    [SerializeField]
    private Vector2 groundCheckOffset;
    /// <summary>
    /// How long is the groundCheck raycast
    /// </summary>
    [SerializeField]
    private float groundCheckDistance;
    /// <summary>
    /// How many ground check raycasts will we use
    /// </summary>
    [SerializeField, Range(1, 100)]
    private int groundCheckCount = 1;
    /// <summary>
    /// How much space is between each raycast, if we have more than one
    /// </summary>
    [SerializeField]
    private float groundCheckStep;

    /// <summary>
    /// A reference to the rigidbody that controls the physics of this object 
    /// </summary>
    private Rigidbody2D rb;
    /// <summary>
    /// The internal buffer for the player input
    /// </summary>
    private Vector2 input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        RefreshInputBuffer();

        //Movement();
        MovementVelocity();

        //If the player presses the jump button, jump
        if (CheckAllGrounded() && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    /// <summary>
    /// Updates the internal vector function that keeps track of the player input
    /// </summary>
    private void RefreshInputBuffer()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            input.x = 1;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            input.x = -1;
        }
        else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            input.x = 0;
        }
    }

    /// <summary>
    /// Moves the player using the MovePosition function of rigidbody2d
    /// </summary>
    private void Movement()
    {
        //The motion that the player is taking this frame
        Vector2 motion = Vector2.zero;
        //The motion in the x axis is determined by the input, speed, and delta time
        motion.x = input.x * speed * Time.deltaTime;
        //We move the player using the rigidbody, casting the player position to Vector2
        //to allow for operation between them
        rb.MovePosition((Vector2)transform.position
            + motion);
    }

    /// <summary>
    /// Moves the player by modifying their velocity
    /// </summary>
    private void MovementVelocity()
    {
        //We initialize an internal velocity variable to determine what the final one should be
        Vector2 velocity = rb.linearVelocity;
        //The velocity in X is determined by the input and the speed. 
        //Input determines direction, speed is self explanatory
        velocity.x = input.x * speed;
        //We set the linear velocity of the object to the new velocity value
        rb.linearVelocity = velocity;
    }

    /// <summary>
    /// Makes the player jump by adding forces to them
    /// </summary>
    private void Jump()
    {
        //We apply force to the rigidbody vertically
        rb.AddForce(transform.up * jumpForce);
    }

    /// <summary>
    /// Checks if the player is grounded by shooting raycast(s) down until it hits something
    /// </summary>
    /// <returns>Whether the raycasts hit something</returns>
    private bool CheckGrounded()
    {


        //If we can draw a raycast between the offset, pointing down, for the specified distance, and hit something
        //then we can jump
        if (Physics2D.Raycast(
            transform.position + (Vector3)groundCheckOffset,
            -transform.up,
            groundCheckDistance))
            {

            Debug.DrawRay(
                    transform.position + (Vector3)groundCheckOffset,
                    -transform.up * groundCheckDistance,
                    Color.red);

            return true;
        }

        Debug.DrawRay(
                transform.position + (Vector3)groundCheckOffset,
                -transform.up * groundCheckDistance,
                Color.blue);

        //Otherwise we can't
        return false;
    }

    /// <summary>
    /// Casts a number of groundChecks equal to GroundCheckCount, GroundCheckStep appart, and returns true
    /// if at least one of them returns true
    /// </summary>
    /// <returns>Whether at least one of the ground checks hit something.</returns>
    private bool CheckAllGrounded()
    {
        //The initial position is what we would use if we only had one raycast
        Vector2 initialPosition = (Vector2)transform.position + groundCheckOffset;

        //We move the initial position based on the amoung of ground checks we're doing
        initialPosition.x -= (groundCheckStep / 2) * (groundCheckCount - 1);

        bool atLeastOneHit = false;

        //For each ground check we're checking
        for (int i =0; i < groundCheckCount; i++)
        {
            //We stablish the position of the ray, starting at the initial position
            Vector2 pos = initialPosition;
            //The position on x changes based on which ray we're working with
            pos.x += groundCheckStep * i;

            //If the current raycast hits something
            if (Physics2D.Raycast(pos, -transform.up, groundCheckDistance))
            {
                //If we hit something, we draw a red ray
                Debug.DrawRay(
                        pos,
                        -transform.up * groundCheckDistance,
                        Color.red);

                ////We return true
                //return true;

                atLeastOneHit = true;
            }
            else
            {
                //If we didn't hit anything, we draw a blue ray
                Debug.DrawRay(
                        pos,
                        -transform.up * groundCheckDistance,
                        Color.blue);
            }
        }

        return atLeastOneHit;
    }
}
