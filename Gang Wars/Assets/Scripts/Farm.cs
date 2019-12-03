//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Farm : MonoBehaviour
//{
//    //Variables:
//    public TerritoryController tc;
//    public PlayerController pc;
//    public float farmSpawnRate;

//    void Start() 
//    { 
////        tc.gangOwned = false;
//        tc.territoryCost = 150000f;
//        farmSpawnRate = Time.time + 3.75f;
//        //tc.captureRate = 500f;
//    }

//    void Update()
//    {
//     //   if (tc.gangOwned == true)
//        {
//            if(Time.time > farmSpawnRate)
//            {
//                Instantiate(tc.gangMember);
//                farmSpawnRate += 3.75f;
//            }
//        }

//    }

//    private void OnTriggerStay(Collider other)
//    {
//        if (other.gameObject.tag == "player" && pc.reputation >= tc.territoryCost)
//        {
//            //tc.captureRate -= 1f;

//           // if (tc.captureRate <= 0f)
//            //{
//           //     tc.gangOwned = true;
//           // }

//        }
//    }
//}

