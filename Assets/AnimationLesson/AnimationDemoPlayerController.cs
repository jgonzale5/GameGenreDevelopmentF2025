using UnityEngine;

public class AnimationDemoPlayerController : MonoBehaviour
{
    //A reference to the animator that controls the animations of the player character
    public Animator animator;

    private Vector3 movementInput = Vector3.zero;
    private int HP = 100;

    // Update is called once per frame
    void Update()
    {
        //Gets the player input and stores it in the movement input vector
        if (Input.GetKeyDown(KeyCode.D))
            movementInput.x = 1;
        else if (Input.GetKeyDown(KeyCode.A))
            movementInput.x = -1;
        else if (Input.GetKey(KeyCode.D) == false && 
            Input.GetKey(KeyCode.A) == false) //We only set the movement x to 0 if neither key is being pressed
        {
            movementInput.x = 0;
        }

        //Sets the float "Velocity" in the referenced animator to the current value of the movement input
        animator.SetFloat("Velocity", movementInput.x);

        if (Input.GetKeyDown(KeyCode.X))
        {
            HP = 0;
        }

        animator.SetInteger("HP", HP);
    }
}
