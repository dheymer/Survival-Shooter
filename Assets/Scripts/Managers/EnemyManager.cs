using UnityEngine;

/**
 * Class that manages the enemy spawn
 * */
public class EnemyManager : MonoBehaviour{
    // Public variables
    public PlayerHealth playerHealth;           // The player's health
    public GameObject enemy;                    // The spawn enemy
    public float spawnTime = 3f;                // Time between enemy spawns
    public Transform[] spawnPoints;             // Array of spawn points

    /**
     * Initial Method
     * */
    void Start (){
        // Invoke the spawns each time the spawn time elapses
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

    /**
     * Method that spawns the enemies
     * */
    void Spawn (){
        // Check if player is death
        if(playerHealth.currentHealth <= 0f){
            // If so, leave the method
            return;
        }
        // Otherwise, pick one spawn point randomly
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        // ...and create an enemy in that point
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
