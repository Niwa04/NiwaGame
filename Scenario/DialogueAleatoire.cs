using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;
public class DialogueAleatoire : MonoBehaviour
{
    public string dialogue;
    public float time;
    bool dejaVu;
    // Start is called before the first frame update
    void Start()
    {
        dejaVu = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (!dejaVu && FindObjectOfType<GameController>().activeP.gameObject == other.gameObject){
            StartCoroutine(FindObjectOfType<UiManager>().afficheDialogue(dialogue,time));
            dejaVu = true;
        }

    }
}
