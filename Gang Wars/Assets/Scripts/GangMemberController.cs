using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangMemberController : MonoBehaviour
{
    public PlayerController pc;
    public GameObject Player1, Player2;
    public float moveSpeed;
    public float distanceThreshold; // How close it is willing to get to something
    public int ownership; // 0 is for player 1 and 1 is for player 2
    GameObject otherPlayer;
    public Color player1Controlled, player2Controlled;
    private Material startingMaterial;

    public Renderer gangMemberShirt;
    private Material shirtColor;

    public Animator gangMemberController;
    void Start()
    {
        //Assign the starting material to the renderer componment's material:
        //Fill PlayerController in the inspector:
        //pc = new PlayerController();
        //pc = gameObject.AddComponent<PlayerController>();

        //Fill Player1 in the inspector:
        Player1 = GameObject.FindWithTag("Player1");

        //Fill Player2 in the inspector:
        Player2 = GameObject.FindWithTag("Player2");

        //Set MoveSpeed and DistanceThreshold:
        moveSpeed = 4f;
        distanceThreshold = 2f;
        shirtColor = gangMemberShirt.material;

        //Each gang member adds this much reputation to their owner:
        //pc.reputation += 1;

        //Make sure that all gang members are added to a list:
        if(pc != null)
        {
            pc.ownedGangMembersList.Add(gameObject);
        }

        if (pc == null)
        {
            pc = GetComponent<PlayerController>();
        }
        //For Player one
        if (ownership == 0)
        {
            //Find the correct player:
            otherPlayer = GameObject.FindWithTag("Player1");
            var pcAlternatePlayer = otherPlayer.GetComponent<PlayerController>();

            //Add gang members to owner's list:
            pcAlternatePlayer.ownedGangMembersList.Add(gameObject);
        }

        //For Player two
        if (ownership == 1)
        {
            //Find the correct player:
            otherPlayer = GameObject.FindWithTag("Player2");
            var pcOtherPlayer = otherPlayer.GetComponent<PlayerController>();

            //Add gang members to owner's list:
            pcOtherPlayer.ownedGangMembersList.Add(gameObject);
        }
    }

    void Update()
    {
        if (ownership == 0)
        {
            //Change color based on owner:
            /*startingMaterial.color = player1Controlled;*/
/*            shirtColor = gangMemberShirt.material;
            Debug.Log(shirtColor.GetColor("_Color"));
            shirtColor.SetColor("_Color", player1Controlled);*/

            shirtColor.SetColor("_Color", player1Controlled);
            //Make them follow their owner:
            transform.LookAt(Player1.transform);
            if (Vector3.Distance(Player1.transform.position, transform.position) > distanceThreshold)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                
                gangMemberController.SetBool("RunningForward",true);
                gangMemberController.SetBool("Idle", false);
            }

            else
            {
                gangMemberController.SetBool("RunningForward",false);
                gangMemberController.SetBool("Idle", true);
            }
        }

        if (ownership == 1)
        {
            //Change color based on owner:
            /*startingMaterial.color = player2Controlled;*/

        
            shirtColor.SetColor("_Color", player2Controlled);

            //Make them follow their owner:
            transform.LookAt(Player2.transform);
            if (Vector3.Distance(Player2.transform.position, transform.position) >= distanceThreshold)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                
                gangMemberController.SetBool("RunningForward",true);
                gangMemberController.SetBool("Idle", false);
            }

            else
            {
                gangMemberController.SetBool("RunningForward",false);
                gangMemberController.SetBool("Idle", true);
            }
        }
    }
}


