using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeLimit;
    private float currentTime;
    public Text gameOverText;
    public Text player1WinText;
    public Text player2WinText;
    public Text timeLeftText;
    public float time;
    public TerritoryController tc;

    void Start()
    {
        //player1WinText.enabled = false;
        gameOverText.enabled = true;
        //player2WinText.enabled = true;
        timeLeftText.enabled = true;
        //transform.localScale = new Vector3(1, 1, 1);
        //Invoke("gameOver", time);
        gameOverText.text = "";
    }


    void Update()
    {
        int _time = Mathf.FloorToInt(time -= Time.deltaTime);
        timeLeftText.text = "Time Left: " + (_time.ToString());

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }

        if (_time <= 7f)
        {
            GameObject.Find("EndFX.Source").GetComponent<AudioSource>().Play();
        }
    }

    public void gameOver(int whoWon)
    {
        if (whoWon == 0)
        {
            print("Who Won == 0");
            //print(player1WinText.enabled);
            //player1WinText.enabled = true;
            gameOverText.text = "Player 1 Wins!";
        }

        if(whoWon == 1)
        {
            print("Who won == 1");
            gameOverText.text = "Player 2 Wins!";

            //player2WinText.enabled = true;
        }

        Time.timeScale = 0f;
    }
}
