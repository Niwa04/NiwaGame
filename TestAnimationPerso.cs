using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TestAnimationPerso : MonoBehaviour
{
    public Animator animator;
    float atk;
    float comp;
    float hit;
    int arm;

    GameObject currentArme;

    public GameObject perso;
    
    public GameObject pet;
    public PlayableDirector timeline;
    bool canAnim;
    // Start is called before the first frame update
    void Start()
    {
        atk = 0f;
        comp = 3f;
        hit = 0f;
        arm = 0;
        ChangeArme();
        canAnim = true;
    }


    IEnumerator changeCanAnim(){
        canAnim = false;
        yield return new WaitForSeconds(1.5f); 
        canAnim = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    public void Attaque(){
        if(!canAnim)
            return;
        animator.SetFloat("ActionIndex",atk);
        atk++;
        if(atk == 3){
            atk = 0;
        }

        if(pet != null){
            pet.GetComponentInChildren<Animator>().SetFloat("ActionIndex",atk);
            animator.SetFloat("ActionIndex",0f);

            pet.GetComponentInChildren<Animator>().SetTrigger("Action");
        }else{
            animator.SetTrigger("Action");

        }

        
        StartCoroutine(changeCanAnim());
    }
    public void Competence(){
        if(!canAnim)
            return;
        animator.SetFloat("ActionIndex",comp);
        animator.SetTrigger("Action");
        comp++;
        if(comp == 6){
            comp = 3;
        }
                StartCoroutine(changeCanAnim());

    }

    public void Hit(){
        if(!canAnim)
            return;
        animator.SetTrigger("hit");
                animator.SetFloat("HitIndex",hit);
        if(pet != null){
            if(hit == 1)
                pet.GetComponentInChildren<Animator>().SetFloat("HitIndex",hit);
                pet.GetComponentInChildren<Animator>().SetTrigger("hit");

        }
        hit++;
        if(hit == 3){
            hit = 0;
        }
        StartCoroutine(changeCanAnim());

    }

    public void Death(){
        if(!canAnim)
            return;
        animator.SetTrigger("death");
        StartCoroutine(changeCanAnim());

    }


    public void AttaqueSpecial(){
        if(!canAnim)
            return;
        timeline.Play();
    }


    public void ChangeArme(){
      
        if(currentArme != null){
            Destroy(currentArme);
        }
        try
        {
            
                
                ArmeData arme = null;
                if(perso.GetComponent<PersonnageDataManager>().perso.persoArmeData)
                    arme = perso.GetComponent<PersonnageDataManager>().perso.persoArmeData.armes[arm];
                else   
                    arme = perso.GetComponent<PersonnageDataManager>().perso.arme;
        
                currentArme = Instantiate(arme.model3D,perso.GetComponentInChildren<ArmeLocation>().transform.position, perso.GetComponentInChildren<ArmeLocation>().transform.rotation);
                currentArme.transform.SetParent(perso.GetComponentInChildren<ArmeLocation>().transform);
                arm++;
                if(arm == 3)
                    arm = 0;
                
        }
        catch (System.Exception)
        {
            
        }

    }
     
}
