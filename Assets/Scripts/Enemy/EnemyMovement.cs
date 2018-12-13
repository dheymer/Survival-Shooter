using UnityEngine;

/**
 * Class to control the movement of the enemy
 * */
public class EnemyMovement : MonoBehaviour{
    Transform player;                       // The player's transform, to obtain its position
    PlayerHealth playerHealth;              // The player's health
    EnemyHealth enemyHealth;                // The enemy's health
    UnityEngine.AI.NavMeshAgent nav;        // The enemy's nav mesh agent

    /**
     * Awake Method
     * */
    void Awake (){
        // Obtain the player's transform, the player's health and enemy's health
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        // Obtain the enemy's navmesh agent
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }

    /**
     * Graphics Engine Update Method
     * */
    void Update (){
        // Check if the enemy and the player are alive
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0){
            // Set the enemy's destination to the player's position
            nav.SetDestination (player.position);
        }else{
            // Otherwise, disable the enemy's nav mesh agent
            nav.enabled = false;
        }
    }
}
