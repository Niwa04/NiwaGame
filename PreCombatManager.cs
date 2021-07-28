using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreCombatManager : MonoBehaviour
{

    public int index;
    public Transform canvas;

    public GameObject button;

    public CombatData combatData;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        combatData.stageCurrent = -1;
        combatData.personnages = new List<PersonnageData>();
    }

    void NextDialogue(){
        index++;
        canvas.GetChild(index).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && index < 5){
            NextDialogue();
        }

    }

    public void LoadChoixScene(){
        SceneManager.LoadSceneAsync("Demo/Combat/CombatChoix");
    }
}
