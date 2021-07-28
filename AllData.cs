using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllData", menuName = "AllData", order = 0)]
public class AllData : ScriptableObject {

    public   List<PersonnageData> personnages;

    public List<ObjetData> book;

    }
