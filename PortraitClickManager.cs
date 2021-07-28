using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;
public class PortraitClickManager : MonoBehaviour
{

    public void click(int i){
        InputManager input = FindObjectOfType<GameController>().players[i];
        input.GetComponent<CompanionInput>().followCible();
        //input.transform.GetChild(0).transform.GetChild(0).gameObject.active = true;

        try
        {
           moveCamera(input);         
        }
        catch (System.Exception)
        {
                    }
        
        foreach (GameObject item in FindObjectOfType<CombatManager>().buttons)
        {
            item.transform.GetChild(0).gameObject.active = false;
        }        
        FindObjectOfType<CombatManager>().buttons[i].transform.GetChild(0).gameObject.SetActive(true);
        FindObjectOfType<CombatManager>().buttons[i].transform.GetChild(0).gameObject.SetActive(true);
    }

    public void moveCamera( InputManager input){
        Debug.Log("Strategique "+FindObjectOfType<CameraControllerStrategie>().transform.GetChild(0).transform.position);
       // Debug.Log("Normal "+input.transform.GetChild(0).transform.GetChild(0).GetComponent<Camera>().transform.position);

      //  FindObjectOfType<CameraControllerStrategie>().gameObject.GetComponent<CameraControllerStrategie>().enabled = false;

        Vector3 p = input.transform.GetChild(0).transform.GetChild(0).GetComponent<Camera>().transform.position;
        FindObjectOfType<CameraControllerStrategie>().gameObject.transform.GetChild(0).gameObject.transform.position = p;
        FindObjectOfType<CameraControllerStrategie>().gameObject.transform.GetChild(0).gameObject.transform.rotation = input.transform.GetChild(0).transform.GetChild(0).GetComponent<Camera>().transform.rotation;
     //   FindObjectOfType<CameraControllerStrategie>().gameObject.GetComponent<CameraControllerStrategie>().enabled = true;

    }



    public void click1(){
      InputManager input = FindObjectOfType<GameController>().players[0];
      input.GetComponent<CompanionInput>().followCible();
         foreach (GameObject item in FindObjectOfType<CombatManager>().buttons)
        {
            item.transform.GetChild(0).gameObject.active = false;
        }
        FindObjectOfType<CombatManager>().buttons[0].transform.GetChild(0).gameObject.active = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
