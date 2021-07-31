using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using TPCWC;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Saut : MonoBehaviour
{
    public PlayableAsset timeline;


     private void OnTriggerStay(Collider other) {
        
        if (Input.GetKeyDown("w") && FindObjectOfType<GameController>().activeP.gameObject.name == "Jiyeon" && other.gameObject.name=="Jiyeon")
        {
            Debug.Log("Jiyeon : Je saut !");
            FindObjectOfType<GameController>().activeP.GetComponent<PlayableDirector>().playableAsset = timeline;
            FindObjectOfType<GameController>().activeP.GetComponent<NavMeshAgent>().enabled  = false;	
            FindObjectOfType<GameController>().activeP.GetComponent<PlayableDirector>().Play();
        }
    }
}
