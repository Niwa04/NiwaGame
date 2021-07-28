using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGalerie : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quitter(){
        menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
