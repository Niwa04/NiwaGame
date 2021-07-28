using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyAction {
    
    public GameObject cible;
    public GameObject lanceur;
    public CompetenceData competence;

    public string spawn;

    
 //  CompetenceManager competenceManager = GameObject.Find("CompetenceManager").GetComponent<CompetenceManager>();


    public MyAction(GameObject lanceur, GameObject cible, CompetenceData competence){
        this.cible = cible;
        this.lanceur = lanceur;
        this.competence = competence;
        this.spawn = "";
    }




  public int execute2(){
   //   lanceur.GetComponent<Animator>().SetTrigger("goAttack");
       return 0;
    }



     public override string ToString()
    {
         return "Action : " + lanceur.name + " lance " + competence.name + " sur " + cible.name;
    }

}
