using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "Arme", menuName = "Personnage/Arme")]
public class ArmeData : ScriptableObject {

    public GameObject model3D;
    public int puissance;

    public Sprite logo;

    public Caracteristique caracteristique;

}
