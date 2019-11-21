using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerritoryController : MonoBehaviour
{
    //Variables:
    public PlayerController pc;
    public float territoryCost;
    public float captureRate, spawnRate;
    public bool gangOwned;
   // public Text ownedTerritoryList;
    public GameObject gangMember;
    public Material captureMat;
    private Material objectMat;

    //  REPUTATION IS HOW MUCH CURRENCY THE PLAYER HAS
    //  TERRITORY COST IS HOW MUCH THE PLAYER NEEDS

    void Start()
    {
        objectMat = GetComponent<Renderer>().material;
        captureRate = 500.0f;
        pc.reputation = 0;
        gangOwned = false;
//        ownedTerritoryList.enabled = false;

        if (tag == "default")
        {
            territoryCost = 1f;
            spawnRate = Time.time + 5.0f;
        }
    }

    void Update()
    {
        if (gangOwned == true)
        {
            if (Time.time > spawnRate)
            {
                Instantiate(gangMember, gangMember.transform);
                spawnRate += 5.0f;
            }
            //if (spawnRate >= 10.0f)
            //{
            //    spawnRate = 5.0f;
            //}
            if (Input.GetKeyDown(KeyCode.E))
            {
            //    ownedTerritoryList.enabled = true;
            }
            if (Input.GetKey(KeyCode.R))
            {
         //       ownedTerritoryList.enabled = false;
            }

            objectMat = captureMat;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (other.gameObject.tag == "Player" && pc.reputation >= territoryCost)
            {
                gangOwned = true;
            }
        }
    }
}
