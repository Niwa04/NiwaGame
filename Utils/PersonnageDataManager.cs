using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TPCWC;

public class PersonnageDataManager : MonoBehaviour
{
    
    public PersonnageData perso;

     
    public Image lifeBarre;

    public Image Strategie;


    public bool isDie;

 //   public Caracteristique caracteristiqueCurrent;

    public int hpCurrent;

    public int shieldValue;

    public AudioClip audioDeath;

    private void Start() {
        hpCurrent = perso.hpMax;
        if(perso.strategies != null && perso.strategies.Length >0)
            perso.strategieCurrent = perso.strategies[0];
            try
            {
               FindObjectOfType<UiManager>().UpdateButtonStrategie();

            }
            catch (System.Exception)
            {
                
            }

        initCaracteristique();
        if(FindObjectOfType<UiManager>() == null){
                            GetComponent<NavMeshAgent>().enabled  = false;

                          transform.rotation = perso.rotation;

                            GetComponent<NavMeshAgent>().enabled  = true;

        }
   //    caracteristiqueCurrent = (Caracteristique) perso.caracteristiqueCurrent.MemberwiseClone();
    }

   public void dammage(MyAction a){
       int puissance = a.competence.puissance;
       PersonnageData lanceur = a.lanceur.GetComponent<PersonnageDataManager>().perso;
     
       if(hpCurrent <= 0)
            return;

        appliDammage(a);
        //perso.hpCurrent -= (puissance + frc)/2;
       try
       {
         
        if(!a.lanceur.GetComponent<ActionManager>().wait){
                //    if(gameObject.tag == "Player")
             a.lanceur.GetComponent<CristalScript>().tryToAddCrystal();
        }

       }
       catch (System.Exception)
       {
       }

        int rdm =  UnityEngine.Random.Range(0,100);
       
        if(a.competence.canHit > rdm ){
            if(!GetComponent<ActionManager>().IsCanalise() && !GetComponent<ActionManager>().isUseCristal && hpCurrent > 0)
            {
                GetComponentInChildren<Animator>().SetTrigger("hit");
                talk(perso.hitAudio);
            }
           
        }
        if(hpCurrent <= 0 ){
            hpCurrent = 0;
            die();
        }
          updateUI();
        FindObjectOfType<BarreLifeAllMonstreScript>().updateProgression();
    }

    void appliDammage(MyAction a){

        int frc = a.lanceur.GetComponent<PersonnageDataManager>().perso.getCurrentCaracteristique().frc;
        int def = a.cible.GetComponent<PersonnageDataManager>().perso.getCurrentCaracteristique().def; 
        int puissance = a.competence.puissance + frc - def ;
       // Debug.Log(a.lanceur.transform.name+" inflige "+puissance+" a "+a.cible.transform.name+" avec "+a.competence.name);
        if(shieldValue > 0){
            shieldValue -= puissance;
            if(shieldValue <= 0){
                hpCurrent += shieldValue;
                shieldValue = 0;
                deleteBuff("Shield");
                GetComponentInChildren<Animator>().SetTrigger("hit");

            }
        }else{
            hpCurrent -= (puissance);
        }

    }   

    void deleteBuff(string s){
        BuffScript[] bs = GetComponents<BuffScript>();
        foreach (BuffScript item in bs)
        {
            if(item.type == "Shield"){
                Destroy(item.obj);
                Destroy(item);
                Debug.Log("ca detruit ");
            }
        }
    }

    public void heal(MyAction action){
        int puissance = action.competence.puissance + action.lanceur.GetComponent<PersonnageDataManager>().perso.getCurrentCaracteristique().mag;
        Debug.Log(action.lanceur.transform.name+" guerrie "+puissance+" a "+action.cible.transform.name);
        
        hpCurrent += puissance;
        if(hpCurrent > perso.hpMax)
            hpCurrent = perso.hpMax;
        FindObjectOfType<BarreLifeAllMonstreScript>().updateProgression();
                  updateUI();

    }

    void die(){
        isDie = true;
        GetComponentInChildren<Animator>().SetTrigger("death");
        if(gameObject.tag == "Enemy")
                Destroy(gameObject, 2f);
        try
        {
            Destroy(GetComponent<ActionManager>().GetLine());
            GetComponent<CompanionInput>().pointToFollow = null;
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<CompanionInput>().canFollow = false;
            try
            {
                
             GetComponent<AudioSource>().clip = audioDeath;
            GetComponent<AudioSource>().Play();  
            }
            catch (System.Exception)
            {
                
            }
          //  FindObjectOfType<CombatManager>().checkIfEnd();
        }
        catch (System.Exception)
        {
                    }
    }

    private void Update() {
              if( Input.GetKeyDown("r")){
                  hpCurrent = perso.hpMax;
              }

    }

    void updateUI(){
        if(!lifeBarre)
            return;
        float a = (float) hpCurrent / (float) perso.hpMax;
        lifeBarre.fillAmount = a; 
    }

    void initCaracteristique(){
        perso.caracteristiqueCurrent.frc = perso.caracteristique.frc;
        perso.caracteristiqueCurrent.def = perso.caracteristique.def;
        perso.caracteristiqueCurrent.mag = perso.caracteristique.mag;

    }

    public void talk(AudioClip audio){
        try
        {
            GetComponent<AudioSource>().Stop();  
            GetComponent<AudioSource>().clip = audio ;
            GetComponent<AudioSource>().Play(); 

        }
        catch (System.Exception)
        {
        }
    }

    public void talkSpecial(){
          int aud =  UnityEngine.Random.Range(0,perso.specialAttackAudio.Length);
          
        FindObjectOfType<CombatManager>().audioSourceSpecial.clip = perso.specialAttackAudio[aud];
        FindObjectOfType<CombatManager>().audioSourceSpecial.Play();
    }

    public void talk(AudioClip[] audios){
        if(audios.Length == 0)
            return;
        int aud =  UnityEngine.Random.Range(0,audios.Length);
        talk(audios[aud]);

    }

}
