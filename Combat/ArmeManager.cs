using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<ArmeLocation>();
        ArmeData arme = GetComponent<PersonnageDataManager>().perso.arme;
     //   Debug.Log(transform.name+" "+GetComponentInChildren<ArmeLocation>());
        GameObject obj = Instantiate(arme.model3D,GetComponentInChildren<ArmeLocation>().transform.position, GetComponentInChildren<ArmeLocation>().transform.rotation);
      //  obj.transform.position = GetComponentInChildren<ArmeLocation>().transform.position;
        obj.transform.SetParent(GetComponentInChildren<ArmeLocation>().transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
