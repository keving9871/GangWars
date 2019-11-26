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

    void Start()
    {
        //Each gang member adds this much reputation to their owner:
        pc.reputation += 1;

        //Make sure that all gang members are added to a list:
        if( pc != null)
        {
            pc.ownedGangMembersList.Add(gameObject);
        }

        if (ownership == 0)
        {
            //Find the correct player:
            otherPlayer = GameObject.FindWithTag("Player1");
            var pcAlternatePlayer = otherPlayer.GetComponent<PlayerController>();

            //Add gang members to owner's list:
            pcAlternatePlayer.ownedGangMembersList.Add(gameObject);
        }

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
            //Make them follow their owner:
            transform.LookAt(Player1.transform);
            if (Vector3.Distance(Player1.transform.position, transform.position) >= distanceThreshold)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }

        if (ownership == 1)
        {
            //Make them follow their owner:
            transform.LookAt(Player2.transform);
            if (Vector3.Distance(Player2.transform.position, transform.position) >= distanceThreshold)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }
}
