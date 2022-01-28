using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script to operate the tutorial
/// </summary>
public class TutorialTextBehavior : MonoBehaviour
{

    public TextMeshProUGUI textBox;
    public FruitGeneratorBehavior fruitSpawner;
    public GameBehavior gameManager;

    // Text to display in the tutorial. An empty string represents a pause in the text to stop user progression.
    public List<string> tutorialScript = new List<string>
    {
        "Welcome to the Ninja Game! Press spacebar to continue.",
        "In this world, fruit plagues the people as it falls from the sky, making a mess of everything.",
        "It is up to the food ninjas to clean up all the fruit with their ninja stars",
        "To become a food ninja - a hero - you must first learn how to destroy fruit with shurikens.",
        "Try moving around with WASD!",
        "",
        "Perfect! You can also move the mouse to change the direction you are looking.",
        "When the fruit falls from the sky, you will probably want to move the mouse to look up at the fruit.",
        "Now that you can move and look around, try throwing some shurikens by clicking the mouse!",
        "",
        "Excellent! Now to put it all together...",
        "Try hitting some fruit with your ninja stars!",
        "",
        "I think you are ready to go to the city! Press spacebar again to move on."
    };

    // Unskippable text waits for a player action
    private bool skippableText;
    private int scriptIndex = 0;
    // Check for teaching the player how to move
    private bool firstMove = false;
    // Check for teaching the player how to throw shurikens
    private bool firstShuriken = false;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize components
        textBox = this.GetComponent<TextMeshProUGUI>();

        textBox.SetText( tutorialScript[scriptIndex] );
        skippableText = true;

        this.gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();

        // Turn off the fruit generator
        this.fruitSpawner = GameObject.Find("FruitGenerator").GetComponent<FruitGeneratorBehavior>();
        this.fruitSpawner.spawningActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Allow user to progress text with spacebar
        if (skippableText && Input.GetKeyDown(KeyCode.Space))
        {
            progressText();
        } 

        // If text is not skippable, we are waiting for the user to take action
        if (!skippableText)
        {
            TeachPlayer();
        }
    }

    /// <summary>
    /// Progress the text script. At empty strings, the text will pause until it is set to continue.
    /// </summary>
    private void progressText()
    {
        scriptIndex++;

        // When dialogue is over, end the text and go to the next level
        if (scriptIndex >= tutorialScript.Count)
        {
            textBox.SetText("");
            SceneManager.LoadScene(2);
        }
        else
        {

            textBox.SetText(tutorialScript[scriptIndex]);

            // If there's an empty string, the user should not be able to progress the text until something happens
            if (scriptIndex < tutorialScript.Count - 1 && tutorialScript[scriptIndex + 1] == "")
            {
                skippableText = false;
            }

        } // end of else
    }


    /// <summary>
    /// After teaching the user how to do something, resume the text box
    /// </summary>
    private void ResumeText()
    {
        skippableText = true;
        scriptIndex++;
        progressText();
    }

    /// <summary>
    /// Wait for the player to take action in order to progress the tutorial
    /// </summary>
    private void TeachPlayer()
    {
        // First checkpoint: Teach the player how to move
        if (!firstMove)
        {
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                firstMove = true;
                ResumeText();

            }
            // Second checkpoint: Teach how to throw shurikens
        } else if (!firstShuriken)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstShuriken = true;
                ResumeText();
                fruitSpawner.timeBetweenSpawn = 5f;
                fruitSpawner.spawningActive = true;
            }
            // Third checkpoint: Teach to destroy fruit
        } else
        {
            // Once the user destroys a fruit, progress dialogue to the end
            if (gameManager.points > 0)
            {
                ResumeText();
            }
        }
    }

}
