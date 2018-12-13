using UnityEngine;

/**
 * Class that makes the camera follow the player
 * */
public class CameraFollow : MonoBehaviour {

    public Transform target;                // The reference to the followed object (player)
    public float smoothing = 5f;            // The smooth following level of the camera

    private Vector3 offset;                 // The separation between the player and the camera

    /**
     * The starting method
     * */
    void Start(){
        // Initialize the offset, assigning the distance between the camera and the player
        offset = transform.position - target.position;
    }

    /**
     * Physics Engine Update Method
     * */
    void FixedUpdate(){
        // Obtain the position to place the camera
        Vector3 targetCamPos = target.position + offset;
        // Update the camera position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
