using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private List<PersonnageData> personnageDatas;

    public ArmePosseder armes;

    private ArmeData[] armePerso;
    private string menuEquipementPath = "Menu/Menu Equipement/";
    private string menuCompetencePath = "Menu/Menu Competence/";

    private GameObject menuEquipement;
    private GameObject menuCompetence; 
    public int currentPerso; 
    private GameObject cristalCurrent;

    public MyData myData;

    public GameObject aFerme;

    // Start is called before the first frame update
    void Start()
    {
        personnageDatas = myData.personnages;
        currentPerso = 0;
        menuEquipement = GameObject.Find(menuEquipementPath);
        menuCompetence = GameObject.Find(menuCompetencePath);
        UpdateMenu();

       // menuCompetence.SetActive(false);
       // menuEquipement.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPerso(){
        currentPerso++;
        if(currentPerso == personnageDatas.Count)
            currentPerso = 0;
        
        try
        {
            UpdateMenuEquipement();

        }
        catch (System.Exception)
        {
            UpdateMenuCompetence();
        }

    }

    public void PrevPerso(){
        currentPerso--;
        if(currentPerso == -1)
            currentPerso = personnageDatas.Count - 1;
     
        try
        {
            UpdateMenuEquipement();

        }
        catch (System.Exception)
        {
            UpdateMenuCompetence();
        }

    }

    public void AfficherMenu(GameObject obj){
        MasquerLesMenus();
        obj.SetActive(true);
        if(obj.name == "Menu Equipement"){
            UpdateMenuEquipement();
        }
        if(obj.name == "Menu Competence"){
            UpdateMenuCompetence();
        }

    }

    public void AfficherMenuGalerie(GameObject obj){
        obj.SetActive(true);
        this.gameObject.SetActive(false);

    }

    public void AfficherMenuObjet(GameObject obj){
        MasquerLesMenus();
        obj.SetActive(true);
        UpdateMenuObjet();
    }
    void UpdateMenuObjet(){
        List<ObjetData> c = myData.objet;
        GameObject content = GameObject.Find("Menu/Menu Objet/Scroll View/Viewport/Content");
        for(int i = 0; i < content.transform.childCount; i++){
                content.transform.GetChild(i).gameObject.SetActive(false);
        }
       for(int i = 0; i < c.Count; i++){
            content.transform.GetChild(i).gameObject.SetActive(true);
            content.transform.GetChild(i).GetChild(1).GetComponent<Text>().text =  c[i].name;
            try
            {
             content.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite =  c[i].logo;
               
            }
            catch (System.Exception)
            {
            
            }
       }
    }

    public void UseObjet(int i){
        ObjetData ob = myData.objet[i];
        Instantiate(ob.effet);
        Debug.Log("Utilisation de "+ob.name);
        myData.objet.Remove(ob);
        UpdateMenuObjet();
    }


    private void MasquerLesMenus(){
        foreach (var item in GameObject.FindGameObjectsWithTag("Menu"))
          {
              item.SetActive(false);
          }
    }

    void UpdateMenuByPerso(){
        PersonnageData p = personnageDatas[currentPerso];

    }

    void UpdateMenu(){
        

    }

    void UpdateMenuEquipement(){
        UpdatePersoCanvas(menuEquipementPath);
        UpdateCaracteristique("Frc");
        UpdateCaracteristique("Def");
        UpdateCaracteristique("Prc");
        UpdateCaracteristique("Cha");
        UpdateCaracteristique("Esp");
        UpdateCaracteristique("Rap");
        UpdateCaracteristique("Mag");    
        UpdateAllArme();
    }
    void UpdateMenuCompetence(){
        UpdatePersoCanvas(menuCompetencePath);
        UpdateCristalCompetence("");
        UpdateAllCompetence();
    }

    void UpdatePersoCanvas(string menu){
        Debug.Log(menu);
        PersonnageData p = personnageDatas[currentPerso];
        Text name = GameObject.Find(menu+"Name").GetComponent<Text>();
        Image port = GameObject.Find(menu+"Perso/Portrait/Image").GetComponent<Image>();
                Image arme = GameObject.Find(menu+"Perso/Arme/Image").GetComponent<Image>();

        Image class1 = GameObject.Find(menu+"Perso/Class 1/Image").GetComponent<Image>();
        Image class2 = GameObject.Find(menu+"Perso/Class 2/Image").GetComponent<Image>();
        Image class3 = GameObject.Find(menu+"Perso/Class 3/Image").GetComponent<Image>();


        name.text = p.name;
        port.sprite = p.sprite;
        arme.sprite = p.arme.logo;
        class1.sprite = p.strategies[0].logo;
        class2.sprite = p.strategies[1].logo;
        class3.sprite = p.strategies[2].logo;
    }

    void UpdateCaracteristique(string cara){
        Text val = GameObject.Find(menuEquipementPath+"Caracteristique/"+cara+"/"+cara+"Val").GetComponent<Text>();
        Text plusOuMoins = GameObject.Find(menuEquipementPath+"Caracteristique/"+cara+"/Plus").GetComponent<Text>();
        Text bonus = GameObject.Find(menuEquipementPath+"Caracteristique/"+cara+"/"+cara+"Val (2)").GetComponent<Text>();
        Text total = GameObject.Find(menuEquipementPath+"Caracteristique/"+cara+"/"+cara+"Val (4)").GetComponent<Text>();

        val.text = GetValCaracteristique(cara)+"";
        int t =  GetValCaracteristique(cara) + GetValCaracteristiqueArme(cara);
        
        if( GetValCaracteristiqueArme(cara) < 0){
            plusOuMoins.text = "-";
            bonus.text = -1*GetValCaracteristiqueArme(cara)+"";

        }
        else{
            plusOuMoins.text = "+";
            bonus.text = GetValCaracteristiqueArme(cara)+"";

        }

        if(t < 0)
            total.text = "0";
        else
        total.text = t + "";

    }

    int GetValCaracteristique(string cara){
        PersonnageData p = personnageDatas[currentPerso];

        switch (cara)
        {
            case "Frc":
                return p.caracteristique.frc;
            case "Def":
                return p.caracteristique.def;
            case "Prc":
                return p.caracteristique.prc;
            case "Cha":
                return p.caracteristique.cha;
            case "Mag":
                return p.caracteristique.mag;
            case "Rap":
                return p.caracteristique.rap;
            default :
                return p.caracteristique.esp;
        }
    }

    int GetValCaracteristiqueArme(string cara){
        ArmeData p = personnageDatas[currentPerso].arme;

        switch (cara)
        {
            case "Frc":
                return p.caracteristique.frc;
            case "Def":
                return p.caracteristique.def;
            case "Prc":
                return p.caracteristique.prc;
            case "Cha":
                return p.caracteristique.cha;
            case "Mag":
                return p.caracteristique.mag;
            case "Rap":
                return p.caracteristique.rap;
            default :
                return p.caracteristique.esp;
        }
    }


    void UpdateCristalCompetence(string name){
        
        Text eau = GameObject.Find("Menu/Menu Competence/Cristals/eau/Button/Text").GetComponent<Text>();
        Text vent = GameObject.Find("Menu/Menu Competence/Cristals/vent/Button/Text").GetComponent<Text>();
        Text feu = GameObject.Find("Menu/Menu Competence/Cristals/feu/Button/Text").GetComponent<Text>();
        Text terre = GameObject.Find("Menu/Menu Competence/Cristals/terre/Button/Text").GetComponent<Text>();
        Text eauX = GameObject.Find("Menu/Menu Competence/Cristals/eauX/Button/Text").GetComponent<Text>();
        Text ventX = GameObject.Find("Menu/Menu Competence/Cristals/ventX/Button/Text").GetComponent<Text>();
        Text feuX = GameObject.Find("Menu/Menu Competence/Cristals/feuX/Button/Text").GetComponent<Text>();
        Text terreX = GameObject.Find("Menu/Menu Competence/Cristals/terreX/Button/Text").GetComponent<Text>();

        PersonnageData p = personnageDatas[currentPerso];

        if(p.comps.vent != null)
            vent.text =  p.comps.vent.name;
        else
            vent.text = "vide";

        if(p.comps.eau != null)
            eau.text =  p.comps.eau.name;
        else
            eau.text = "vide";

        if(p.comps.eauX != null)
            eauX.text =  p.comps.eauX.name;
        else
            eauX.text = "vide";

        if(p.comps.feu != null)
            feu.text =  p.comps.feu.name;
        else
            feu.text = "vide";

        if(p.comps.feuX != null)
            feuX.text =  p.comps.feuX.name;
        else
            feuX.text = "vide";

        if(p.comps.ventX != null)
            ventX.text =  p.comps.ventX.name;
        else
            ventX.text = "vide";

        if(p.comps.terre != null)
            terre.text =  p.comps.terre.name;
        else
            terre.text = "vide";

        if(p.comps.terreX != null)
            terreX.text =  p.comps.terreX.name;
        else
            terreX.text = "vide";
    }

    void UpdateAllCompetence(){
          // buttonStrategie[i].GetComponent<Image>().color  =  new Color(70f/255f, 70/255f, 70/255f,137f/255f);
        PersonnageData p = personnageDatas[currentPerso];
        List<Strategie> ss = new List<Strategie>(p.strategies);
        GameObject content = GameObject.Find("Menu Competence/Scroll View/Viewport/Content");
        Debug.Log("child : "+ content.transform.childCount );
                for(int i = 0; i < content.transform.childCount; i++){
                content.transform.GetChild(i).gameObject.SetActive(false);
        }
       for(int i = 0; i < myData.competences.Count; i++){
           if(ss.Contains(myData.competences[i].strategie)){
                content.transform.GetChild(i).gameObject.SetActive(true);
                content.transform.GetChild(i).GetChild(1).GetComponent<Text>().text =  myData.competences[i].name;
                try
                {
                content.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite =  myData.competences[i].strategie.logo;
                }
                catch (System.Exception)
                {
                
                }
           }
           

       }
    }

    public void OuvrirMenu(){
        aFerme = GameObject.Find("MenuChoix");
        gameObject.SetActive(true);
        aFerme.SetActive(false);
    }

    public void FermerMenu(){
        gameObject.SetActive(false);
        aFerme.SetActive(true);
    }
    void UpdateAllArme(){
          // buttonStrategie[i].GetComponent<Image>().color  =  new Color(70f/255f, 70/255f, 70/255f,137f/255f);
        ArmeData[] c = findMyArme();
        GameObject content = GameObject.Find("Menu/Menu Equipement/Scroll View/Viewport/Content");
        for(int i = 0; i < content.transform.childCount; i++){
                content.transform.GetChild(i).gameObject.SetActive(false);
        }
       for(int i = 0; i < c.Length; i++){
            content.transform.GetChild(i).gameObject.SetActive(true);
            content.transform.GetChild(i).GetChild(0).GetComponent<Text>().text =  c[i].name;
            try
            {
             content.transform.GetChild(i).GetChild(1).GetComponent<Image>().sprite =  c[i].logo;
               
            }
            catch (System.Exception)
            {
            
            }

       }
    }

    ArmeData[] findMyArme(){
        PersonnageData p = personnageDatas[currentPerso];
        List<ArmeData> res = new List<ArmeData>();
        foreach(ArmeData ar in p.persoArmeData.armes){
            if(  System.Array.IndexOf(armes.armes, ar) > -1)
                res.Add(ar);
        }
        armePerso = res.ToArray();
        return res.ToArray();
    }

    public void clickCristal(int i){
        GameObject content = GameObject.Find("Menu/Menu Competence/Cristals/");
        for(int k = 0; k < content.transform.childCount; k++){
            content.transform.GetChild(k).GetChild(0).GetComponent<Image>().color  =  new Color(250f/255f, 250f/255f, 250f/255f,0f);
       }
        content.transform.GetChild(i).GetChild(0).GetComponent<Image>().color  =  new Color(70f/255f, 70/255f, 70/255f,137f/255f);
        cristalCurrent = content.transform.GetChild(i).gameObject;
        
    }

    public void choiseCompetence(int i){
        SetComp(cristalCurrent.name, myData.competences[i]);
        UpdateMenuCompetence();
    }

    public void choixArme(int i){
        PersonnageData p = personnageDatas[currentPerso];
        p.arme = armePerso[i];
                UpdateMenuEquipement();

    }
    private void SetComp(string cristal, CompetenceData c){
                PersonnageData p = personnageDatas[currentPerso];

        switch (cristal)
        {
            case "eau":
                p.comps.eau =c;
            break;
            case "feu":
                p.comps.feu = c;
            break;
            case "terre":
                p.comps.terre = c;
            break;
            case "vent":
                p.comps.eau = c;
            break;

            case "eauX":
                p.comps.eauX = c;
            break;
            case "feuX":
                p.comps.feuX = c;
            break;
            case "terreX":
                p.comps.terreX = c;
            break;
            case "ventX":
                p.comps.ventX = c;
            break;
                        
            default:
            break;
        }
    }
}
