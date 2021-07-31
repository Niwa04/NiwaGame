using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class Lire : MonoBehaviour
{
    public string contenue;
    
    public GameObject lumiere;
    public Lire prochainLivre;
    public GameObject obj;
    public PlayableAsset timeline;

    private bool dejaLu;

    // Start is called before the first frame update
    void Start()
    {
        dejaLu = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        
        if (!dejaLu && Input.GetKeyDown("w") && FindObjectOfType<GameController>().activeP.gameObject.name == "Sakura")
        {
            Debug.Log("Sakura : Je lis !");
            dejaLu = true;
            StartCoroutine(FindObjectOfType<UiManager>().afficheDialogue(contenue,5f));
            if(obj != null){
                Debug.Log("Activation de l'objet "+obj.name);
                obj.SetActive(true);
            }

            if(timeline != null){
                Debug.Log("Cinematique suite a la lecture");
                FindObjectOfType<GameController>().playableDirector.playableAsset = timeline;
                FindObjectOfType<GameController>().playableDirector.Play();
                transform.GetChild(0).gameObject.SetActive(false);
                return;
            }
            if(prochainLivre != null){
                prochainLivre.lumiere.SetActive(true);
                prochainLivre.enabled = true;
            }
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
