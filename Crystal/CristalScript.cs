using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalScript : MonoBehaviour
{
    public int nombreDeCristal;
    public List<Cristal> cristalsL;
   public Cristal[] cristals ; 
   public int chanceAddCrystal;
     private CrystalManager cm;

     public bool afficheEffetCristal;

    void Start(){
        cm = FindObjectOfType<CrystalManager>();
        cristals = new Cristal[nombreDeCristal]; 
        cristalsL = new List<Cristal>();
    }

    private void Update() {
      
    }
    
    public void deleteCrystal(string name){
        for(int i = 0; i < 8; i++){
            if(cristals[i] != null && cristals[i].type != null && cristals[i].type == name){
                cristals[i] =  new Cristal("");
                cm.afficherCrystals();
                return;
            }        
        }
    }


    public void tryToAddCrystal(){
      
        int rdm =  UnityEngine.Random.Range(0,100);
        if(chanceAddCrystal > rdm){
            addCrystal();
            cm.afficherCrystals();
        }

    }

    public void addCrystal(){
        if(!hasPlaceLibre()){
            return;
        }
        int rdm =  UnityEngine.Random.Range(0,100);
        int chance = 5;
        if(rdm > 100+chance){
            return;
        }
     
        Cristal newC = getRamdomCrystal();
        cristalsL.Add(newC);
        if(afficheEffetCristal)
            afficheEffetAddCristal(newC);
       if(has2SameCristal( newC)){
            fusionCristal(newC);
            possede4CritauxMax();
       }
        else
        {
            int i = getfirstPlaceLibreCristal();
            if(i == -1)
                return;
            cristals[i] = newC;
        }
       
    }


    public void possede4CritauxMax(){
        bool terre = false;
        bool feu = false;
        bool eau = false;
        bool vent = false;
        for(int k = 0 ; k < 8 ; k++){
            Cristal cs = cristals[k];
            try
            {
                if(cs.type == "Eau+"){
                    eau = true;
                } 
                 if(cs.type == "Feu+"){
                    feu = true;
                }  
                 if(cs.type == "Terre+"){
                    terre = true;
                }  
                 if(cs.type == "Vent+"){
                    vent = true;
                }   
             }
            catch (System.Exception)
            {
            }
        } 
        if(terre && feu && eau && vent){
            cm.afficherCrystalUltime();
        }
    }

  
    public void afficheEffetAddCristal(Cristal newC){
        switch (newC.type)
        {
            case "Eau":
                Instantiate(cm.effectAddCristalEau,transform);
            break;
            case "Feu":
                Instantiate(cm.effectAddCristalFeu,transform);
            break;
            case "Vent":
                Instantiate(cm.effectAddCristalVent,transform);
            break;
            case "Terre":
                Instantiate(cm.effectAddCristalTerre,transform);
            break;
            default:
            break;
        }
    }

    public Cristal getRamdomCrystal(){
        int rdm =  UnityEngine.Random.Range(0,100);
        if(rdm >= 0 && rdm <= 25)
            return new Cristal("Feu");
        if(rdm > 25 && rdm <= 50)
            return new Cristal("Eau");
        if(rdm > 50 && rdm <=75)
            return new Cristal("Terre");
        return new Cristal("Vent");
    }

    public bool has2SameCristal(Cristal c){
        int count = 0;
        foreach (var item in cristals)
        {   
             try
            {
                if(item.type == c.type){
                    count ++;
                    if(count == 2)
                        return true;
                }
             }
            catch (System.Exception)
            {
                
            }
        }
        return false;
    }

    public void fusionCristal(Cristal c){
        int firstIndex = -1;
        for(int k = 0 ; k < 8 ; k++){
            Cristal cs = cristals[k];
            try
            {
                if(cs.type == c.type && firstIndex == -1){
                    firstIndex = k;
                }
                if(cs.type == c.type && firstIndex != -1){
                    cristals[k] = new Cristal("");
                }
            }
            catch (System.Exception)
            {
                
            }
        }
        string newType = c.type;
       
        newType += "+";
        
        cristals[firstIndex] = new Cristal(newType);
        fusionMax(cristals[firstIndex]);
    }

    public void fusionMax(Cristal c){
        int firstIndex = -1;
        int secondIndex = -1;
        int troisiemeIndex = -1;
        for(int k = 0 ; k < 8 ; k++){
            Cristal cs = cristals[k];
            try
            {
                if(cs.type == c.type && firstIndex == -1){
                    firstIndex = k;
                }else{
                    if(cs.type == c.type && secondIndex == -1){
                        secondIndex = k;
                    }else{
                        if(cs.type == c.type && troisiemeIndex == -1){
                            troisiemeIndex = k;
                        }  
                    }
                }
                
            }
            catch (System.Exception)
            {
                
            }
        }
        string newType = c.type;
        string newType2 = newType.Replace("+","X");
        if(firstIndex != -1 && secondIndex != -1 && troisiemeIndex != -1){
            cristals[firstIndex] = new Cristal(newType2);
             cristals[secondIndex] = new Cristal("");
            cristals[troisiemeIndex] = new Cristal("");      
        }
    }

    public int getfirstPlaceLibreCristal(){
         for(int k = 0 ; k < 8 ; k++){
            Cristal cs = cristals[k];
              try
            {
                if(cs.type == ""){
                 return k; 
                }  
             }
            catch (System.Exception)
            {
                return k;
            }
        }
        return -1;
    }
    
    public bool hasPlaceLibre(){
           for(int k = 0 ; k < 8 ; k++){
            Cristal cs = cristals[k];
            try
            {
                if(cs.type == ""){
                 return true; 
                }  
             }
            catch (System.Exception)
            {
                return true;
            }
        }
        return false;
    }

    public string hasCPlus(){
        for(int k = 0 ; k < 8 ; k++){
            Cristal cs = cristals[k];
            try
            {
           //     Debug.Log(cs.type + cs.type.Contains("+") );
                if(cs.type.Contains("+")){
                    Debug.Log(gameObject.name+" fusionne possede le cristal "+cs.type);
                     return cs.type; 
                }  
             }
            catch (System.Exception)
            {
               
            }
        }
        return null;
    }

    public Cristal hasCMax(){
        for(int k = 0 ; k < 8 ; k++){
            Cristal cs = cristals[k];
            if(cs.type.Contains("X")){
                return cs; 
            }  
        }
        return null;
    }
    
    public void displayCri(){
        Debug.Log("Diplay cri de "+gameObject.name);
        foreach (Cristal item in cristalsL)
        {
            Debug.Log(item.type);
        }
    }
}
