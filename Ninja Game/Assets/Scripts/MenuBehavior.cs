using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu script
/// </summary>
public class MenuBehavior : MonoBehaviour
{
    /// <summary>
    /// Load the first scene after the main menu
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
