using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TPCWC;
public class Interaction : MonoBehaviour
{   
    public PlayableAsset scene;

    private void OnTriggerStay(Collider other) {
        
        if (Input.GetKeyDown("w") || FindObjectOfType<GameController>().gameData.interaction)
        {
            FindObjectOfType<PlayableDirector>().playableAsset = scene;
            FindObjectOfType<PlayableDirector>().Play();
        }
    }

}
