using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatData", menuName = "CombatData", order = 0)]
public class CombatData : ScriptableObject {

    public int stageCurrent = 0;
    public   List<PersonnageData> personnages;
    public List<CombatStageData> stages;

    }
