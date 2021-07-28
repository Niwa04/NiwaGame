using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;

public class ButtonSelector : MonoBehaviour
{

         public GameObject logSelectCrystal;

    private GameObject currentPerso;
    private PersonnageData persoData;

     private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        currentPerso = gc.activeP.gameObject;
        persoData = currentPerso.GetComponent<PersonnageDataManager>().perso;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void displaySelectCrystal(int i){
        if(logSelectCrystal.transform.childCount == 1)
            GameObject.Destroy(logSelectCrystal.transform.GetChild(0).gameObject);
        GameObject cry = Instantiate(transform.GetChild(i).GetChild(0).gameObject,logSelectCrystal.transform);
        cry.name = transform.GetChild(i).GetChild(0).name;
        cry.transform.SetParent(logSelectCrystal.transform); 
        cry.transform.position = logSelectCrystal.transform.position;
        cry.transform.rotation = transform.GetChild(i).GetChild(0).rotation;
    }
}
