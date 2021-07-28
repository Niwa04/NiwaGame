using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TPCWC;

using UnityEngine.Playables;
using UnityEngine.Timeline;
public class ActionManager : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Animator animator;
    private PersonnageDataManager personnageDataManager;
    private Gambit gambit;

    public float delayAction;
        private float delayAction2;

    public float timerAction;

    public float stoppingDist = 2.5f;

    public bool actionEnAttente;

    public bool wait;
    public bool enCombat;

    private float canalisationTimer;
    private float canalisationCount;
    public bool canalise;

    private GameObject line;
    private GameObject currentCible;
    private GameObject oldCible;
    private MyAction specialAttack;
    public bool isUseCristal;
    private CombatManager combatM;
    void Start()
    {
        //get trigger and override it's radius
        navAgent = GetComponent<NavMeshAgent>();
        gambit = GetComponent<Gambit>();

        animator = GetComponentInChildren<Animator>();
        personnageDataManager = GetComponentInChildren<PersonnageDataManager>();
        combatM = FindObjectOfType<CombatManager>();
        wait = false;
        navAgent.stoppingDistance = stoppingDist;
        timerAction = 0f;
        delayAction2 = delayAction *  FindObjectOfType<CombatManager>().speed;
        isUseCristal = false;
    }



    void Update()
    {
        DrawRay();
        if (wait)
        {
            return;
        }
        if ((animator.GetFloat("vertical") > 0.1))
        {
            drawLine();

            return;
        }
        if (wait || personnageDataManager.isDie)
        {
            return;
        }
        
        if(isUseCristal){
            return;
        }

        if (!actionEnAttente && enCombat && combatM.combat && gambit.cibleCurrent != null)
        {
            timerAction += Time.deltaTime;
            drawLine();
            if (timerAction >= delayAction2)
            {
                gambit.useCristal = false;
                lanceAction();
                timerAction = 0;
                delayAction2 = delayAction *  FindObjectOfType<CombatManager>().speed;

            }
        }

    }

    void DrawRay(){
        if(gambit.cibleCurrent == null)
            return;
        Transform p = GetComponentInChildren<LocationScriptCenter>().transform;
        Transform q = gambit.cibleCurrent.gameObject.GetComponentInChildren<LocationScriptCenter>().transform;

        Vector3 aaa = q.position-p.position ;
	    Debug.DrawRay(p.position, aaa, Color.blue);
    }

    IEnumerator stopCanalis(){
            if(gameObject.tag == "Player")
                attackSpecial();
            yield return new WaitForSeconds(6f);
            canalise = false;
            isUseCristal = false;

    }

    public void stopUseCristal(){
        isUseCristal = false;
        combatM.goCombat();
    }
    
    
    public void attackSpecial(){
        combatM.combat = true;
        //GetComponentInChildren<PlayableDirector>().Play();
        //StartCoroutine(endCinematique(GetComponentInChildren<PlayableDirector>().duration));
    }

    IEnumerator endCinematique(double aWait){
        Debug.Log("On va attendre "+aWait);
        yield return new WaitForSeconds((float) aWait);
        combatM.combat = true;
        isUseCristal = false;
    }

    private void FixedUpdate() {
        if(!enCombat || canalise || gambit.useCristal || !combatM.combat)
            return;
        if(gambit == null || gambit.hitScript == null)
            return;
        gambit.preparAction();
        drawLine();
    }
    public void restTimerAction()
    {
        timerAction = 0f;
                delayAction2 = delayAction *  FindObjectOfType<CombatManager>().speed;

    }

    void lanceAction()
    {

        if (!enCombat || !combatM.combat)
            return;

        actionEnAttente = true;

        float a = gambit.lanceAction();
        if(gambit.hitScript.action != null){
            animator.SetTrigger("Action");
        }
    
        personnageDataManager.talk(personnageDataManager.perso.frappeAudio);
       
        actionEnAttente = false;
      
    }


    public void talkSpecial(){
        personnageDataManager.talkSpecial();
    }

    public Gambit GetGambit()
    {
        return gambit;
    }
    public bool IsCanalise()
    {
        return canalise;
    }
    public bool GetWait()
    {
        return this.wait;
    }

    public void prepareAction(CompetenceData c,float i,string spawn, GameObject cible){
        if(c== null){
            return;
        }
        
        MyAction a = new MyAction(transform.gameObject,cible,c);
        if(spawn == ""){
           a.spawn = c.spawn;            
        }else
           a.spawn = spawn;
        gambit.hitScript.action = a;
        animator.SetFloat("ActionIndex",i);
    }

    public void prepareAction(string s,float i,string spawn, GameObject cible){
        CompetenceData c = FindAction.findCompetenceByNameIn(FindObjectOfType<CompetenceManager>().competences,s);
        if(c== null){
            Debug.Log("Probleme de recuperation de la competence "+s);
            return;
        }
        
        MyAction a = new MyAction(transform.gameObject,cible,c);
        if(spawn == ""){
           a.spawn = c.spawn;            
        }else
           a.spawn = spawn;
        gambit.hitScript.action = a;
        animator.SetFloat("ActionIndex",i);
    }

       public void prepareAction(int s,float i,string spawn, GameObject cible){
        CompetenceData c = FindObjectOfType<CompetenceManager>().competences[s];
        if(c== null){
            return;
        }

        MyAction a = new MyAction(transform.gameObject,cible,c);
        a.spawn = spawn;
        gambit.hitScript.action = a;
        animator.SetFloat("ActionIndex",i);
    }

    public IEnumerator useCristal(float time){
        actionEnAttente = true;
       // StartCanalisation(time);
        yield return new WaitForSeconds(time);
        timerAction = 0;
        actionEnAttente = false;
        isUseCristal = false; 
    }

    public void drawLine(GameObject cible){
        if(line != null)
            Destroy(line);
        GameObject newLine = FindObjectOfType<CombatManager>().line;
        newLine.GetComponent<DrawLineRender>().Point1 = transform;
        newLine.GetComponent<DrawLineRender>().Point3 = cible.transform;
        line = Instantiate(newLine);
    }

    public GameObject GetLine(){
        return line;
    }
    public void drawLine(){
        GameObject cible =  gambit.cibleCurrent;
        if(cible == currentCible)
            return;
        if(cible == null)
            Destroy(line);

        currentCible = cible;
        Destroy(line);
        try
        {
            GameObject newLine = null;
            if(cible.tag == gameObject.tag)
                newLine = FindObjectOfType<CombatManager>().lineAllie;
            else if (gameObject.tag == "Player")
                newLine = FindObjectOfType<CombatManager>().line2;
            else
                newLine = FindObjectOfType<CombatManager>().line;
            newLine.GetComponent<DrawLineRender>().Point1 = transform;
            newLine.GetComponent<DrawLineRender>().Point3 = cible.transform;
            newLine.GetComponent<DrawLineRender>().Point2Ypositio = UnityEngine.Random.Range(10f,13f);
            line = Instantiate(newLine);
        }
        catch (System.Exception)
        {    
            Destroy(line);
        }
    }
    
}