using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TPCWC;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public abstract class Gambit : MonoBehaviour {

    private NavMeshAgent navAgent;
    protected Animator animator;
    protected PersonnageData perso;
    public bool useCristal;

    private PlayableDirector timeline;
 

    public HitScript hitScript;

    protected ActionManager actionManager;

    protected CompanionInput companion;
    protected CompetenceManager competenceManager;
    protected MyAction actionEnAttente;

    public GameObject cibleCurrent;
    private void Start() {
       
            navAgent = GetComponent<NavMeshAgent>();

            animator = GetComponentInChildren<Animator>();
          
            hitScript = GetComponentInChildren<HitScript>();

            perso = GetComponentInChildren<PersonnageDataManager>().perso;

            actionManager = GetComponent<ActionManager>();
            competenceManager = FindObjectOfType<CompetenceManager>();

            companion    =  GetComponent<CompanionInput>();
            timeline = GetComponentInChildren<PlayableDirector>();
    }

    public void Ultime(){
        timeline.Play();

    }
    public abstract MyAction useCrystal(string o);

    //public abstract void useCrystalDirect(string o);

    public abstract void preparAction();

   public void useCrystalDirect(string name){
        useCrystal(name);
        StartCoroutine(actionManager.useCristal(hitScript.action.competence.timeCanalisation));
     }

       public float lanceAction(){
        if(hitScript.action ==  null)
            return -1;
       return hitScript.action.competence.timeCanalisation;
       
    }

}
