using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;
using TPCWC;

public class RalentiMode : MonoBehaviour
{
    bool ralentis;
    // Start is called before the first frame update
    void Start()
    {
        ralentis = false;
    }

    // Update is called once per frame
    void Update()
    {
    
           if (Input.GetKeyDown("space"))
        {
            ralentis = !ralentis;
            if(ralentis){
                GetComponent<Image>().enabled = true;
                FindObjectOfType<GameController>().time = 0.15f;
            }else{
                GetComponent<Image>().enabled = false;
                FindObjectOfType<GameController>().time = 1f;
            }
          
        }



           if (Input.GetKeyDown(KeyCode.Tab))
        {
            FindObjectOfType<GameController>().time =2f;
        }
         if (Input.GetKeyUp(KeyCode.Tab))
        {
            FindObjectOfType<GameController>().time = 1f;
        }
    }
}
