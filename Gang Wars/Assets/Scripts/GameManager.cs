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
    public Text timeLeftText;
    private float timeleft;

    void Start()
    {
        currentTime = Time.deltaTime;
        player1WinText.enabled = false;
        player2WinText.enabled = false;
        tieText.enabled = false;
        timeLeftText.enabled = true;
    }


    void Update()
    {
        //Time Left:
        timeleft = timeLimit - currentTime;
        timeleft = Mathf.Round(timeleft);
        timeLeftText.text = "Time Left: " + timeleft.ToString();

        //Check if time is up and end game:
        if (currentTime >= timeleft)
        {
            gameOver();
        }
    }

    void gameOver()
    {
        print("Game Over");
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
