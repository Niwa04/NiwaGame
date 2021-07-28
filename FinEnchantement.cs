using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinEnchantement : MonoBehaviour
{
    void OnDestroy() {
       PersonnageData persoData = GetComponentInParent<PersonnageDataManager>().perso;   
       persoData.enchantement = "";
    }
}
