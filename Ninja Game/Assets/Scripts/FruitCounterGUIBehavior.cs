using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Main GUI during gameplay
/// </summary>
public class FruitCounterGUIBehavior : MonoBehaviour
{

    // Three text areas to edit
    TextMeshProUGUI counter;
    TextMeshProUGUI currentWeapon;
    TextMeshProUGUI nextWeapon;

    CanvasGroup whitePanel;
    CanvasGroup endScreen;


    public GameBehavior gameManager;

    // Booleans so that the text is not constantly updating
    bool isTripleShruikenUnlocked = false;
    bool isGameWon = false;


    // Milestones
    private int tripleShruikenUnlock = 25;
    private int gameWinUnlock = 100;

    private bool fading = false;
    private float fadeDuration = 1f;
    private float fadeStartTime;
    private float alphaChange;  // This variable is the overall change in alpha (so if the target is 0 from 1, the change is -1)

    private bool fadingWhite = false;
    private bool fadingEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        counter = GameObject.Find("FruitNumber").GetComponent<TextMeshProUGUI>();
        currentWeapon = GameObject.Find("CurrentWeapon").GetComponent<TextMeshProUGUI>();
        nextWeapon = GameObject.Find("NextWeapon").GetComponent<TextMeshProUGUI>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();

        whitePanel = GameObject.Find("White Screen").GetComponent<CanvasGroup>();

        endScreen = GameObject.Find("End Screen").GetComponent<CanvasGroup>();

    }

    // Update is called once per frame
    void Update()
    {
        // Update the fruit counter
        counter.SetText(createFruitCounterText());

        // When we unlock triple shurikens, update the gui
        if(!isTripleShruikenUnlocked && gameManager.points >= tripleShruikenUnlock)
        {
            currentWeapon.SetText("Current Weapon: Triple Shuriken");
            nextWeapon.SetText("");

            isTripleShruikenUnlocked = true;

        } else if (!isGameWon && gameManager.points >= gameWinUnlock)
        {
            // When the game is won, transition to the win screen.

            fadingWhite = true;
            fadeStartTime = Time.time;
            isGameWon = true;

        } else if (fadingWhite)    // This will end after the block has been executed at least once, and after the method is finished fading
        {
            fadingWhite = fade(whitePanel, 1, fadeDuration);
            
            // When finished fading, begin the end screen fade
            if (!fadingWhite)
            {
                fadingEnd = true;
            }
        } else if (fadingEnd)
        {
            fadingEnd = fade(endScreen, 1, fadeDuration);

            // When the end screen is finished, tell the game manager to end the game
            if (!fadingEnd)
            {
                gameManager.EndGame();
            }
        }


    }

    /// <summary>
    /// Creates the string to display in the fruit counter, with the next milestone if there is one
    /// </summary>
    /// <returns> the string to display in the fruit counter box </returns>
    private string createFruitCounterText()
    {
        string fruitCounter = "Fruit Destroyed: " + gameManager.points;

        if (gameManager.points < tripleShruikenUnlock)
        {
            fruitCounter += " / " + tripleShruikenUnlock;
        } else
        {
            fruitCounter += " / " + gameWinUnlock;
        }

        return fruitCounter;
    }

    private bool fade(CanvasGroup toFade, float targetAlpha, float fadeDuration)
    {
        // If the fade has not begun, initialize the fade
        if (!fading)
        {
            fadeStartTime = Time.time;
            fading = true;
            alphaChange = targetAlpha - toFade.alpha;
        
        } else
        {
            // Continue the fade
            // Repeat this loop, slowly fading until the fade is complete
            if (Time.time > fadeStartTime + fadeDuration)
            {
                fading = false;
                toFade.alpha = targetAlpha;
            }
            else
            {
                // Slowly fade in, linearly to the target across the fadeDuration
                toFade.alpha += alphaChange * Time.deltaTime / fadeDuration;
            }
        }

        return fading;
    }

}
