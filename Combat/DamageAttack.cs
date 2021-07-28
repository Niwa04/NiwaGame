using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;

public class DamageAttack : MonoBehaviour
{
    public MyAction action;
    private float countDown = 0.5f;
    
    private void Start(){
           if(action == null)
            return;
            if(action.competence.pointCible == "Raycast")
                goRaycast();
            if(action.competence.pointCible == "Distance")
                goDistance();  

    }
    private void goRaycast(){
        Transform arme = action.lanceur.GetComponentInChildren<LocationScript>().transform;
        Debug.DrawRay(transform.position, transform.forward*10, Color.red);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 1000);
        foreach (var item in hits)
        {
            if(item.transform == action.cible.transform){
                action.cible.gameObject.GetComponent<PersonnageDataManager>().dammage(action);
                return;
            }
            
        }

    }

     private void goDistance(){

        float distanceToCible = Vector3.Distance(action.lanceur.transform.position, action.cible.transform.position);
        if(distanceToCible <= action.competence.range){
              if(action.competence.typeComptence.name == "Heal" )
                action.cible.gameObject.GetComponent<PersonnageDataManager>().heal(action);
            else
                action.cible.gameObject.GetComponent<PersonnageDataManager>().dammage(action);
        }

        
    }

      private void goColision(GameObject item){
        if(item.transform == action.cible.transform){
            if(action.competence.typeComptence.name == "Heal" )
                action.cible.gameObject.GetComponent<PersonnageDataManager>().heal(action);
            else
                action.cible.gameObject.GetComponent<PersonnageDataManager>().dammage(action);
        }
        
    }



    private void Update(){
        
    }
    public void SetAction(MyAction a){
        action = a;

    }

      private void OnTriggerEnter(Collider other) {
        if(action.competence.pointCible == "Collision" && other.gameObject == action.cible) {
               if(action.competence.typeComptence.name == "Heal" )
                 action.cible.gameObject.GetComponent<PersonnageDataManager>().heal(action);
                else
                action.cible.gameObject.GetComponent<PersonnageDataManager>().dammage(action);
        }
    }
        
    private void OnTriggerStay(Collider other) {

            countDown -= Time.deltaTime;
            if(countDown <= 0){
                if(action.competence.pointCible == "CollisionStay" && other.gameObject == action.cible) {
                    action.cible.gameObject.GetComponent<PersonnageDataManager>().dammage(action);
                    countDown = 10f;
                } 
            }
        
    }
}
