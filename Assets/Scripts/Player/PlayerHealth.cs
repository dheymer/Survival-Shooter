using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Class that manages the player's health
 * */
public class PlayerHealth : MonoBehaviour{
    public int startingHealth = 100;                         // Player's starting health
    public int currentHealth;                                // Player's current health
    public Slider healthSlider;                              // The player's health slider
    public Image damageImage;                                // The image that indicates when the player takes damage
    public AudioClip deathClip;                              // The player's death audio clip
    public float flashSpeed = 5f;                            // The duration of the damage image to be shown
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);  // The color for the damage image


    Animator anim;                                           // The player's animator controller
    AudioSource playerAudio;                                 // The player's audio source
    PlayerMovement playerMovement;                           // The player's movement class
    PlayerShooting playerShooting;                           // The player's shooting class
    bool isDead;                                             // The dead player trigger
    bool damaged;                                            // The player damage trigger

    /**
     * Initialization method 
     * */
    void Awake (){
        // Obtain the player's animator reference, along with the AudioSource and the PlayerMovement and PlayerShooting components
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        // Init the current health with the sarting health value
        currentHealth = startingHealth;
    }

    /**
     * Graphics Engine Update Method
     * */
    void Update (){
        // Check if the player is damaged
        if(damaged){
            // Paint the screen with the damage color
            damageImage.color = flashColour;
        }else{
            // Otherwise, make the transition of the scree color to transparent over time
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        // Set the damage trigger to false
        damaged = false;
    }

    /**
     * Method to update the player's health
     * */
    public void TakeDamage (int amount){
        // Set the damage trigger to true
        damaged = true;
        // Decrease the player's health depending on the amunt of damage taken
        currentHealth -= amount;
        // Update the health indicator with the player's health value
        healthSlider.value = currentHealth;
        // Play the "player hurt" sound
        playerAudio.Play ();
        // Check if the player's health reaches 0 and the player is not dead yet
        if(currentHealth <= 0 && !isDead){
            // If so, make the player die
            Death ();
        }
    }

    /**
     * Method that makes the player die
     * */
    void Death (){
        // set the plater's dead trigger to true
        isDead = true;
        // Disable the shooting effects
        playerShooting.DisableEffects ();
        // Calls the player's dead animation
        anim.SetTrigger ("Die");
        // Set the audio source with the "player dead" sound and play it
        playerAudio.clip = deathClip;
        playerAudio.Play ();
        // Disable the player's movement and shooting
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    /**
     * Method to restart the game
     * */
    public void RestartLevel (){
        // Loads the game scene
        SceneManager.LoadScene (0);
    }
}
