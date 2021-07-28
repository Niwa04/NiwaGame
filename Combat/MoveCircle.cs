using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;

public class MoveCircle : MonoBehaviour
{
      CameraManager c;
      bool selected;

    public float dist;
     public CompanionInput companion;

     private Vector3 positionDeBase;

     private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
         c = FindObjectOfType<CameraManager>();
         selected = false;
         positionDeBase = transform.position;

    }
    // Update is called once per frame
    void Update()
    {      
        if(Input.GetMouseButtonDown(1)){

            if(!CanMove())
                return ;
            RaycastHit hit;
            Ray ray = c.mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                transform.position = hit.point;
                GetComponent<Animator>().Play(0);
            }
            companion.goPoint(transform.gameObject);
            positionDeBase = transform.position;
            GetComponent<AudioSource>().Play(); 
        }

        if(Input.GetKeyDown("m")){
            
            float a = UnityEngine.Random.Range(-dist,dist);
            float b = UnityEngine.Random.Range(-dist,dist);
            
            count++;
          
            transform.position = positionDeBase;
          
             transform.Translate(a,b,0);
        }         
    }

    bool CanMove(){
        if(companion == null)
            return false;
        if(companion.GetComponent<ActionManager>().wait) 
            return false;
        if(companion.GetComponent<PersonnageDataManager>().hpCurrent == 0)
            return false;
        if(companion.GetComponent<ActionManager>().IsCanalise()) 
            return false;
        

        return true;
           
    }
}
