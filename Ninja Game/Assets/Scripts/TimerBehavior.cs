using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Timer to keep track of the game's time
/// </summary>
public class TimerBehavior : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    public float timeLimit = 60;

    private bool timeUp;
    /// <summary>
    /// True when the timer is over, false when the timer is still going
    /// </summary>
    public bool TimeUp
    {
        get { return timeUp; }
        private set { timeUp = value;  }
    }
    

    private float timeElapsed;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        timerText = this.GetComponent<TextMeshProUGUI>();
        this.startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed = Time.time - startTime;

        if(timeElapsed < timeLimit)
        {
            timerText.SetText("Time Remaining: " + (int)(timeLimit - timeElapsed));
        } else
        {
            TimeUp = true;
            timerText.SetText("Time Remaining: " + 0);
        }
        
    }
}
