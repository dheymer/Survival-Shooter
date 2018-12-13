using UnityEngine;

/**
 * Class that makes the enemy attack the player
 * */
public class EnemyAttack : MonoBehaviour{
    public float timeBetweenAttacks = 0.5f;                     // Time between attacks
    public int attackDamage = 10;                               // Damage done to the player with each attack


    Animator anim;                                              // Enemy animator controller
    GameObject player;                                          // The player's game object
    PlayerHealth playerHealth;                                  // The player's health
    EnemyHealth enemyHealth;                                    // The enemy's health
    bool playerInRange;                                         // Trigger that indicates the player is in range
    float timer;                                                // Timer to check the attack interval

    /**
     * Initialization method
     * */
    void Awake (){
        // Obtain the player's and player's health references, along with the enemy's health
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        // Get the enemy's animator
        anim = GetComponent <Animator> ();
    }

    /**
     * Method that checks if the player is in the attack range of the enemy
     * */
    void OnTriggerEnter (Collider other){
        // If the object in range is the player
        if(other.gameObject == player){
            // Set the InRange trigger to true
            playerInRange = true;
        }
    }

    /**
     * Method that checks if the player leaves the attack range of the enemy
     * */
    void OnTriggerExit (Collider other){
        // If the object that leaves the range is the player
        if(other.gameObject == player){
            // Set the InRange trigger to false
            playerInRange = false;
        }
    }

    /**
     * Graphics Engine Update Method
     * */
    void Update (){
        // Increase the timer with the update interval
        timer += Time.deltaTime;
        // Check if the timer between attacks is reached and if the player is in the attack range, and if the enemy still lives
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0){
            // If so, attack the player
            Attack ();
        }
        // Check if the player's health reaches 0
        if(playerHealth.currentHealth <= 0){
            // If so, set the PlayerDead trigger to true, so the enemy can go to idle status
            anim.SetTrigger ("PlayerDead");
        }
    }

    /**
     * Method in which the enemy attacks the player
     * */
    void Attack (){
        // Set the timer to 0
        timer = 0f;
        // Check if the player still have health
        if(playerHealth.currentHealth > 0){
            // If so, makes the player take damage
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
