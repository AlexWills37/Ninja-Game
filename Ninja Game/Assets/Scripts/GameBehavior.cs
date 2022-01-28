using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the overall game logic, including points, win condition, and pausing the game
/// </summary>
public class GameBehavior : MonoBehaviour
{
    private bool isPaused = false;

    private int privatePoints;
    /// <summary>
    /// The number of points the user currently has
    /// </summary>
    public int points
    {
        get { return privatePoints; }
        set
        {
            privatePoints = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Locks the cursor at the beginning of the gamme
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        // Escape toggles pauase and unlocks the cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pause, freezing the game and unlocking the cursor
            isPaused = !isPaused;
            if (isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }
    }

    /// <summary>
    /// Display the user's points and whether or not the game is paused
    /// </summary>
    private void OnGUI()
    {
        Rect topMiddle = new Rect(Screen.width / 2, Screen.height / 3, 300, 50);

        if (isPaused)
        {
            GUI.Label(topMiddle, "Paused");
            if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 200, 50), "Quit Game"))
            {
                Application.Quit();
            }
            
        }

        // Display the score
        GUI.Box(new Rect(20, 20, 150, 25), "Fruit Destroyed: " + points);
    }
}
