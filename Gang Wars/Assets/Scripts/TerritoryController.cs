using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerritoryController : MonoBehaviour
{
    //Variables:
    public PlayerController pc;
    public float territoryCost;
    public float spawnRate, captureRate;
    public bool gangOwned;
   // public Text ownedTerritoryList;
    public GameObject gangMember;
    public Material captureMat;
    private Material objectMat;
    bool readyToSpawn;

    //  REPUTATION IS HOW MUCH CURRENCY THE PLAYER HAS
    //  TERRITORY COST IS HOW MUCH THE PLAYER NEEDS

    void Start()
    {
        objectMat = GetComponent<Renderer>().material;
        //captureRate = 500.0f;
        gangOwned = false;
        //ownedTerritoryList.enabled = false;

        if (tag == "default")
        {
            territoryCost = 1f;
            spawnRate = Time.deltaTime + 5.0f;
        }

        readyToSpawn = true;
    }

    void Update()
    {
        if (gangOwned == true)
        {
            if (readyToSpawn == true)
            {
                InvokeRepeating("Spawn", 0, 5);
                readyToSpawn = false;
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

    private void Spawn()
    {
        Instantiate(gangMember, transform.position, Quaternion.identity);
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
