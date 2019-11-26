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

    void Start()
    {
        pc.reputation += 1;
    }

    void Update()
    {
        if (gameObject.tag == "Player1")
        {
            transform.LookAt(Player1.transform);
            if (Vector3.Distance(Player1.transform.position, transform.position) >= distanceThreshold)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }

        if (gameObject.tag == "Player2")
        {
            transform.LookAt(Player2.transform);
            if (Vector3.Distance(Player2.transform.position, transform.position) >= distanceThreshold)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }
}
