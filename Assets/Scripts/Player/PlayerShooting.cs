using UnityEngine;

/**
 * Class that controls the player shooting
 * */
public class PlayerShooting : MonoBehaviour{
    // Public variables
    public int damagePerShot = 20;                      // The damage done per shot
    public float timeBetweenBullets = 0.15f;            // The time between shoots
    public float range = 100f;                          // The maximum reach of the shoots

    // Public variables
    float timer;                                        // Timer to check if the player can shoot again
    Ray shootRay = new Ray();                           // The player shoot's Ray
    RaycastHit shootHit;                                // The object which the bullet collides with
    int shootableMask;                                  // Colliders objects
    ParticleSystem gunParticles;                        // Gun particles
    LineRenderer gunLine;                               // Gun line renderer
    AudioSource gunAudio;                               // Gun Audio source
    Light gunLight;                                     // Gun Light
    float effectsDisplayTime = 0.2f;                    // Duration of the shoot effect

    /**
     * Initialization Method
     * */
    void Awake (){
        // Obtain the scene shootable objects
        shootableMask = LayerMask.GetMask ("Shootable");
        // Obtain the shooting effect elements
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }

    /**
     * Graphics Engine Update Method
     * */
    void Update (){
        // Increase the timer
        timer += Time.deltaTime;
        // Check if the firing button is pressed and is time to shoot
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0){
            // then shoot
            Shoot ();
        }
        // If the time between shoots and the shooting effects duration has passed
        if(timer >= timeBetweenBullets * effectsDisplayTime){
            // Disable the effects
            DisableEffects ();
        }
    }

    /**
     * Method that disables the shooting effects
     * */
    public void DisableEffects (){
        //Disable the shoot line
        gunLine.enabled = false;
        // Disable the shoot light
        gunLight.enabled = false;
    }

    /**
     * Method that makes the shoot
     * */
    void Shoot (){
        // Init the timer to 0
        timer = 0f;
        // Play the shoot audio
        gunAudio.Play ();
        // Enable the light
        gunLight.enabled = true;
        // Play the particles effect
        gunParticles.Stop ();
        gunParticles.Play ();
        // Enable the shoot line and set it's position at the end of the gun barrel
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);
        // Set the origin of the ray at the barrel's end, and project it forward
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        // Check if the shoot hits something
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask)){
            // Obtain the enemy health of the object shot
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            // If the object shot is an enemy
            if(enemyHealth != null){
                // Make the enemy take damage
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            // Draw the shoot line from the gun to the object
            gunLine.SetPosition (1, shootHit.point);
        }else{
            // If the gun hits nothing, draw the shoot line from the gun to the range units forward
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
