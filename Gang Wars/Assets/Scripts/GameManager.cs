using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timeLimit;
    private float currentTime;
    private int player1TerritoriesOwned, player2TerritoriesOwned;
    public Text player1WinText;
    public Text player2WinText;
    public Text tieText;

    void Start()
    {
        currentTime = Time.time;
        timeLimit = 500f;
        player1WinText.enabled = false;
        player2WinText.enabled = false;
        tieText.enabled = false;
    }


    void Update()
    {
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
