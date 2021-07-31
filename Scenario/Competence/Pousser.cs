using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

using TPCWC;

public class Pousser : MonoBehaviour
{
    public float m_Thrust = 20f;
    public float forceMarcus;
    public float durer;
    private float timer;
    private bool canPousse;

    public GameObject obj;
    void Start()
    {
        timer = 0f;
        canPousse = true;
    }

    // Update is called once per frame
    void Update()
    {
       if(timer > 0f){
            obj.transform.Translate(transform.forward * m_Thrust*Time.deltaTime);           
            FindObjectOfType<GameController>().activeP.GetComponent<Rigidbody>().AddForce(transform.forward * forceMarcus);
            timer -= Time.deltaTime;
       }
       if(timer < 0f){
           timer = 0f;
            FindObjectOfType<GameController>().activeP.GetComponent<CapsuleCollider>().radius = 0.3f;
            Debug.Log("Marcus : J'ai fini de pousse !");
            FindObjectOfType<CharacterMotor>().enabled = true;
            canPousse = true;

       }
    }


    private void OnTriggerStay(Collider other) {
        
        if (canPousse && Input.GetKeyDown("w") && FindObjectOfType<GameController>().activeP.gameObject.name == "Marcus" && other.gameObject.name == "Marcus")
        {
            FindObjectOfType<CharacterMotor>().enabled = false;
            Debug.Log("Marcus : Je pousse !");
            FindObjectOfType<GameController>().activeP.transform.rotation = Quaternion.LookRotation(transform.forward);
            FindObjectOfType<GameController>().activeP.GetComponentInChildren<Animator>().SetTrigger("pousse");
            FindObjectOfType<GameController>().activeP.GetComponent<CapsuleCollider>().radius = 0.9f;

            timer = durer;
            canPousse = false;

        }
    }
    

    private Transform FindDirection(Transform position){
        return null;
    }

    private void OnTriggerExit(Collider other) {
        
        if (other.gameObject.name == "Marcus")
        {
            FindObjectOfType<GameController>().activeP.GetComponent<Rigidbody>().mass = 1;
        }
    }
}
