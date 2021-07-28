using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TPCWC;
public class MySceneManager : MonoBehaviour
{
    public GameObject image;
     public GameObject loadingScreen;
     public Slider slider;
     public Text text;
     public GameData gameData;
    private string Chapitre1 = "NiwaGame/Scene/Chapitre 1/Chapitre 1 - 1";
    // Start is called before the first frame update
    void Start()
    {
                Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadSceneCombat(){
         SceneManager.LoadSceneAsync(3);

    }

     public void loadChapitre1(){
          loadingScreen.SetActive(true);

          Debug.Log("Chapitre 1 go");
          //FindObjectOfType<AllDataScript>().allData.personnages[0].position = new Vector3(0,0,0);
          //image.SetActive(true);
          gameData.newGame = true;
          gameData.end = false;

          StartCoroutine(Loadtest());
    }

    IEnumerator Loadtest(){
          AsyncOperation operation =  SceneManager.LoadSceneAsync(Chapitre1);

          while( ! operation.isDone){
               float progress = Mathf.Clamp01(operation.progress/ .9f);
               slider.value = progress;
               text.text = progress*100f+"%";
               yield return null;
          }

    }

     public void loadCinematique(){
        image.SetActive(true);
         SceneManager.LoadSceneAsync(5);

    }

    public void loadScene(int i){
         SceneManager.LoadSceneAsync(i);

    }

    public void quitAppli(){
         Application.Quit();  
    }

     public void Active(GameObject g){
          g.SetActive(true);
     }

     public void Desactive(GameObject g){
          g.SetActive(false);
     }

}
