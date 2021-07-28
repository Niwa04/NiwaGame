using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPersoType : MonoBehaviour
{
    public GameObject switchWith;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          if(Input.GetKeyDown("b")){
              switchAction();
        }
    }

    public void switchAction(){
        transform.gameObject.SetActive(false);
        switchWith.transform.position = transform.position;
        switchWith.transform.rotation = transform.rotation;
        switchWith.SetActive(true);
        
    }


    
    /* void switchAction(){
       transform.gameObject.SetActive(false);
        switchWith.GetComponent<PersonnageDataManager>().perso.position = transform.position;
        switchWith.transform.rotation = transform.rotation;
        switchWith.SetActive(true);
        Debug.Log("  switchWith "+switchWith.name);
    }*/

    
}
