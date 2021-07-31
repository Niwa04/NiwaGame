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
    }

    public void hit(){
        FindObjectOfType<GameController>().activeP.GetComponentInChildren<HitScript>().Hit();
    }

    public void goCombat(){
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
          item.GetComponent<ActionManager>().enCombat = true;
        }
          foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            item.GetComponent<ActionManager>().enCombat = true;
        }
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
