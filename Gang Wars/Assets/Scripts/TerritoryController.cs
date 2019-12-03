using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerritoryController : MonoBehaviour
{
    //Variables:
    public PlayerController pc, pc2; //Player Controllers
    public GangMemberController GMC; //Gang Member Controllers
    public int territoryCost; //Territory Cost in gang members
    public float spawnRate; //How fast gang members spawn
    public bool player1GangOwned, player2GangOwned; //Which player owns the territory or gang member
    public GameObject gangMember; //Gang member game object
    public Material captureMat; //Color change once a player takes a territory
    //public Text costText, spawnableText; //Display how much a territory cost on it
    public Transform spawnPoint;
    private Material objectMat; //Original material on spawn for territory
    bool readyToSpawn; //Once a territory is owned it becomes capable of spawning gang members
    public Color capturedColorPlayer1, capturedColorPlayer2; //Corresponding colors for the player's territories
    public TextMeshPro costTextPro, type;
    GameObject otherPlayer; //reference for the other player in code
    GameObject thisPlayer; //reference for the current player in code


    //  REPUTATION IS HOW MUCH CURRENCY THE PLAYER HAS AND IS DIRECTLY LINKED TO HOW MANY GANG MEMBERS ARE OWNED
    //  TERRITORY COST IS HOW MUCH THE PLAYER NEEDS

    void Start()
    {
        //if (pc != null)
        //{
        //    pc.ownedTerritoryList.Add(gameObject);
        //}

        pc.reputation = 1;
        pc2.reputation = 2;

        objectMat = GetComponent<Renderer>().material;
        //player1GangOwned = false;
        //player2GangOwned = false;
        //ownedTerritoryListPlayer1.enabled = false;   MAKE THIS THE UI DISABLE FOR PLAYER 1
        //ownedTerritoryListPlayer2.enabled = false;   MAKE THIS THE UI DISABLE FOR PLAYER 2

        //if (tag == "default")
        //{
        //    territoryCost = 1;
        //    spawnRate = Time.deltaTime + 5.0f;
        //}

        readyToSpawn = true;
    }

    void Update()
    {
        costTextPro.text = territoryCost.ToString();

        if(readyToSpawn == true)
        {
            //spawnableTextPro.text = "can spawn";
        }

        if (player1GangOwned == true)
        {
            if (readyToSpawn == true)
            {
                InvokeRepeating("Spawn", 0, spawnRate);
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
                InvokeRepeating("Spawn", 0, spawnRate);
                readyToSpawn = false;
                GMC.ownership = 1;
            }

            if (Input.GetKey(KeyCode.RightShift))
            {
                //ownedTerritoryListPlayer2.enabled = true; // MAKE THIS THE UI ENABLE FOR PLAYER 2
            }

            objectMat.color = capturedColorPlayer2;
        }
    }

    private void Spawn()
    {
        GameObject thug = Instantiate(gangMember, spawnPoint.position, Quaternion.identity) as GameObject;
        
        var thugScript = thug.GetComponent<GangMemberController>();

        //Set their ownership on spawn:
        if (player1GangOwned == true)
        {
            pc.reputation += 1;
            thugScript.ownership = 0;
        }

        if (player2GangOwned == true)
        {
            pc2.reputation += 1;
            thugScript.ownership = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Get the PlayerController OnTriggerEnter
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            pc = other.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            Debug.Log("Player 1 is staying");
            if (Input.GetKeyDown(pc.purchaseKey))
            {
                Debug.Log("Purchase Key pressed!");
                if (pc.reputation >= territoryCost)
                {
                    Debug.Log("Reputation > Territory Cost");
                    player1GangOwned = true;
                    player2GangOwned = false;

                    pc.reputation -= territoryCost; // Take the reputation a territory cost after purchase

                    //Take the corresponding amount of gang members from that player
                    //Use the ownedGangMembersList as a reference from the player controller!
                    //Iterate through a loop to go through the list
                    for (int i = territoryCost - 1; i > 0; i--)
                    {
                        GameObject member = pc.ownedGangMembersList[i];
                        pc.ownedGangMembersList.Remove(member);
                        Destroy(member);
                        //if (i <= territoryCost)
                        //{
                        //    Destroy(gangMember);
                        //    pc.ownedGangMembersList.Remove(gangMember);
                        //}
                    }
                }
                takeTerritory(player1GangOwned);
            }
        }

        if (other.gameObject.tag == "Player2")
        {
            if (Input.GetKeyDown(pc.purchaseKey))
            {
                if (pc2.reputation >= territoryCost)
                {
                    player1GangOwned = false;
                    player2GangOwned = true;

                    pc2.reputation = pc2.reputation - this.territoryCost; // // Take the reputation a territory cost after purchase

                    for (int i = territoryCost - 1; i >= 0; i--)
                    {
                        pc2.ownedGangMembersList.Remove(pc2.ownedGangMembersList[pc2.ownedGangMembersList.Count - 1]);
                        if (i <= territoryCost)
                        {
                            Destroy(gangMember);
                            pc2.ownedGangMembersList.Remove(gangMember);
                        }
                    }
                }
                takeTerritory(player1GangOwned);
            }
        }
    }

    public void takeTerritory(bool player1Owned)
    {
        print("take territory");

        //not owned add it to territory list
        if (!pc.ownedTerritoryList.Contains(gameObject))
        {
            pc.ownedTerritoryList.Add(gameObject);
            print(pc.ownedTerritoryList.Count);
        }

        //if owned by player one, other player = player2
        if (player1Owned)
        {
            //pc.reputation = pc.reputation - this.territoryCost;
            otherPlayer = GameObject.FindWithTag("Player2");
            var pcOtherPlayer = otherPlayer.GetComponent<PlayerController>();
            thisPlayer = GameObject.FindWithTag("Player1");
            var pcThisPlayer = thisPlayer.GetComponent<PlayerController>();

            if (pcOtherPlayer.ownedTerritoryList.Contains(gameObject))
            {
                //remove it from other player
                pcOtherPlayer.ownedTerritoryList.Remove(gameObject);
            }

            GameObject firstGangMember = pcThisPlayer.ownedGangMembersList[0];
            pcThisPlayer.ownedGangMembersList.Remove(firstGangMember);
            Destroy(firstGangMember);   
        }

        else
        {
            //pc.reputation = pc.reputation - this.territoryCost;
            otherPlayer = GameObject.FindWithTag("Player1");
            var pcAlternatePlayer = otherPlayer.GetComponent<PlayerController>();

            if (pcAlternatePlayer.ownedTerritoryList.Contains(gameObject))
            {
                pcAlternatePlayer.ownedTerritoryList.Remove(gameObject);
            }
        }
    }
}
