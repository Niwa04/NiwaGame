using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimiteGestionnaire 
{
    public Dictionary<GameObject, int> inimites = new Dictionary<GameObject, int>();

    public InimiteGestionnaire(){
        GameObject[] persos = new GameObject[8];
        foreach (var item in persos)
        {
            inimites.Add(item,0);
        }
    }

    public GameObject findPersoWithTopInimite(){
        foreach(KeyValuePair<GameObject, int> entry in inimites)
        {
            return entry.Key;
        }
        return null;
    }

    public void UpateInimite(GameObject a, int puissance){
        PersonnageData pd = null;
        int total = 0;
        switch (pd.strategieCurrent.name)
        {
            case "Magicien":
                total = puissance / 3;
            break;
            case "Defenceur":
                total = puissance * 4;
            break;
            default:
                total = puissance;
            break;
        }
        inimites[a] += total;
    }

    public string toString(){
        string res = "";
         foreach(KeyValuePair<GameObject, int> entry in inimites)
        {
            res += entry.Key+":"+entry.Value+" | ";
        }
        return res;
    }

}

