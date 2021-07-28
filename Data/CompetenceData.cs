using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Competence", order = 0)]
public class CompetenceData : ScriptableObject {

    public string description;
    public string cout;
    public string spawn;

    public EnumTypeElement element;
    public EnumTypeCible cible;

    public EnumTypeSpawn apparition;

    public EnumPreAction preAction;

    public float animationIndex;

    public int puissance;
    public float time;
    public float timeForAppli;
    public float timeCanalisation;
    public GameObject model;
    public GameObject cercle;
    public GameObject canalisation;

    public string animVariable;
    public string pointCible;

    public TypeCompetence typeComptence;    
    public float range; 

    public AudioClip audio;

    public bool aAfficher;

    public int canHit;

    public bool attache;

    public bool isCristalCompetence;

    public Strategie strategie;

}