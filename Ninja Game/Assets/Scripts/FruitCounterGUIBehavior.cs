using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitCounterGUIBehavior : MonoBehaviour
{

    // Three text areas to edit
    TextMeshProUGUI counter;
    TextMeshProUGUI currentWeapon;
    TextMeshProUGUI nextWeapon;

    public GameBehavior gameManager;

    // Booleans so that the text is not constantly updating
    bool isTripleShruikenUnlocked = false;


    // Milestones
    private int tripleShruikenUnlock = 25;

    // Start is called before the first frame update
    void Start()
    {
        counter = GameObject.Find("FruitNumber").GetComponent<TextMeshProUGUI>();
        currentWeapon = GameObject.Find("CurrentWeapon").GetComponent<TextMeshProUGUI>();
        nextWeapon = GameObject.Find("NextWeapon").GetComponent<TextMeshProUGUI>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();

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
        }

        return fruitCounter;
    }
}
