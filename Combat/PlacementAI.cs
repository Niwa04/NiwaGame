using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;
public class PlacementAI : MonoBehaviour
{
    Gambit gambit;
    CompanionInput companion;

  public float timer;

    // Start is called before the first frame update
    void Start()
    {
       gambit = GetComponent<Gambit>();
       companion = GetComponent<CompanionInput>();
        timer = 1f;
    }

    // Update is called once per frame
    void Update()
    {

         if(gambit.cibleCurrent == null){
            timer -= Time.deltaTime;
         }

         if(timer < 0f){
            timer = 3f;
            Debug.Log(transform.name+" va chercher a se placer autrepart");
            GameObject empty = new GameObject();
            
            GameObject cible = FindCible.findHeroLePlusProche(transform);
            if(cible == null)
                return;
            GameObject pivot = Instantiate(empty);
            pivot.transform.position = cible.transform.position;
            
            GameObject obj = Instantiate(empty);
            obj.transform.position = transform.position;

            obj.transform.SetParent(pivot.transform);
            float rdm1 =  UnityEngine.Random.Range(-33,33);

            pivot.transform.rotation = pivot.transform.rotation * Quaternion.Euler(0, rdm1, 0);

            /*float rdm1 =  UnityEngine.Random.Range(-5,5);
            float rdm2 =  UnityEngine.Random.Range(-1,1);
            



            Vector3 decal = new Vector3(rdm1,0f,rdm2);
            Vector3 newpo = transform.position + decal;
            obj.transform.position = newpo;*/
            companion.goPoint(obj);
            Destroy(obj, 2f);
         }

      
    }
}
