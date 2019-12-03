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
    public float time;

    void Start()
    {
        player1WinText.enabled = false;
        player2WinText.enabled = false;
        tieText.enabled = false;
        timeLeftText.enabled = true;
        transform.localScale = new Vector3(1, 1, 1);
        Invoke("gameOver", time);
    }


    void Update()
    {

        //currentTime = Time.deltaTime;
        //currentTime = Mathf.Round(currentTime);

        int _time = Mathf.FloorToInt(time -= Time.deltaTime);
        timeLeftText.text = "Time Left: " + (_time.ToString());
    }

    void gameOver()
    {
       // print("Game Over");
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
        Time.timeScale = 0f;
    }
}
