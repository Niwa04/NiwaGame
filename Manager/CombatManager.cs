using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;
using UnityEngine.UI;
using TMPro;


public class CombatManager : MonoBehaviour
{

    public bool combat;
    GameObject[] players;
    GameObject[] monstres;

    public float speed = 1;

    public AudioClip changeStrategieAudio;

    public GameObject line;
        public GameObject line2;

    public GameObject lineAllie;
    public GameObject cercleSelect;

    public Button[] buttonStrategie;
    
    public GameObject strategieDescription;

    public CombatData combatData;

    private GameObject stageEnemy;

    public Transform[] positions;

    public GameObject[] buttons;
    public bool init;
    public AudioSource audioSourceSpecial;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        monstres = GameObject.FindGameObjectsWithTag("Enemy");
        if(init)
            initStage();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)){ 
           
           changeCurrentPersoStrategie();
        }
    }

    public void hit(){
        FindObjectOfType<GameController>().activeP.GetComponentInChildren<HitScript>().Hit();
    }

    public void goCombat(){
        Debug.Log("GOCOMBAT");
        StartCoroutine(reprendCombat());
    }

    IEnumerator reprendCombat(){
        yield return new WaitForSeconds(0.2f);
        combat = true;
    }

     public void initStage(){
         int current = combatData.stageCurrent;
         CombatStageData stage = combatData.stages[current];
         try
         {
                     stageEnemy = Instantiate(stage.enemys, Vector3.zero, Quaternion.identity);

         }
         catch (System.Exception)
         {
             
         }
        initPersos();
    }

    public void initPersos(){
        int i = 0;
        List<InputManager> imputs = new List<InputManager>();
        Debug.Log("Initiation de "+combatData.personnages.Count+" personnage");
        foreach(PersonnageData p in combatData.personnages){
            GameObject obj = Instantiate(p.model3D);
            obj.transform.position = positions[i].transform.position;
            imputs.Add(obj.GetComponent<InputManager>());
            initButton(i,obj);  

            i++;

        }
        FindObjectOfType<GameController>().activeP = imputs[0];
        FindObjectOfType<GameController>().players = imputs.ToArray();
    }

    public void initButton(int i, GameObject obj){
        Debug.Log("Init button de "+combatData.personnages[i].name);
        GameObject btn = buttons[i];
        btn.SetActive(true);
        btn.transform.GetChild(3).GetComponent<Image>().sprite = combatData.personnages[i].sprite;
        btn.transform.GetChild(2).GetComponent<Image>().sprite = combatData.personnages[i].strategieCurrent.logo;
        obj.GetComponent<PersonnageDataManager>().lifeBarre = btn.transform.GetChild(1).GetComponent<Image>();
        obj.GetComponent<PersonnageDataManager>().Strategie = btn.transform.GetChild(2).GetComponent<Image>();
    }



    public void setCombat(){
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            Debug.Log(item.transform.name+" pret au combat ");
          item.GetComponent<ActionManager>().enCombat = true;
        }
          foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            item.GetComponent<ActionManager>().enCombat = true;

        }
    }

    public void checkIfEnd(){
        Debug.Log("Je ckecj si c'est la end");
    }

    public void changeCurrentPersoStrategie(){
       GameObject a = FindObjectOfType<GameController>().activeP.transform.gameObject;
       PersonnageDataManager perso = a.GetComponent<PersonnageDataManager>();
       string name = perso.perso.strategieCurrent.name;
        Strategie[] strats = perso.perso.strategies;
        if(strats[0].name == name){
            perso.perso.strategieCurrent = strats[1];
            changeLogo();
                                    a.GetComponent<ActionManager>().GetGambit().preparAction();

            a.GetComponent<ActionManager>().drawLine();
            return;
        }
         if(strats[1].name == name){
            perso.perso.strategieCurrent = strats[2];
                        changeLogo();
                                                a.GetComponent<ActionManager>().GetGambit().preparAction();

            a.GetComponent<ActionManager>().drawLine();

            return;
        }
         if(strats[2].name == name){
            perso.perso.strategieCurrent = strats[0];
                        changeLogo();
                        a.GetComponent<ActionManager>().GetGambit().preparAction();
            a.GetComponent<ActionManager>().drawLine();

            return;
        }
        

    }

    private void changeLogo(){
               GameObject a = FindObjectOfType<GameController>().activeP.transform.gameObject;

               PersonnageDataManager perso = a.GetComponent<PersonnageDataManager>();


            perso.Strategie.sprite = perso.perso.strategieCurrent.logo;
                      GetComponent<AudioSource>().PlayOneShot(changeStrategieAudio,0.5f);  

        
    }

    public void setStrategie1(){
        GameObject a = FindObjectOfType<GameController>().activeP.transform.gameObject;
       PersonnageDataManager perso = a.GetComponent<PersonnageDataManager>();
       string name = perso.perso.strategieCurrent.name;
        Strategie[] strats = perso.perso.strategies;
        perso.perso.strategieCurrent = strats[0];
        changeLogo();

        updateUIbuttonStrat(0);
    }

     public void setStrategie2(){
        GameObject a = FindObjectOfType<GameController>().activeP.transform.gameObject;
       PersonnageDataManager perso = a.GetComponent<PersonnageDataManager>();
       string name = perso.perso.strategieCurrent.name;
        Strategie[] strats = perso.perso.strategies;
                    perso.perso.strategieCurrent = strats[1];
                                            changeLogo();
        updateUIbuttonStrat(1);


    }

     public void setStrategie3(){
        GameObject a = FindObjectOfType<GameController>().activeP.transform.gameObject;
       PersonnageDataManager perso = a.GetComponent<PersonnageDataManager>();
       string name = perso.perso.strategieCurrent.name;
        Strategie[] strats = perso.perso.strategies;
                    perso.perso.strategieCurrent = strats[2];
                                            changeLogo();
        updateUIbuttonStrat(2);


    }

    public void updateUIbuttonStrat(int k){
     /*   for (int i = 0; i < 3; i++)
        {
           buttonStrategie[i].GetComponent<Image>().color  =  new Color(70f/255f, 70/255f, 70/255f,137f/255f);
        }
                   buttonStrategie[k].GetComponent<Image>().color  =  new Color(156f/255f, 5f/255f, 0f/255f,174f/255f);
                    GameObject a = FindObjectOfType<GameController>().activeP.transform.gameObject;
          PersonnageDataManager perso = a.GetComponent<PersonnageDataManager>();
       strategieDescription.GetComponent<Text>().text = perso.perso.strategieCurrent.description;*/
    }


    public void allStopPerso(){
        foreach(GameObject item in players){
            item.GetComponent<ActionManager>().enCombat = false;
        }
         foreach(GameObject item in monstres){
            item.GetComponent<ActionManager>().enCombat = false;
        }
    }

    public void allStartPerso(){
        foreach(GameObject item in players){
            item.GetComponent<ActionManager>().enCombat = true;
        }
         foreach(GameObject item in monstres){
            item.GetComponent<ActionManager>().enCombat = true;
        }
    }

    
}
