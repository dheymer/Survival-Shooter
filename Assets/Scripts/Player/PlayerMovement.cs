using UnityEngine;

/**
 * Class to control the player movement
 * */
public class PlayerMovement : MonoBehaviour{
    public float speed = 6f;                // Player movement speed
    private Vector3 movement;               // Player movement
    private Animator playerAnim;            // Player animator controller
    private Rigidbody playerRB;             // Player rigid body
    private int floorMask;                  // The mas corresponding to the scene's floor
    private float camRayLength = 100f;      // Ray Lenght, to check if it collides with the floor

    /**
     * Awake Method
     * */
    private void Awake(){
        // Obtain the reference to the player's animator and rigid body
        playerAnim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        // Obtain the Floor layer's mask, to check where in the floor is the mouse cursor
        floorMask = LayerMask.GetMask("Floor");
    }

    /**
     * Physics Engine Update Method
     * */
    private void FixedUpdate(){
        // Obtain the float values of the movement input keys
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        // Make the player move
        Move(h, v);
        // Update the direction the player is facing
        Turning();
        // Animate the Player depending on it's status
        Animating(h, v);
    }

    /**
     * Movement method
     * */
    void Move(float h, float v) {
        // Set the movement vector, then normalize it and apply the speed to the movement
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        // Apply the movement vector to the player's rigid body
        playerRB.MovePosition(transform.position + movement);
    }

    /**
     * Method to turn the player's facing (and pointing) direction 
     * */
    void Turning() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);         // Ray to the mouse position on the screen
        RaycastHit floorHit;                                                    // The point in the floor where the ray did hit
        // Check if the camRay collided with the floor
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;        // The Vector with the facing direction of the player
            // Prevent that the player looks upwards or downwards
            playerToMouse.y = 0f;
            // And establish the player's rotation vector
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            // Apply the rotation vector to the player
            playerRB.MoveRotation(newRotation);
        }
    }

    /**
     * Method to check the animations
     * */
    void Animating(float h, float v) {
        // Check if the layer is not moving
        bool idle = ((h == 0) && (v == 0));
        // Assign the status to the animator controller
        playerAnim.SetBool("IsWalking", !idle);
    }
}
