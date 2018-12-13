using UnityEngine;

/**
 * Class that manages the enemy's health
 * */
public class EnemyHealth : MonoBehaviour{
    // Public variables
    public int startingHealth = 100;                        // The enemy's starting health
    public int currentHealth;                               // Enemy's current health
    public float sinkSpeed = 2.5f;                          // Enemy's sinking speed in the floor when killed
    public int scoreValue = 10;                             // Score value when the enemy is killed
    public AudioClip deathClip;                             // Enemy's death sound

    // Private variables
    Animator anim;                                          // Enemy's animator
    AudioSource enemyAudio;                                 // Enemy's Audio Source
    ParticleSystem hitParticles;                            // Enemy's smoke when hit
    CapsuleCollider capsuleCollider;                        // Enemy's collider
    bool isDead;                                            // Enemy's death trigger
    bool isSinking;                                         // Trigger that indicates that the enemy is sinking in the floor

    /**
     * Initialization Method
     * */
    void Awake (){
        // Obtain the references to the enemy's animator, audio source, particles and collider
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        // Initialize the enemy's health with the sarting health
        currentHealth = startingHealth;
    }

    /**
     * Graphics Engine Update Method
     * */
    void Update (){
        // Check if the enemy is sinking
        if(isSinking){
            // If so, keep moving the enemy down in the floor
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    /**
     * Method that makes the enemy to take damage
     * */
    public void TakeDamage (int amount, Vector3 hitPoint){
        // if the enemy is dead, finish the method
        if(isDead)
            return;
        // Otherwise, play the hurting audio
        enemyAudio.Play ();
        // Decrease the enemy's health
        currentHealth -= amount;
        // Create some particles at the enemy's position
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        // Check if the enemy's health reaches 0
        if(currentHealth <= 0){
            // If so, make the enemy die
            Death ();
        }
    }

    /**
     * Method that makes the enemy die
     * */
    void Death (){
        // Activate the isDead trigger
        isDead = true;
        // Transform the enemy collider to a trigger
        capsuleCollider.isTrigger = true;
        // Calls the enemy's death animation
        anim.SetTrigger ("Dead");
        // Set the enemy's dead audio clip and play it
        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }

    /**
     * Method that sinks the enemy in the floor
     * */
    public void StartSinking (){
        // Make the enemy stop following the player
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        // Set the enemy's rigid body to kinematic
        GetComponent <Rigidbody> ().isKinematic = true;
        // Set the singing trigger to true
        isSinking = true;
        // Updates the score
        ScoreManager.score += scoreValue;
        // Destroys the game object
        Destroy (gameObject, 2f);
    }
}
