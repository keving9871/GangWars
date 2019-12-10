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
    public Transform spawnPoint; //Where the gang members spawn
    private Material objectMat; //Original material on spawn for territory
    bool readyToSpawn; //Once a territory is owned it becomes capable of spawning gang members
    public Color capturedColorPlayer1, capturedColorPlayer2; //Corresponding colors for the player's territories
    public TextMeshPro costTextPro, type; //Texts for territory costs and type of territory
    GameObject otherPlayer; //reference for the other player in code
    public bool homebase;
    public GameManager gm;

    //  REPUTATION IS HOW MUCH CURRENCY THE PLAYER HAS AND IS DIRECTLY LINKED TO HOW MANY GANG MEMBERS ARE OWNED
    //  TERRITORY COST IS HOW MUCH THE PLAYER NEEDS

    void Start()
    {
        //Start both players with 1 reputation because they begin the game with one gang member
        pc.reputation = 1;
        pc2.reputation = 1;

        //Set a reference for the starting material:
        objectMat = GetComponent<Renderer>().material;

        //At the start of the game spawn a gang member:
        readyToSpawn = true;
    }

    void Update()
    {
        //Prevent the cost of territory from being negative:
        if(territoryCost < 0)
        {
            territoryCost = 0;
        }

        costTextPro.text = territoryCost.ToString(); //Sets the cost text to whatever the territory cost is

        SpawnMethod(player1GangOwned, 0, capturedColorPlayer1);
        SpawnMethod(player2GangOwned, 1, capturedColorPlayer2);
    }

    private void Spawn()
    {
        GameObject thug = Instantiate(gangMember, spawnPoint.position, Quaternion.identity) as GameObject; //instantiate a gang member
        
        var thugScript = thug.GetComponent<GangMemberController>(); //create a reference for the GangMemberController class

        //Set their ownership on spawn:
        if (player1GangOwned == true)
        {
            pc.reputation += 1; //increment player 1's reputation by 1 for a gang member
            thugScript.ownership = 0; //set the ownership to player 1 in the inspector
        }

        if (player2GangOwned == true)
        {
            pc2.reputation += 1; //increment player 2's reputation by 1 for a gang member
            thugScript.ownership = 1; //set the ownership to player 2 in the inspector
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Get the PlayerController OnTriggerEnter
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2") //check which player it is
        {
            pc = other.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player1" &&  pc.isPlayer1)
        {
            //Stockpile Mechanic:
            if (Input.GetKeyDown(pc.stockpileKey)) //Did they press Q:
            {
                territoryCost += 1;
                pc.reputation -= 1;
                if (pc.reputation > 0)
                {
                    GameObject member = pc.ownedGangMembersList[pc.ownedGangMembersList.Count - 1];
                    pc.ownedGangMembersList.Remove(member);
                    Destroy(member);

                }
            }

            if (Input.GetKeyDown(pc.purchaseKey)) //Did they press E:
            {
                if (pc.reputation >= territoryCost) // if they have more reputation than the territory costs:
                {
                    pc.reputation -= territoryCost;

                    //Win condition:
                    if (homebase == true)
                    {
                        gm.gameOver(0);
                        homebase = false;
                    }

                    if (pc.ownedGangMembersList.Count >= 0 && pc.reputation >= 0) //if they have at least one gang member and reputation:
                    {
                        player1GangOwned = true; //if player 1 buys it set the bool to their ownership
                        player2GangOwned = false; //if player 1 buys it set the bool to false for player 2 owned

                        //Iterate through a loop to go through the list backwards:
                        for (int i = territoryCost - 1; i > 0; i--)
                        {
                            GameObject member = pc.ownedGangMembersList[i]; //Store a reference to the gang members you want in i (equal to territory cost)
                            pc.ownedGangMembersList.Remove(member); //Take the corresponding amount of gang members from that player
                            Destroy(member); //Destroy those gang members in the scene
                        }
                        takeTerritory(pc.isPlayer1);
                    }
                }
            }
        }

        if (other.gameObject.tag == "Player2" && !pc.isPlayer1)
        {
            //Stockpile Mechanic:
            if (Input.GetKeyDown(pc.stockpileKey)) //Did they press /
            {
                territoryCost += 1;
                pc.reputation -= 1;
            }

            if (Input.GetKeyDown(pc.purchaseKey)) //Did they press E:
            {
                if (pc.reputation >= territoryCost) // if they have more reputation than the territory costs:
                {
                    pc.reputation -= territoryCost;

                    //Win condition:
                    if (homebase == true)
                    {
                        gm.gameOver(1);
                        homebase = false;
                    }
                   
                    if (pc.ownedGangMembersList.Count >= 0 && pc.reputation >= 0) //if they have at least one gang member and reputation:
                    {
                        player1GangOwned = false; //if player 1 buys it set the bool to their ownership
                        player2GangOwned = true; //if player 1 buys it set the bool to false for player 2 owned

                        //Iterate through a loop to go through the list backwards:
                        for (int i = territoryCost - 1; i > 0; i--)
                        {
                            GameObject member = pc.ownedGangMembersList[i]; //Store a reference to the gang members you want in i (equal to territory cost)
                            pc.ownedGangMembersList.Remove(member); //Take the corresponding amount of gang members from that player
                            Destroy(member); //Destroy those gang members in the scene
                        }
                        takeTerritory(pc.isPlayer1);
                    }
                }
            }
        }
    }

    //This method only runs onTriggerStay after the purchase button has been pressed
    public void takeTerritory(bool player1Owned)
    {
        //if not owned add it to territory list of the player buying it:
        if (!pc.ownedTerritoryList.Contains(gameObject))
        {
            pc.ownedTerritoryList.Add(gameObject); //add it to list
        }

        //if owned by player one, other player = player2
        if (player1Owned)
        {
            otherPlayer = GameObject.FindWithTag("Player2"); //Set the reference of the other player to the gameObject tagged Player2

            var pcOtherPlayer = otherPlayer.GetComponent<PlayerController>(); //Set the reference to the player controller of other player

            if (pcOtherPlayer.ownedTerritoryList.Contains(gameObject)) //if the other player owns this territory
            {
                //remove this territory from the other player's list:
                pcOtherPlayer.ownedTerritoryList.Remove(gameObject);
            }
        }

        //not owned by player 1
        else
        {
            otherPlayer = GameObject.FindWithTag("Player1"); //other player is set to Player1

            var pcAlternatePlayer = otherPlayer.GetComponent<PlayerController>(); //Set the reference for player 1's player controller

            if (pcAlternatePlayer.ownedTerritoryList.Contains(gameObject)) //if the other player owns this territory
            {
                //remove this territory from their list:
                pcAlternatePlayer.ownedTerritoryList.Remove(gameObject);
            }
        }
    }

    void SpawnMethod(bool check, int index, Color color)
    {

        if (check)
        {
            if (readyToSpawn)
            {
                InvokeRepeating("Spawn", 0, spawnRate);
                readyToSpawn = false;
                GMC.ownership = index;
            }

            objectMat.color = color;
        }
    }
}
