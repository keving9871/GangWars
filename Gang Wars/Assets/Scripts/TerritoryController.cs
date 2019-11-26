using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerritoryController : MonoBehaviour
{
    //Variables:
    public PlayerController pc;
    public GangMemberController GMC;
    public float territoryCost;
    public float spawnRate, captureRate;
    public bool player1GangOwned, player2GangOwned;
    public GameObject gangMember;
    public Material captureMat;
    private Material objectMat;
    bool readyToSpawn;
    public Color capturedColorPlayer1, capturedColorPlayer2;
    GameObject otherPlayer;

    //  REPUTATION IS HOW MUCH CURRENCY THE PLAYER HAS
    //  TERRITORY COST IS HOW MUCH THE PLAYER NEEDS

    void Start()
    {
        if (pc != null)
        {
            pc.ownedTerritoryList.Add(gameObject);
        }

        objectMat = GetComponent<Renderer>().material;
        //captureRate = 500.0f; // FUTURE TIME IT TAKES TO CAPTURE AN ENEMY LAND
        //player1GangOwned = false;
        //player2GangOwned = false;
        //ownedTerritoryListPlayer1.enabled = false;   MAKE THIS THE UI DISABLE FOR PLAYER 1
        //ownedTerritoryListPlayer2.enabled = false;   MAKE THIS THE UI DISABLE FOR PLAYER 2

        if (tag == "default")
        {
            territoryCost = 1f;
            spawnRate = Time.deltaTime + 5.0f;
        }

        readyToSpawn = true;
    }

    void Update()
    {
        if (player1GangOwned == true)
        {
            if (readyToSpawn == true)
            {
                InvokeRepeating("Spawn", 0, 5);
                readyToSpawn = false;
                GMC.ownership = 0;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                //ownedTerritoryList.enabled = true; // MAKE THIS THE UI ENABLE FOR PLAYER 1
            }

            objectMat.color = capturedColorPlayer1;
        }

        if (player2GangOwned == true)
        {
            if (readyToSpawn == true)
            {
                InvokeRepeating("Spawn", 0, 5);
                readyToSpawn = false;
                GMC.ownership = 1;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                //ownedTerritoryListPlayer2.enabled = true; // MAKE THIS THE UI ENABLE FOR PLAYER 2
            }

            objectMat.color = capturedColorPlayer2;
        }
    }

    private void Spawn()
    {
        GameObject thug =  Instantiate(gangMember, transform.position, Quaternion.identity) as GameObject;
        
        var thugScript = thug.GetComponent<GangMemberController>();

        if (player1GangOwned == true)
        {
            thugScript.ownership = 0;
        }

        if (player2GangOwned == true)
        {
            thugScript.ownership = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            pc = other.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            if (Input.GetKeyDown(pc.purchaseKey))
            {
                if (pc.reputation >= territoryCost)
                {
                    player1GangOwned = true;
                    player2GangOwned = false;
                }
                takeTerritory(player1GangOwned);
            }
        }

        if (other.gameObject.tag == "Player2")
        {
            if (Input.GetKeyDown(pc.purchaseKey))
            {
                if (pc.reputation >= territoryCost)
                {
                    player1GangOwned = true;
                    player2GangOwned = false;
                }
                takeTerritory(player1GangOwned);
            }
        }
    }

    public void takeTerritory(bool player1Owned)
    {
        if (!pc.ownedTerritoryList.Contains(gameObject))
        {
            pc.ownedTerritoryList.Add(gameObject);
            print(pc.ownedTerritoryList.Count);

        }

        if (player1Owned)
        {
            otherPlayer = GameObject.FindWithTag("Player2");
            var pcOtherPlayer = otherPlayer.GetComponent<PlayerController>();

            if (pcOtherPlayer.ownedTerritoryList.Contains(gameObject))
            {
                pcOtherPlayer.ownedTerritoryList.Remove(gameObject);
            }
        }

        else
        {
            otherPlayer = GameObject.FindWithTag("Player1");
            var pcAlternatePlayer = otherPlayer.GetComponent<PlayerController>();

            if (pcAlternatePlayer.ownedTerritoryList.Contains(gameObject))
            {
                pcAlternatePlayer.ownedTerritoryList.Remove(gameObject);
            }
        }
    }
}
