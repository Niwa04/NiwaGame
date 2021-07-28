using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TPCWC;
using TMPro;


public class HitScript : MonoBehaviour
{

    public MyAction action;
    public string cristalName;
    public List<MyAction> actions;

    public void Hit(){  
        if(action != null && action.cible != null){
            afficheEffet();
            GetComponentInParent<ActionManager>().restTimerAction();
        }   
    }


    public void afficheEffet(){
        if(action.competence.typeComptence.name == "Buff"){
            appliBuff(action);
            return;
        }
            
        Transform loc = GetLocationApparition(action);
        action.lanceur.transform.LookAt(action.cible.transform);
        GameObject a = Instantiate(action.competence.model,loc);

        a.GetComponentInChildren<DamageAttack>().SetAction(action);
        
        a.transform.SetParent(null);
        a.transform.position = loc.transform.position;
        a.transform.localScale = new Vector3(1f,1f,1f);

        if(action.competence.attache)
            a.transform.SetParent(loc);

        try
        {
            if(action.competence.spawn != "EnemySol")
                a.transform.LookAt(action.cible.GetComponentInChildren<LocationScriptCenter>().gameObject.transform);
        } catch (System.Exception){
        }
        
        if(action.competence.typeComptence.name == "Buff")
            return;
        if(action.competence.timeForAppli != 0){
           Destroy(a, action.competence.timeForAppli);

        }else
            Destroy(a, action.competence.time*3);

        if(cristalName != null){
            action.lanceur.GetComponent<CristalScript>().deleteCrystal(cristalName);
            cristalName = null;
        }
    }


    public void afficheCercleMagic(){
        if(action.competence.cercle)
            Instantiate(action.competence.cercle,transform);
    }

    public void afficheCanalisation(){
        if(!action.competence.canalisation)
            return;
        GameObject a = Instantiate(action.competence.canalisation,transform);
        Destroy(a, action.competence.timeCanalisation);
    }

  
Transform GetLocationApparition(MyAction act){
        Transform res = null;
        EnumTypeSpawn spawn = act.competence.apparition;
        try{
            switch (spawn)
            {
                case EnumTypeSpawn.Centre :
                    res = act.lanceur.GetComponentInChildren<LocationScriptCenter>().gameObject.transform;
                break;
                case EnumTypeSpawn.Enemy :
                    res =  act.cible.transform;
                    res.eulerAngles = Vector3.zero;
                break;
                case EnumTypeSpawn.EnemyCentre :
                    res = act.cible.GetComponentInChildren<LocationScriptCenter>().gameObject.transform;
                break;
                case EnumTypeSpawn.EnemySol :
                    res = act.cible.transform;
                break;
                case EnumTypeSpawn.EnemySol2 :
                    res = act.cible.GetComponentInChildren<LocationScriptSol>().gameObject.transform;
                    res.eulerAngles = Vector3.zero;
                break;
                case EnumTypeSpawn.Arme :
                    res = act.lanceur.GetComponentInChildren<ArmeLocation>().gameObject.transform;
                break;
                case EnumTypeSpawn.Main :
                    res = act.lanceur.GetComponentInChildren<MainLocation>().gameObject.transform;
                break;
                case EnumTypeSpawn.Sol :
                    res = act.lanceur.GetComponentInChildren<LocationScriptSol>().gameObject.transform;
                    res.eulerAngles = Vector3.zero;
                break;
                case EnumTypeSpawn.Devant :
                    res = act.lanceur.GetComponentInChildren<DevantLocation>().gameObject.transform;
                    res.eulerAngles = Vector3.zero;
                break;
                case EnumTypeSpawn.LocationScript :
                    res = act.lanceur.GetComponentInChildren<LocationScript>().gameObject.transform;
                    res.eulerAngles = Vector3.zero;
                 break;
                default:
                    res = act.lanceur.GetComponentInChildren<LocationScript>().gameObject.transform;
                    break;
            }
        }catch{
               return  act.lanceur.transform;
        }
        return res;
    }

    public void lookEnnemie(){
        if(action.cible)
           action.lanceur.transform.LookAt(action.cible.transform);
    }

    public void stopTrigger(){
        GetComponentInParent<CapsuleCollider>().isTrigger = true;
    }

    
    private void appliBuff(MyAction action){
      BuffScript buffScript =  action.cible.AddComponent<BuffScript>();
      buffScript.value = action.competence.puissance;
      buffScript.type = action.competence.animVariable;
      buffScript.id = action.competence.name;
      buffScript.obj = gameObject;
      buffScript.action = action;
    }

    public void attackSpecial(){
        GetComponentInParent<ActionManager>().attackSpecial();
    }

}
