using UnityEngine;
using UnityEngine.UI;

/**
 * Class that manages the score text
 * */
public class ScoreManager : MonoBehaviour{
    // Public variables
    public static int score;                // The game score

    // Private variables
    Text text;                              // The score text component

    /**
     * Initialization method
     * */
    void Awake (){
        // Obtain the score's text component
        text = GetComponent <Text> ();
        // Initialize the score
        score = 0;
    }

    /**
     * Graphics Engine Update Method
     * */
    void Update (){
        // Updates the game score text
        text.text = "Score: " + score;
    }
}
