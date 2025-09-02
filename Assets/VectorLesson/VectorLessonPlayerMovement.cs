using UnityEngine;

//Makes it so this script always requires a character controller to be in the same object
//[RequireComponent(typeof(CharacterController))]
public class VectorLessonPlayerMovement : MonoBehaviour
{
    /// <summary> The float that will control the speed of the character </summary>
    [SerializeField, Tooltip("Controls the speed of the character")]
    private float speed;
    /// <summary> The variable that controls how fast the player turns </summary>
    [SerializeField]
    private float rotationSpeed;

    /// <summary> An example of the text area tag </summary>
    [SerializeField, TextArea(1, 10)]
    private string exampleText;

    #region Character Controller Variables
    /// <summary> A reference to the Character Controller component attached to this object </summary>
    private CharacterController controller;
    //A vector to keep track of the current player velocity
    private Vector3 velocity;
    //The gravity that we're using
    private float gravity = -9.81f;
    #endregion

    /// <summary> A reference to the rigidbody attached to this object </summary>
    private Rigidbody rb;

    /// <summary>
    /// Called when this object is created or at the start if it's already in the scene
    /// </summary>
    private void Start()
    {
        //Get the Character Controller on this object
        //controller = GetComponent<CharacterController>();

        //Get the rigidbody in this object
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //Movement1();
        //Movement2();
        //Movement3();
        //LocalMovement1();
        LocalVectorMovement1();
        Rotation();
    }

    /// <summary>
    /// Moves the player in the Z axis without checking for things on the way
    /// </summary>
    private void Movement1()
    {
        //Method 1 - Just move forward in Z
        //Cache the current player position
        Vector3 playerPos = transform.position;
        //Add to the z value the speed of the character multiplied by the vertical
        //axis input (S&W) to determine if they should move, then multiply by
        //Time.deltaTime so this is not framerate-dependent
        playerPos.z += speed * Input.GetAxis("Vertical") * Time.deltaTime;
        //Teleport the player to the new playerPos
        transform.position = playerPos;
    }

    /// <summary>
    /// Moves the player in the Z axis using the character controller
    /// </summary>
    private void Movement2()
    {
        //Method 2 - Use the character controller
        //Initialize a local vector3 variable that's (0,0,0)
        Vector3 motion = Vector3.zero;
        //Set the Z to the speed, multiplied by the player input (up or down),
        //and delta time to prevent framerate dependency
        motion.z = speed * Input.GetAxis("Vertical") * Time.deltaTime;
        //Move the controller in the motion given
        controller.Move(motion);

        //We update the velocity

        //We calculate the current vertical velocity of the player by
        //multiplying delta time by the gravity
        velocity.y += Time.deltaTime * gravity;
        //We move the played down using the velocity
        controller.Move(velocity * Time.deltaTime);
    }
    
    /// <summary>
    /// Moves the player in the Z axis using the rigidbody
    /// </summary>
    private void Movement3()
    {
        //We initialize a motion variable to keep track of where the player is moving
        Vector3 motion = Vector3.zero;
        //We set the z value to be equal to the speed, multiplied by the input for 
        //direction, multiplied by delta time to avoid framerate dependency
        motion.z = speed * Input.GetAxis("Vertical") * Time.deltaTime;
        //Move the object to the desired position using the rigidbody
        rb.Move(transform.position + motion, transform.rotation);
    }

    /// <summary>
    /// A version of movement1 that uses the local position instead, so if the parent of
    /// the object is not aligned with the world we can move the player accordingly
    /// </summary>
    private void LocalMovement1()
    {
        //Cache the current player position
        Vector3 playerPos = transform.localPosition;
        //Add to the z value the speed of the character multiplied by the vertical
        //axis input (S&W) to determine if they should move, then multiply by
        //Time.deltaTime so this is not framerate-dependent
        playerPos.z += speed * Input.GetAxis("Vertical") * Time.deltaTime;
        //Teleport the player to the new playerPos
        transform.localPosition = playerPos;
    }

    /// <summary>
    /// A version of movement1 but instead of the global z axis, 
    /// we use the forward vector of the object
    /// </summary>
    private void LocalVectorMovement1()
    {
        //Get the current local player position
        Vector3 playerPos = transform.localPosition;
        //Get the forward vector of the player, this is a unit vector
        //We multiply it by the speed, the input, and delta time to find out how much
        //we should move the player in each axis to have them moved the appropiate
        //direction in their forward vector
        playerPos += transform.forward * speed * Input.GetAxis("Vertical") * Time.deltaTime;
        //Teleport the player to the new playerPos
        transform.localPosition = playerPos;
    }

    //
    private void Rotation()
    {
        //
        transform.Rotate(Vector3.up,
            rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
    }
}
