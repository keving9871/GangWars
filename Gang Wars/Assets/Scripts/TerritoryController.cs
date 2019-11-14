using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerritoryController : MonoBehaviour
{
    //Variables:
    public float territoryProgress;
    public bool gangOwned;

    void Start()
    {
        territoryProgress = 0f;
        gangOwned = false;
    }

    void Update()
    {
        if (territoryProgress >= 5000f)
        {
            gangOwned = true;
        }
    }

    private void FixedUpdate()
    {
        if (territoryProgress >= 0)
        territoryProgress -= 1f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            territoryProgress += 5f;
        }
    }
}
