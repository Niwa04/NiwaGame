using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;

public static class FindCible
{
     
    public static GameObject Find(Transform t, EnumTypeCible cible){
        GameObject res = null;
        if(cible == EnumTypeCible.EnemyProche)
            res = findEnemyLePlusProche(t);
        if(cible == EnumTypeCible.EnemySansObstacle)
            res = findEnemyLePlusProche2SansObstacle(t);
        if(cible == EnumTypeCible.AllieProche)
            res = findAllieLePlusProche(t);
        return res;
    }
     //////////////////////////////////  Find Allie  /////////////////////////////////
    public static GameObject findAllieLePlusProche(Transform t){
        float minDis = 1000000;
        GameObject res = null;
        foreach (var item in GameObject.FindGameObjectsWithTag(t.gameObject.tag))
        {
            float dis = Vector3.Distance(item.transform.position, t.position);
            if(minDis > dis && item.GetComponent<PersonnageDataManager>().hpCurrent > 0 && item.transform.name != t.name && t.gameObject.GetComponent<PersonnageDataManager>().perso.range > dis){
                res = item;
                minDis = dis;
            }
        }

        return res;
    }
    
    
    public static GameObject findAllieAvecLeMoinDePv(Transform t){
          int minHp = 1000000;
          GameObject res = null;
          string autre = "autre : ";
          foreach (var item in GameObject.FindGameObjectsWithTag(t.gameObject.tag))
          {
                           float dis = Vector3.Distance(item.transform.position, t.position );

            int hp = item.gameObject.GetComponent<PersonnageDataManager>().hpCurrent;
             if(minHp > hp && hp > 0 && t.gameObject.GetComponent<PersonnageDataManager>().perso.range > dis){
                 res = item;
                 minHp = hp;
             }else{
                 autre += item.transform.name+" : "+hp+" | ";
             }
          }
          
          return res;
    }



    public static GameObject findAllieAvecMoinDeXPercent(Transform t, float percent){
          float minHp = 100f;
          GameObject res = null;
          string autre = "autre : ";
          foreach (var item in GameObject.FindGameObjectsWithTag(t.gameObject.tag))
          {
                           float dis = Vector3.Distance(item.transform.position, t.position );

            int hpCurent = item.gameObject.GetComponent<PersonnageDataManager>().hpCurrent;
            int hpMax = item.gameObject.GetComponent<PersonnageDataManager>().perso.hpMax;
            float hp = (float) hpCurent / (float) hpMax * 100; 
             if(minHp > hp && hp > 0 && hp < percent && t.gameObject.GetComponent<PersonnageDataManager>().perso.range > dis){
                 res = item;
                 minHp = hp;
                
             }else{
                // autre += item.transform.name+" avec "+hp+"% | ";
             }
          }
          if(res){
             // Debug.Log("Le plus faible de percent est : "+res.transform.name+" avec "+minHp+"%" );
           //   Debug.Log(autre);
          }
          return res;
    }
    


    public static GameObject findAllieSansBouclier(Transform t){
          float minHp = 100f;
          GameObject res = null;
          foreach (var item in GameObject.FindGameObjectsWithTag(t.gameObject.tag))
          {
                           float dis = Vector3.Distance(item.transform.position, t.position );

             if(item.GetComponent<PersonnageDataManager>().shieldValue == 0 && item.GetComponent<PersonnageDataManager>().hpCurrent > 0 && t.gameObject.GetComponent<PersonnageDataManager>().perso.range > dis){
                 return item;
                
             }
          }
        
          return res;
    }

       public static GameObject findAllieSansEnchantement(Transform t, string name){
          float minHp = 100f;
          GameObject res = null;
          foreach (var item in GameObject.FindGameObjectsWithTag(t.gameObject.tag))
          {
                bool hasEnchant = false;
               BuffScript[] bs = item.gameObject.GetComponents<BuffScript>();
                             float dis = Vector3.Distance(item.transform.position, t.position );

                foreach (BuffScript b in bs)
                {
                    if(b.type == "Enchant"){
                        hasEnchant = true;
                    }
                }
                if(!hasEnchant  && item.GetComponent<PersonnageDataManager>().hpCurrent > 0 && t.gameObject.GetComponent<PersonnageDataManager>().perso.range > dis){
                    return item;
                }
          }
        
          return res;
    }


    ///////////////////////////////

    public static GameObject findTagEnemy(){
        return GameObject.FindGameObjectsWithTag("Enemy")[0];
    }

