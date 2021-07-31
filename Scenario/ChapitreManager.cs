using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using TPCWC;
using System.IO;
using UnityEngine.UI;

using Slate;
public class ChapitreManager : MonoBehaviour
{

    public bool chargeDialogue;
    public DialoguesData dialoguesData;
    public List<string> evenements;

    public float count;
    public float timer;
    public Text text;
    int indexDialogue;
    public GameObject mainCamera;

    public Cutscene  cutscene;    

    public bool playCutSceneOnAwake;
    
    public List<GameObject> checkpoints;

    private bool end;    

    private void Awake() {
        if(FindObjectOfType<GameController>().gameData.end){
            SceneManager.LoadScene("NiwaGame/End");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
        //Cursor.visible = false;
        indexDialogue = 0;
        if(chargeDialogue)
            testReadFile();
        if(FindObjectOfType<GameController>().gameData.newGame){
            FindObjectOfType<EvenementManager>().clearEvents();
           // cutscene.Play();
            FindObjectOfType<GameController>().gameData.newGame = false;
            ArriverApresCombat();
        }else{
            ArriverApresCombat();
        }
    }

    // Update is called once per frame
    void Update()
    {    
        if(FindObjectOfType<GameController>().activeP.vertical > 0.9f){
            count += Time.deltaTime;
            if(count > timer){
                count = 0;
                int rdm =  UnityEngine.Random.Range(0,100);
                if(rdm < 30){
                   goCombat();
                }
            }
        }
        
    }


    void goCombat(){
        if(!FindObjectOfType<GameController>().gameData.combatActive)
            return;
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {   
            item.GetComponent<PersonnageDataManager>().perso.position = item.transform.localPosition;
            item.GetComponent<PersonnageDataManager>().perso.rotation = item.transform.localRotation;
        }
            FindObjectOfType<GameController>().gameData.ScenePrecedente = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadSceneAsync("NiwaGame/Combat/Squelette CMB");
    }

    void goCombatEnd(){


        SceneManager.LoadSceneAsync("NiwaGame/Combat/Apollo CMB");

    }

	void testReadFile ()
	{
		string fileName = "C:/fichier.txt";
		dialoguesData.dialogues = File.ReadAllLines(fileName);
        Debug.Log(dialoguesData.dialogues.Length);
	}

    public void displayNextDialogue(){
      
        text.text = dialoguesData.dialogues[indexDialogue];
        indexDialogue++;
        if(text.text == "")
            displayNextDialogue();
    }
    public void clearDialogue(){
         text.text = "";
    }

    public void close(GameObject obj){
        obj.SetActive(false);
    }

    public void switchAllModel(){
        var l = new List<GameObject>();
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {   
            try
            {
                if(item.active){
                    l.Add(item);
                    item.GetComponent<PersonnageDataManager>().perso.position = item.transform.position;
                }
            }
            catch (System.Exception)
            {}
        }
        foreach(var item in l){
            try
            {    
                item.GetComponent<SwitchPersoType>().switchAction();
            }
            catch (System.Exception)
            {}
        }
    }

    public void lanceCombat(int combat){
        FindObjectOfType<GameController>().gameData.end = true;
        switchAllModel();
        goCombatEnd();
    }


    public void ArriverApresCombat(){
       
         foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {   
            item.GetComponent<NavMeshAgent>().enabled  = false;
            item.transform.localPosition = item.GetComponent<PersonnageDataManager>().perso.position;
            item.transform.localRotation = item.GetComponent<PersonnageDataManager>().perso.rotation;
			item.GetComponent<NavMeshAgent>().enabled  = true;
        }
    }
}