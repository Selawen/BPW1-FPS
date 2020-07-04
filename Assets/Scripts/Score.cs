using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int points { get; private set; }
    public int targetsKilled { get; private set; }
    public int targetsAlive { get; private set; }

    public int gameOverLimit;

    private AddTargets waveScript;

    // Start is called before the first frame update
    void Start()
    {
        waveScript = gameObject.GetComponent<AddTargets>();
        targetsKilled = 0;
        points = 0;

        if (gameOverLimit == 0)
        {
            gameOverLimit = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //amount of targets still active
        targetsAlive = waveScript.targetCounter - targetsKilled;

        //game over when amount of active targets is too high
        if (targetsAlive >= gameOverLimit)
        {
            gameObject.GetComponent<GameOverScript>().GameOver("Overwhelmed!");
        }
    }

    /// <summary>
    /// add point for kill and increment killcount
    /// </summary>
    /// <param name="pointsAdded"></param>
    public void AddPoints(int pointsAdded)
    {
        points += pointsAdded;
        targetsKilled++;
    }
}
