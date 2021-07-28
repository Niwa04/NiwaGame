using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "Personnage/Hero")]
public class PersonnageData : ScriptableObject {

    public GameObject model3D;
    public Sprite sprite;
    public float range= 2f;
    public int hpMax = 50;
   // public int hpCurrent;

    public PersoCurrentCompData comps;
    public string enchantement;

    public Strategie strategieCurrent;
    public Strategie[] strategies;

    public Caracteristique caracteristique;
    public Caracteristique caracteristiqueCurrent;

    public Caracteristique getCurrentCaracteristique(){
        return caracteristiqueCurrent;
    }

    public Vector3 position;
    public  Quaternion  rotation;

    public AudioClip[] audioSource;

    public AudioClip[] deathAudio;
    public AudioClip[] hitAudio;
    public AudioClip[] frappeAudio;
    
    public AudioClip[] provocationAudio;
    public AudioClip[] specialAttackAudio;
    public PersoArmeData persoArmeData;

    public ArmeData arme;

    public bool newScene;

}
