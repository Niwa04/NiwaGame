using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;
public class AfficheScript : MonoBehaviour
{
    
    public GameObject obj;

    private void OnTriggerEnter(Collider other) {
                if(other.gameObject.tag == "Player" && FindObjectOfType<GameController>().activeP.transform.name == other.transform.name)

        obj.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
