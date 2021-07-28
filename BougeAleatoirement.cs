using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BougeAleatoirement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {     
        if(Input.GetKeyDown("m")){
            
            float a = UnityEngine.Random.Range(-5,5);
            float b = UnityEngine.Random.Range(-5,5);
                                
             transform.Translate(a,b,0);
        }   
        
    }

    void aaa(){
        
   
    }
}