    public static GameObject findLePlusProche(Transform t,string tag){
          float minDis = 1000000;
          GameObject res = null;
          foreach (var item in GameObject.FindGameObjectsWithTag(tag))
          {
             float dis = Vector3.Distance(item.transform.position, t.position);
             if(minDis > dis && item.GetComponent<PersonnageDataManager>().hpCurrent > 0 && t.gameObject.GetComponent<PersonnageDataManager>().perso.range > dis){
                 res = item;
                 minDis = dis;
             }
          }
          return res;
    }

    
    public static GameObject findLePlusProcheSansRange(Transform t,string tag){
          float minDis = 1000000;
          GameObject res = null;
          foreach (var item in GameObject.FindGameObjectsWithTag(tag))
          {
             float dis = Vector3.Distance(item.transform.position, t.position);
             if(minDis > dis && item.GetComponent<PersonnageDataManager>().hpCurrent > 0){
                 res = item;
                 minDis = dis;
             }
          }
          return res;
    }

    

       public static GameObject findLePlusFaible(Transform t,string tag){
          int hpMin = 1000000;
          GameObject res = null;
          foreach (var item in GameObject.FindGameObjectsWithTag(tag))
          {
                           float dis = Vector3.Distance(item.transform.position, t.position );

             int hp = item.GetComponent<PersonnageDataManager>().hpCurrent;
             if(hpMin > hp && item.GetComponent<PersonnageDataManager>().hpCurrent > 0 && t.gameObject.GetComponent<PersonnageDataManager>().perso.range > dis){
                 res = item;
                 hpMin = hp;
             }
          }
          return res;
    }

    public static GameObject findEnemyLePlusProche(Transform transform){
        if(transform.gameObject.tag == "Enemy")
            return findLePlusProche(transform,"Player");

        return findLePlusProche(transform,"Enemy");
    }

    public static GameObject findEnemyLePlusProcheSansRange(Transform transform){
        if(transform.gameObject.tag == "Enemy")
            return findLePlusProcheSansRange(transform,"Player");

        return findLePlusProcheSansRange(transform,"Enemy");
    }

    public static GameObject findHeroLePlusProche(Transform t){
          float minDis = 1000000;
          GameObject res = null;
          foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
          {
             float dis = Vector3.Distance(item.transform.position, t.position);
             if(minDis > dis && item.GetComponent<PersonnageDataManager>().hpCurrent > 0 && t.gameObject.GetComponent<PersonnageDataManager>().perso.range > dis){
                 res = item;
                 minDis = dis;
             }
          }
          return res;
    }


    public static GameObject findHeroLePlusProcheSansObstacle(Transform t){
          float minDis = 1000000;
          GameObject res = null;
          foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
          {
             bool hasObs = hasObstacle(t,item.transform);

             float dis = Vector3.Distance(item.transform.position, t.position);
             if(minDis > dis && item.GetComponent<PersonnageDataManager>().hpCurrent > 0  && item.transform.name != t.name && !hasObs && t.gameObject.GetComponent<PersonnageDataManager>().perso.range > dis){
                 res = item;
                 minDis = dis;
             }
          }
          return res;
    }


    public static GameObject findHeroLePlusLoinSansObstacle(Transform t){
          float maxDis = 0;
          GameObject res = null;
          foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
          {
             bool hasObs = hasObstacle(t,item.transform);

             float dis = Vector3.Distance(item.transform.position, t.position);
             if(maxDis < dis && item.GetComponent<PersonnageDataManager>().hpCurrent > 0  && item.transform.name != t.name && !hasObs && t.gameObject.GetComponent<PersonnageDataManager>().perso.range > dis){
                 res = item;
                 maxDis = dis;
             }
          }
          return res;
    }

    public static GameObject findEnemyLePlusProche2SansObstacle(Transform t){
          float minDis = 1000000;
          GameObject res = null;
          
          foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
          {
             bool hasObs = hasObstacle(t,item.transform);
             float dis = Vector3.Distance(item.transform.position, t.position );
             if(minDis > dis && item.GetComponent<PersonnageDataManager>().hpCurrent > 0 && item.transform.name != t.name && !hasObs && t.gameObject.GetComponent<PersonnageDataManager>().perso.range > dis) {
                 res = item;
                 minDis = dis;
             }
          }
          return res;
    }

    private static bool hasObstacle(Transform t1, Transform cible){
        try{
            Transform p = t1.gameObject.GetComponentInChildren<LocationScriptCenter>().transform;
            Transform q = cible.gameObject.GetComponentInChildren<LocationScriptCenter>().transform;

            Vector3 aaa = q.position-p.position ;
            Debug.DrawRay(p.position, aaa*100, Color.red);
            
            float minDis = 1000000;
            GameObject res = null;
            
            RaycastHit[] hits = Physics.RaycastAll(p.position, aaa, 1000);

            foreach (var item in hits)
            {

                float dis = Vector3.Distance(item.transform.position, t1.position );
                if(minDis > dis && item.transform.gameObject.GetComponent<ParticleSystem>() == null) {
                    res = item.transform.gameObject;
                    minDis = dis;
                }
                //Debug.Log("Entre "+ t1.name + " et "+cible.name +"--- "+item.transform);

           
                
            }
            if(cible.transform != res.transform){
                return true;
            }
            return false;
        }catch{
            return true;
        }
       
    }
}
