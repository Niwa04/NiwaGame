using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoixFinDeCombatManager : MonoBehaviour
{
    public CombatData combatData;

    public AllData allData;

    public Transform placement;

    public Transform persoPosseder;

    public Transform choixPerso;
    public Transform choixComp;

    public int nombreDePersonnageAChoisir;

    public PersonnageData persoSelect;
    public Strategie stratSelect;

    private List<PersonnageData> resPerso;
    private List<Strategie> resStrat;

    public MyData myData;

    public MenuManager menuManager;
    public GameObject lancement;
    // Start is called before the first frame update
    void Start()
    {
        AffichePersoPosseder();
      AfficherPersonnageAChoisir(); 
      GetAllStrategie();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AffichePersoPosseder(){

          for (int i = 0; i < persoPosseder.childCount; i++)
        {
            persoPosseder.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < combatData.personnages.Count; i++)
        {
            persoPosseder.GetChild(i).GetChild(3).GetComponent<Image>().sprite = combatData.personnages[i].sprite;
            persoPosseder.GetChild(i).gameObject.SetActive(true);
        }
    }

    void AfficherPersonnageAChoisir(){
     
        resPerso = new List<PersonnageData>();
        foreach(PersonnageData p in allData.personnages){
            if(!combatData.personnages.Contains(p)){
                resPerso.Add(p);
            }
        }
        if(combatData.personnages.Count != 0){
                    melangerTab(resPerso);

        }
 
        for (int i = 0; i < placement.childCount; i++)
        {
            placement.GetChild(i);
           GameObject o = Instantiate(resPerso[i].model3D);
          o.transform.position =  placement.GetChild(i).position;
            choixPerso.GetChild(i).GetComponentInChildren<Text>().text = resPerso[i].name;
        }
    }
    void melangerTab<T>(List<T> l){
        for(int i = 0 ; i < l.Count; i++){
            T tmp = l[i];
            int rdm =  UnityEngine.Random.Range(0,l.Count);
            T tmp2 = l[rdm];

            l[i] = tmp2;
            l[rdm] = tmp;

        }
    }

    public void SelectPerso(int i){
        persoSelect = resPerso[i];
        for (int j = 0; j < 3; j++)
        {
            choixPerso.GetChild(j).GetComponent<Image>().color = new Color(70f/255f, 70/255f, 70/255f,1f/255f);
        }
        choixPerso.GetChild(i).GetComponent<Image>().color = new Color(70f/255f, 70/255f, 70/255f,137f/255f);

    }

      public void SelectComp(int i){
        stratSelect = resStrat[i];
        for (int j = 0; j < 3; j++)
        {
            choixComp.GetChild(j).GetComponent<Image>().color = new Color(70f/255f, 70/255f, 70/255f,1f/255f);
        }
        choixComp.GetChild(i).GetComponent<Image>().color = new Color(70f/255f, 70/255f, 70/255f,137f/255f);

    }

    void GetAllStrategie(){
        resStrat = new List<Strategie>();
         foreach(PersonnageData p in combatData.personnages){
             foreach (Strategie s in p.strategies)
             {
                 if(!resStrat.Contains(s)){
                     resStrat.Add(s);
                 }
             }
        }
        melangerTab(resStrat);
        for (int i = 0; i < choixComp.childCount; i++)
        {
            choixComp.GetChild(i).gameObject.SetActive(false);
        }
        if(combatData.personnages.Count == 0)
            return;
         for (int i = 0; i < 3; i++)
        {
            choixComp.GetChild(i).gameObject.SetActive(true);
            choixComp.GetChild(i).GetChild(3).GetComponent<Image>().sprite = resStrat[i].logo;
        }
    }

    public void ValiderEtOuvrirMenu(){
        if(persoSelect != null)
            combatData.personnages.Add(persoSelect);
        if(stratSelect != null){
            ObjetData b = ChercheManuel(stratSelect.name);
            if(b != null){
                myData.objet.Add(b);
                Debug.Log("Rajout de l'objet "+b.name);
            }else{
                Debug.Log("On a pas pu rajouter l'objet");
            }
        }
        menuManager.OuvrirMenu();
        lancement.SetActive(true);
        combatData.stageCurrent++;
    }

    ObjetData ChercheManuel(string s){
        foreach (var obj in allData.book)
        {
            if(obj.name.Contains(s))
                return obj;
        }
        return null;
    }

}
