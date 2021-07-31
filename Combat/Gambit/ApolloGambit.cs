    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApolloGambit : Gambit
{
    private int index = 0;
    public float a;
    float dis ;
    public int phase = 1;

    public override void preparAction()
    {  
        if(phase == 1){
            phase1();
        }else
        {
            phase2();
        }
    }

    /**
        Phase 1 :
        50% -> Attack normal
        25% -> 
    */

    public void phase1(){
        GameObject cible = FindCible.findHeroLePlusProche(transform);
        string cri = GetComponent<CristalScript>().hasCPlus() ;

        cibleCurrent = cible;
        if(cibleCurrent != null)
            dis  = Vector3.Distance(cibleCurrent.transform.position, transform.position);

        if(cibleCurrent == null){
            Debug.Log("Apollo ne trouve pas de cible");
          return ;
        }

        if(dis > 3){
            seRaproche();
            actionEnAttente = hitScript.action;
            return;
        }   

        if(cri != null){
            Debug.Log("GO USER CRISTAL");
            useCrystalPhase1(cri);
            return;    
        }

        if(dis > 3){
            seRaproche();
        }else{
            attaqueLance();
        }
        actionEnAttente = hitScript.action;
       
    }

    public void phase2(){
        GameObject cible = FindCible.findHeroLePlusProcheSansObstacle(transform);
        cibleCurrent = cible;
        if(cibleCurrent == null){
          return ;
        }


        string cri = GetComponent<CristalScript>().hasCPlus() ;

        if(cri != null){
            useCrystalPhase1(cri);
            return;    
        }

        dis  = Vector3.Distance(cibleCurrent.transform.position, transform.position);
        if(dis > 3){
             attaqueDeLoin();
        }else
            attaqueLance();
        actionEnAttente = hitScript.action;
    }
    private void compTest(){
        //actionManager.prepareAction("Foudre",5f,"EnemySol",cibleCurrent);
       // actionManager.prepareAction("Boule De Feu",5f,"Devant",cibleCurrent);
        actionManager.prepareAction("Glace 1",5f,"EnemySol",cibleCurrent);
       // actionManager.prepareAction("SoinApollo",5f,"EnemySol",cibleCurrent);


    }
    private void attaque()
    {
        actionManager.prepareAction("Coup Sans Effet",2f,"Devant",cibleCurrent);
    }
    private void attaqueLance2()
    {
        actionManager.prepareAction("Coup Test",2f,"Devant",cibleCurrent);

//        actionManager.prepareAction("Coup Sans Effet",2f,"Devant",cibleCurrent);
    }
    private void attaqueLanceVortex()
    {
        actionManager.prepareAction("Aqua Vortex",4f,"EnemySol",cibleCurrent);
    }
    private void attaqueLance()
    {
        //actionManager.prepareAction("Aqua Vortex", 4f, "EnemySol2", cibleCurrent);
        actionManager.prepareAction("Coup Sans Effet",1f,"Devant",cibleCurrent);
    }
    private void coupDePied()
    {
        actionManager.prepareAction("Coup Sans Effet",0f,"Devant",cibleCurrent);
    }
    private void attaqueDeLoin()
    {
        actionManager.prepareAction("Projectile 5",4f,"Devant",cibleCurrent);
    }
    private void seRaproche(){
        companion.chase2(2f, cibleCurrent);
        hitScript.action = null;
    }

    public override MyAction useCrystal(string name)
    {   
        return null;
    }

    public void useCrystalPhase1(string name)
    {   
        switch (name)
        {
            case "Feu+":
                cibleCurrent = FindCible.findHeroLePlusLoinSansObstacle(transform);
                actionManager.prepareAction("Projectile 5",4f,"Devant",cibleCurrent);
                break;
            case "Eau+":
                actionManager.prepareAction("SoinApollo",5f,"Centre",gameObject);
                break;
            case "Vent+":
                cibleCurrent = FindCible.findHeroLePlusLoinSansObstacle(transform);
                actionManager.prepareAction("Aqua Vortex", 4f, "EnemySol2", cibleCurrent);
                break;
            case "Terre+":
                actionManager.prepareAction("Coup 3", 0f, "", cibleCurrent);
                break;
            default:
                break;
        }
        useCristal = true;
        GetComponent<CristalScript>().deleteCrystal(name);
        StartCoroutine(actionManager.useCristal(hitScript.action.competence.timeCanalisation));
        actionEnAttente = hitScript.action;
    }

    public void useCrystalPhase2(string name)
    {   
       GameObject cible = FindCible.findHeroLePlusProcheSansObstacle(transform);
        cibleCurrent = cible;
        if(cibleCurrent == null){
          return  ;
        }
        dis  = Vector3.Distance(cibleCurrent.transform.position, transform.position);
          switch (name)
        {
            case "Feu+":
                actionManager.prepareAction("Projectile 5",4f,"Devant",cibleCurrent);
                break;
            case "Eau+":
                actionManager.prepareAction("SoinApollo",5f,"Centre",gameObject);
                break;
            case "Vent+":
                Debug.Log("Attaque Special");
                break;
            case "Terre+":
                actionManager.prepareAction("SoinApollo",5f,"Centre",gameObject);
                break;
            default:
                break;
        }
        actionEnAttente = hitScript.action;
        
        hitScript.cristalName = name;
    }

}
