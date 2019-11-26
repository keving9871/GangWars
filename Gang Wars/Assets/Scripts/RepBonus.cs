using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepBonus : MonoBehaviour
{
    private PlayerController pc;

    public void Start()
    {
        pc = new PlayerController();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            pc.reputation += 5;
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player2")
        {
            pc.reputation += 5;
            Destroy(this.gameObject);
        }
    }
}
