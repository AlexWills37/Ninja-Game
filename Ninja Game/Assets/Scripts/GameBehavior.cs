using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the overall game logic, including points and pausing the game
/// </summary>
public class GameBehavior : MonoBehaviour
{
    public TimerBehavior timer;

    private bool isPaused = false;

    private bool gameOver = false;

    private bool freePlay = false;
    private string rank = "";

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
        timer = GameObject.Find("Timer").GetComponent<TimerBehavior>();
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

        // Check for the end condition
        if (!freePlay && !gameOver && timer.TimeUp)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            gameOver = true;

            // Determine the rank based on how many fruit the player destroyed
            int rankS = (int)(timer.timeLimit) - 10;
            int rankA = (int)(rankS * 0.9);
            int rankB = (int)(rankS * 0.7);
            
            if(points >= rankS)
            {
                rank = "S";
            } else if (points >= rankA)
            {
                rank = "A";
            } else if (points >= rankB)
            {
                rank = "B";
            } else
            {
                rank = "C";
            }

        }
    }

    /// <summary>
    /// Display the user's points and whether or not the game is paused
    /// </summary>
    private void OnGUI()
    {
        Rect topMiddle = new Rect(Screen.width / 2 - 45, Screen.height / 3, 90, 25);

        // When paused, allow the user to quit the game
        if (isPaused)
        {
            GUI.Box(topMiddle, "Paused");
            if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 200, 50), "Quit Game"))
            {
                Application.Quit();
            }
            
        }

        // Game over screen - display score and allow user to quit or keep playing
        if (gameOver)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 3, 200, 55), "Game Over\nFruit Collected: " + points + "\nRank: " + rank);

            
            // Quit game button
            if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 200, 30), "Quit Game"))
            {
                Application.Quit();
            }

            // Continue free play button
            if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 140, 200, 30), "Free Play"))
            {
                EnterFreePlay();
            }

        }

        // Display the score
        GUI.Box(new Rect(20, 20, 150, 25), "Fruit Destroyed: " + points);
    }


    /// <summary>
    /// Configure the game to work in free play - no timer, just fruit.
    /// </summary>
    private void EnterFreePlay()
    {
        gameOver = false;
        freePlay = true;
        timer.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
