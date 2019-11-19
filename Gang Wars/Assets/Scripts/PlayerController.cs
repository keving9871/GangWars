using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int reputation;
    public float moveSpeed;
    public Rigidbody rb;

    void Start()
    {
        reputation = 1;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Control Movement:
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = Camera.main.transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = Camera.main.transform.forward * -moveSpeed * Time.deltaTime;
        }
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            rb.velocity = Camera.main.transform.forward * 0;
        }
    }
}
