using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;

public class CrystalManager : MonoBehaviour
{

    public GameObject cristalFeu;
    public GameObject cristalEau;
    public GameObject cristalVent;
    public GameObject cristalTerre;
   
    public GameObject cristalFeu2;
    public GameObject cristalEau2;
    public GameObject cristalVent2;
    public GameObject cristalTerre2;

    public GameObject cristalFeu3;
    public GameObject cristalEau3;
    public GameObject cristalVent3;
    public GameObject cristalTerre3;

    public GameObject cristalvide;

    public CristalScript cscurrent;

    public GameObject effectAddCristalEau;
    public GameObject effectAddCristalFeu;
    public GameObject effectAddCristalTerre;
    public GameObject effectAddCristalVent;

    private GameController gc;
	private string chemainUi = "---- UI ----/UIStat/bouttonSelectorCrystal/";
    private void Start() {
        gc = FindObjectOfType<GameController>();

    }
    private void Update() {
      
    }
	public void afficherCrystals(){
         cscurrent = gc.activeP.gameObject.GetComponent<CristalScript>();
        Cristal[]   cristals = cscurrent.cristals;

        for(int k = 0 ; k < 8 ; k++){
            Cristal c = cristals[k];
            try
            {
                GameObject t = GameObject.Find(chemainUi+(k+1));
                destroyChild(t);
                GameObject cry = Instantiate(getObjectCristal(c),t.transform);
                cry.name = c.type;
                cry.transform.SetParent(t.transform); 
                cry.transform.position = t.transform.position;
                if(c.type == ""){
                    t.active  = false;
                }else{
                    
                    t.active  = true;

                }
            }
            catch (System.Exception)
            {
                GameObject t = GameObject.Find(chemainUi+(k+1));
                    t.active  = false;
            }  
        }
    }

    public void afficherCrystalUltime(){
        GameObject t = GameObject.Find(chemainUi+"ULTIME");
        t.active  = true;
    }

    public void destroyChild(GameObject o){
            foreach (Transform child in  o.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }

	public GameObject getObjectCristal(Cristal c){
        switch (c.type)
        {
            case "Vent":
                return cristalVent;
                break;
            case "Eau":
                return cristalEau;
                break;
            case "Terre":
                return cristalTerre;
                break;
            case "Feu":
                return cristalFeu;
                break;
            case "Vent+":
                return cristalVent2;
                break;
            case "Eau+":
                return cristalEau2;
                break;
            case "Terre+":
                return cristalTerre2;
                break;
            case "Feu+":
                return cristalFeu2;
                break;
            case "VentX":
                return cristalVent3;
                break;
            case "EauX":
                return cristalEau3;
                break;
            case "TerreX":
                return cristalTerre3;
                break;
            case "FeuX":
                return cristalFeu3;
                break;
            default:
                return cristalvide;
        }
    }

}
