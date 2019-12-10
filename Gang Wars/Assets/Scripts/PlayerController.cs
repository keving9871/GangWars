using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Variables:
    public int reputation;
    public float moveSpeed;
    public Rigidbody rb;
    public bool isPlayer1;
    public List<GameObject> ownedTerritoryList = new List<GameObject>(); //HERES THE LIST FOR TERRITORIES
    public List<GameObject> ownedGangMembersList = new List<GameObject>(); // Here's the list for gang members

    public Animator cityBoyAnimController;
    public Animation swampBoyAnimController;
    
    //UI:
    public Text currentReputation;
    //public Text player1GangMembersListText;
    //public Text player2GangMembersListText;

    //Controls:
    public KeyCode forward;
    public KeyCode back;
    public KeyCode purchaseKey;
    public KeyCode stockpileKey;
    public KeyCode left;
    public KeyCode right;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //player1GangMembersListText.enabled = false;
        //player2GangMembersListText.enabled = false;
        
        cityBoyAnimController.SetBool("Idle", true);
        cityBoyAnimController.SetBool("RunningForward", false);
        cityBoyAnimController.SetBool("RunningBackward", false);
    }

    void Update()
    {
        
        //Display Reputation:
        currentReputation.text = "Reputation: " + reputation.ToString();

        //Make sure that reputation is never negative:
        if(reputation < 0)
        {
            reputation = 0;
        }

        //Control Movement:
        if (Input.GetKey(forward))
        {
            rb.velocity = this.transform.forward * moveSpeed * Time.deltaTime;
            cityBoyAnimController.SetBool("RunningForward",true);
            cityBoyAnimController.SetBool("Idle", false);
        }

        if (Input.GetKey(back))
        {
            rb.velocity = this.transform.forward * -moveSpeed * Time.deltaTime;
            cityBoyAnimController.SetBool("RunningBackward",true);
            cityBoyAnimController.SetBool("Idle", false);
        }

        if (!Input.GetKey(forward) && !Input.GetKey(back))
        {
            rb.velocity = this.transform.forward * 0;
            cityBoyAnimController.SetBool("RunningForward", false);
            cityBoyAnimController.SetBool("RunningBackward", false);
            cityBoyAnimController.SetBool("Idle", true);
        }

        if (Input.GetKey(left))
        {
            rb.velocity = this.transform.right * -moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(right))
        {
            rb.velocity = this.transform.right * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            //Add the list to the text object
            //player1GangMembersListText.enabled = true;
        }
        if (Input.GetKey(KeyCode.Slash))
        {
            //Add the list to the text object
            //player2GangMembersListText.enabled = true;
        }
    }

}
