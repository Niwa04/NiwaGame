using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;


public class CrystalSelector : MonoBehaviour
{
     private GameObject currentPerso;
    private PersonnageData persoData;

     private GameController gc;
     public GameObject logSelectCrystal;

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


    public void ultime(){
      gc.activeP.GetComponent<ActionManager>().GetGambit().Ultime(); 
    }

    private void displaySelectCrystal(int i){
      if(gc.activeP.GetComponent<ActionManager>().isUseCristal)
        return;
        gc.activeP.GetComponent<ActionManager>().isUseCristal = true;
        string cry = transform.GetChild(i-1).GetChild(0).name;
      //  if(!cry.Contains("+"))
        //    return;
        gc.activeP.GetComponent<ActionManager>().GetGambit().useCrystalDirect(cry);
        GetComponent<AudioSource>().Play(); 

    }

    public void click1(){
        displaySelectCrystal(1);
    }

    public void click2(){
                displaySelectCrystal(2);

  
    }

    public void click3(){
      
        displaySelectCrystal(3);

    }

    public void click4(){
             displaySelectCrystal(4);

    }

    
    public void click5(){
             displaySelectCrystal(5);

    }

    
    public void click6(){
             displaySelectCrystal(6);

    }

    
    public void click7(){
             displaySelectCrystal(7);

    }

    
    public void click8(){
             displaySelectCrystal(8);

    }
  
}

