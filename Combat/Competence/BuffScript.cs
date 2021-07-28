using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : MonoBehaviour
{

    public string type;
    public string attribut;
    public int value;
    public string id;
    public GameObject obj;
    public MyAction action;

    private bool withLimit = false;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        if(action.competence.animVariable == "Shield"){
            GetComponent<PersonnageDataManager>().shieldValue = value;
            GameObject a = Instantiate(action.competence.model,transform);
            a.transform.position = action.cible.transform.position;
            //a.transform.position = action.cible.GetComponentInChildren<LocationScriptCenter>().gameObject.transform.position;
            a.transform.SetParent(transform);
            obj = a;
        }
          if(action.competence.animVariable == "Enchant"){
            GetComponent<PersonnageDataManager>().perso.getCurrentCaracteristique().frc += value;            
            GameObject a = Instantiate(action.competence.model);
            a.GetComponentInChildren<PSMeshRendererUpdater>().MeshObject = action.cible.GetComponentInChildren<ArmeLocation>().transform.GetChild(0).gameObject;
            a.GetComponentInChildren<PSMeshRendererUpdater>().UpdateMeshEffect();
            delay = action.competence.timeForAppli;
            withLimit = true;
            obj = a;
        }
        if(action.competence.animVariable == "Defend"){
            GetComponent<PersonnageDataManager>().perso.getCurrentCaracteristique().def += GetComponent<PersonnageDataManager>().perso.caracteristique.def;
            GetComponent<PersonnageDataManager>().perso.getCurrentCaracteristique().frc -=  (GetComponent<PersonnageDataManager>().perso.caracteristique.frc - 2);
            delay = 10f;
            withLimit = true;
            obj = null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(action.competence.animVariable == "Shield" && GetComponent<PersonnageDataManager>().shieldValue <= 0){
            if(obj != null)
                Destroy(obj);
            Destroy(this);
             
        }

        if(withLimit){
            delay -= Time.deltaTime;
            if(delay < 0){
                stopBonus();
                Destroy(this);
                if(obj != null)
                Destroy(obj);
            }
        }
    }

    void stopBonus(){
         if(action.competence.animVariable == "Enchant"){
            GetComponent<PersonnageDataManager>().perso.getCurrentCaracteristique().frc -= value;
          
        }
        if(action.competence.animVariable == "Defend"){
            GetComponent<PersonnageDataManager>().perso.getCurrentCaracteristique().frc +=  (GetComponent<PersonnageDataManager>().perso.caracteristique.frc - 2);
                       GetComponent<PersonnageDataManager>().perso.getCurrentCaracteristique().def -= GetComponent<PersonnageDataManager>().perso.caracteristique.def;

        }
    }
    
}
