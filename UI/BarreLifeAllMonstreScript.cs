using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TPCWC;    
public class BarreLifeAllMonstreScript : MonoBehaviour
{

    public Image lifeBarre;
    public int HPMax;
    bool init ;
    bool end;
    // Start is called before the first frame update
    void Start()
    {
        lifeBarre = GetComponent<Image>();
        init = false;
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            StartCoroutine(finCombat());    
        }
    }


    void initHPMAX(){
        HPMax = 0;
         foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
          {
              HPMax += item.GetComponent<PersonnageDataManager>().perso.hpMax;
          }
          init = true;
    }
    public void updateProgression(){
        if(!init)
          initHPMAX();
        int HPCurrent = 0;
        foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            HPCurrent += item.GetComponent<PersonnageDataManager>().hpCurrent;
        }
        if(HPCurrent == 0 && !end ){
            end = true;
            StartCoroutine(finCombat());
        }
        float a = (float) HPCurrent / (float) HPMax;
        lifeBarre.fillAmount = a;        
    }
    IEnumerator finCombat(){
        yield return new WaitForSeconds(3f);
        Destroy(FindObjectOfType<CombatManager>().cercleSelect);
        FindObjectOfType<CinematiqueCombat>().endCombat();
        yield return new WaitForSeconds(7f);
        SceneManager.LoadSceneAsync( FindObjectOfType<GameController>().gameData.ScenePrecedente);
    }    

}
