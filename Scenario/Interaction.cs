using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TPCWC;
public class Interaction : MonoBehaviour
{   
    public PlayableAsset scene;
    public GameObject obj;
    public string dialogue;
    bool dejaJouer;
    private void Start() {
        dejaJouer = false;
    }

    private void OnTriggerStay(Collider other) {
        
        if (!dejaJouer && (Input.GetKeyDown("w") || FindObjectOfType<GameController>().gameData.interaction) && other.gameObject == FindObjectOfType<GameController>().activeP.gameObject)
        {
            dejaJouer = true;
            StartCoroutine(FindObjectOfType<UiManager>().afficheDialogue(dialogue,5f));

            if(obj != null){
                Debug.Log("Activation de l'objet "+obj.name);
                obj.SetActive(true);
            }
            if(scene != null){
                Debug.Log("Debut de l'animation "+scene);

                FindObjectOfType<GameController>().playableDirector.playableAsset = scene;
                FindObjectOfType<GameController>().playableDirector.Play();
            }
            FindObjectOfType<GameController>().InitObjectInteractible();
            FindObjectOfType<GameController>().afficheCompetenceSpecialDuPerso();
        }
    }

}
