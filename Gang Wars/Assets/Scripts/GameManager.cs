using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeLimit;
    private float currentTime;
    private int player1TerritoriesOwned, player2TerritoriesOwned;
    public Text player1WinText;
    public Text player2WinText;
    public Text tieText;
    public Text time;
    private float timeleft;

    void Start()
    {
        currentTime = Time.time;
        timeLimit = 500f;
        player1WinText.enabled = false;
        player2WinText.enabled = false;
        tieText.enabled = false;
        time.enabled = true;
    }


    void Update()
    {
        timeleft = timeLimit - currentTime;
        timeleft = Mathf.Round(timeleft);
        time.text = "Time Left: " + timeleft.ToString();
        currentTime = Time.time;
        if (currentTime >= timeLimit)
        {
            gameOver();
        }
    }

    void gameOver()
    {
        if (player1TerritoriesOwned > player2TerritoriesOwned)
        {
            player1WinText.enabled = true;
        }
        if (player1TerritoriesOwned == player2TerritoriesOwned)
        {
            tieText.enabled = true;
        }
        if (player1TerritoriesOwned < player2TerritoriesOwned)
        {
            player2WinText.enabled = true;
        }
    }
}
