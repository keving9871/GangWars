using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangMemberController : MonoBehaviour
{
    public PlayerController pc;
    public GameObject Player;
    public float moveSpeed;
    public float distanceThreshold; // How close it is willing to get to something

    void Start()
    {
        pc.reputation += 1;
    }

    void Update()
    {
        transform.LookAt(Player.transform);
        if (Vector3.Distance(Player.transform.position, transform.position) >= distanceThreshold)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

    }
}
