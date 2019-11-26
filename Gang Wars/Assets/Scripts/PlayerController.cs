﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Variables:
    public int reputation;
    public float moveSpeed;
    public Rigidbody rb;
    public List<GameObject> ownedTerritoryList = new List<GameObject>(); //HERES THE LIST FOR TERRITORIES
    public List<GameObject> ownedGangMembersList = new List<GameObject>(); // Here's the list for gang members

    //UI:
    public Text currentReputation;
    public Text player1GangMembersListText;
    public Text player2GangMembersListText;

    //Controls:
    public KeyCode forward;
    public KeyCode back;
    public KeyCode purchaseKey;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player1GangMembersListText.enabled = false;
        player2GangMembersListText.enabled = false;
    }

    void Update()
    {
        //Display Reputation:
        currentReputation.text = "Reputation: " + reputation.ToString();

        //Control Movement:
        if (Input.GetKey(forward))
        {
            rb.velocity = Camera.main.transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(back))
        {
            rb.velocity = Camera.main.transform.forward * -moveSpeed * Time.deltaTime;
        }

        if (!Input.GetKey(forward) && !Input.GetKey(back))
        {
            rb.velocity = Camera.main.transform.forward * 0;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            //Add the list to the text object
            player1GangMembersListText.enabled = true;
        }
        if (Input.GetKey(KeyCode.Slash))
        {
            //Add the list to the text object
            player2GangMembersListText.enabled = true;
        }
    }

}
