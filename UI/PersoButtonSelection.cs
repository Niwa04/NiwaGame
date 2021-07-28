using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;

public class PersoButtonSelection : MonoBehaviour
{
    public PersonnageDataManager perso;
    public void changePerso(){
        perso.GetComponent<CompanionInput>().goPointNull();
        if(perso != null)
            FindObjectOfType<MoveCircle>().companion = perso.GetComponent<CompanionInput>();
        else
            FindObjectOfType<MoveCircle>().companion = null;
    }
}
