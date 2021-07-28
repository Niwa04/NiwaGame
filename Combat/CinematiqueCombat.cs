using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TPCWC;
public class CinematiqueCombat : MonoBehaviour
{

    public PlayableDirector timeline;
    public CameraController cam;

    // Start is called before the first frame update
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();

    }

    void Update() {
      
    }

    // Update is called once per frame
   public void endCombat()
    {
          foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
          {   
                item.GetComponent<CompanionInput>().navAgent.isStopped = true;
                item.GetComponent<CompanionInput>().navAgent.speed = 0;
                item.GetComponent<ActionManager>().enCombat = false;
                item.GetComponent<CompanionInput>().vertical = 0;
                item.GetComponent<CompanionInput>().chase2(10f,item);
                item.GetComponent<CompanionInput>().gameObject.transform.localPosition = item.GetComponent<CompanionInput>().transformDebut;
                item.transform.LookAt(FindObjectOfType<CameraEndLocation>().gameObject.transform);

          }
        timeline.Play();
          
    }
}
