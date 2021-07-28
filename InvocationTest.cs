using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocationTest : MonoBehaviour
{   
    public GameObject feu;
    public GameObject eau;
    public GameObject terre;
    public GameObject vent;

    public void clicka(int i){
        feu.SetActive(false);
        eau.SetActive(false);
        terre.SetActive(false);
        vent.SetActive(false);

        switch (i)
        {
            case 0:
                feu.SetActive(true);
                break;
            case 1:
                eau.SetActive(true);
                break;
            case 2:
                terre.SetActive(true);
                break;
            case 3:
                vent.SetActive(true);
                break;
            
            default:
            break;
        }
    }
}
