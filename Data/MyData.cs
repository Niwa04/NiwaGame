using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyData",  order = 0)]
public class MyData : ScriptableObject {

    public   List<PersonnageData> personnages;
    public   List<CompetenceData> competences;
    public   List<ArmeData> armes;
    public   List<ObjetData> objet;

    }
