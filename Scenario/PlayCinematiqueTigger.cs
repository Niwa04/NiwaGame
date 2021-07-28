using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slate;
using TPCWC;
public class PlayCinematiqueTigger : MonoBehaviour
{
    public string eventName;
    public Cutscene scene;
    private bool played;

    public List<string> aJouerApresEvenements;

    private EvenementManager evenementManager;
    // Start is called before the first frame update
    void Start()
    {
        played = false;
       evenementManager = FindObjectOfType<EvenementManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other) {
        bool canplay = canPlayCinematique();
        Debug.Log("canplay = "+canplay);
        if(!canplay)
            return;
        if(canplay && other.gameObject.tag == "Player" && FindObjectOfType<GameController>().activeP.transform.name == other.transform.name && !played){
            scene.Play();
            played = true;
            evenementManager.addEvent(eventName);
        }
    }

    private bool canPlayCinematique(){
        if(!FindObjectOfType<GameController>().gameData.cinematiqueActive)
            return false;
        if(evenementManager.containsEvent(eventName)){
            Debug.Log("L'evenement n'est pas passe : "+eventName+" : "+evenementManager.containsEvent(eventName));
            return false;
        }
        if(!evenementPasseSontPresent())
            return false;
        return true;
    }

    private bool evenementPasseSontPresent(){
        foreach (var item in aJouerApresEvenements)
        {
            if(!evenementManager.containsEvent(item))
                return false ; 
        }
        return true;
    }
}
