using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Variables:
    public int reputation;
    public float moveSpeed;
    public Rigidbody rb;
    public List<GameObject> ownedTerritoryList = new List<GameObject>(); //HERES THE LIST

    //UI:
    public Text currentReputation;

    //Controls:
    public KeyCode forward;
    public KeyCode back;
    public KeyCode purchaseKey;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }

}
