using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace TPCWC
{
    public class CompanionInput : MonoBehaviour
    {

        #region Inspector Variables

        [Tooltip("Agent will attack this tagged Object")]
        public string tagToAttack = "Enemy";

        [Header("-- Companion Agent Properties --")]
        [Tooltip("Usually the player whom Companion will follow")]
        public Transform followTarget;
        [Tooltip("Will be autoassigned to the Enemy when he's in range.")]
  

        public float followSpeed = 3.25f;
        [Tooltip("Stop from player at this Distance")]
        public float stoppingDist = 2.5f;

        [Header("-- AI States")]
        [Tooltip("If true, Companion Agent will follow 'followTarget'")]
        public bool canFollow;
        [Tooltip("If true, Companion Agent will chase 'attackTarget'")]
        public bool chase;
        [Tooltip("If true, Companion Agent will attack 'attackTarget'")]
        public bool attack;

        #endregion

        #region Private Variables
        public float vertical;
        public NavMeshAgent navAgent;
        Animator animator;
        #endregion

        public Transform pointToFollow;
        public Transform targetToLook;

        public float currentDistanceToStop;

        public bool lookEnnemie;

        public GameController gameController;

        private Transform positionDeBase;

        public Vector3 transformDebut;

         public    List<GameObject> objects = new List<GameObject>();

        // Use this for initialization
        void Start()
        {
            gameController = FindObjectOfType<GameController>();
            //Initializing the Companion Input
            InitializeCompanionInput();
            positionDeBase = transform;
            transformDebut = new Vector3(gameObject.transform.localPosition.x,gameObject.transform.localPosition.y,gameObject.transform.localPosition.z);
        }

        private void Update() {
            		
			if( Input.GetKeyDown("l")){
				goPoint();
			}

        }


        //Initializing the Companion Input
        void InitializeCompanionInput()
        {
            //get NavMeshAgent ref
            navAgent = GetComponent<NavMeshAgent>();
            //override the stopping distance
            navAgent.stoppingDistance = stoppingDist;

            //find animator
            animator = GetComponentInChildren<Animator>();

        }


        //Calculating everything in FixedUpdate()
        void FixedUpdate()
        {

            
            
            //exit if CompanionInput is disabled
            if (!this.enabled)
                return;

            if(lookEnnemie && targetToLook)
                transform.LookAt(targetToLook.position);


            //else we will give WHOLE CONTROL from Input Manager to Companion Input!

            //if not chasing,attacking or not following
            if (!chase && !attack && !canFollow)
            {
                //we want our agent to stop
                navAgent.isStopped = true;
                //again lerping speed so it don't stop abruptly
                vertical = Mathf.Lerp(vertical, 0, Time.deltaTime * 5);
                //set the animator
                animator.SetFloat("vertical", vertical);

                return;

            }

            //if not chasing or attacking
            if (!chase && !attack)
            {
                //if following target
                if (followTarget && canFollow)
                {
                    //we will be setting the destination to 'followTarget'
                    navAgent.SetDestination(followTarget.position);
                    
                    //override the stopping distance
                    navAgent.stoppingDistance = currentDistanceToStop;

                    if(followTarget == pointToFollow){
                        navAgent.stoppingDistance = 0;
                        float dist = Vector3.Distance(transform.position, followTarget.transform.position);
                        if(dist < currentDistanceToStop){
                            canFollow = false;
                            lookEnnemie = true;
                        }
                    }
                    //resuming the nav mesh capabilities
                    navAgent.isStopped = false;
                    //lerping speed so it don't start running abruptly
                    navAgent.speed = Mathf.Lerp(navAgent.speed, followSpeed, Time.deltaTime * 5);
                    animator.applyRootMotion = false;
                    animator.SetFloat("vertical", vertical);

                }
            }

            //again lerping speed so it don't change velocity abruptly
            float verticalVelocity = Mathf.Lerp(vertical, navAgent.desiredVelocity.magnitude, Time.deltaTime * 5);
            //feeding into our local 'vertical' float
            vertical = Mathf.Clamp01(verticalVelocity);

            if (animator)
            {
                //disable root motion while as a companion
                animator.applyRootMotion = false;
                animator.SetFloat("vertical", vertical);
            }

        }

        public void goPoint(){
            followTarget = pointToFollow;
            ToggleFollow();
        }

        public void goPoint(GameObject obj){
            if(obj == null)
                return;
            followTarget = obj.transform;
            navAgent.isStopped = false;
            canFollow = true;
            currentDistanceToStop = stoppingDist;
        }

          public void goPoint(GameObject obj, float dist){
            if(obj == null)
                return;
            followTarget = obj.transform;
            navAgent.isStopped = false;
            canFollow = true;
            currentDistanceToStop = dist;
        }

    

        public void goPointNull(){
            followTarget = null;
            navAgent.isStopped = true;
            canFollow = true;
        }
        //Toggle between follow or not follow!
        //called from the GameController!
        public void ToggleFollow()
        {

            //toggle bool
            canFollow = !canFollow;
            currentDistanceToStop = 3f;
            //if bool turns to not follow
            if (!canFollow)
            {
                //we simply stop our agent
                navAgent.isStopped = true;
                navAgent.speed = 0;

                vertical = 0;
                animator.SetFloat("vertical", vertical);
            }
            //else if bool turns to follow
            else
            {
                try
                {
                     //we simply resumes our agent
                    navAgent.isStopped = false;

                }
                catch (System.Exception)
                {
                    
                }
               

            }
        }

        public void followCible(){
            foreach (var player in FindObjectOfType<GameController>().players)
            {
                player.gameObject.GetComponent<CompanionInput>().goPointNull();
            }     
            FindObjectOfType<MoveCircle>().companion = this;
            FindObjectOfType<GameController>().activeP = GetComponent<InputManager>();
            FindObjectOfType<CombatManager>().cercleSelect.transform.position = FindObjectOfType<GameController>().activeP.transform.position;
            FindObjectOfType<CombatManager>().cercleSelect.transform.SetParent(FindObjectOfType<GameController>().activeP.transform);
            FindObjectOfType<CrystalManager>().afficherCrystals();
             FindObjectOfType<CameraControllerStrategie>().target = FindObjectOfType<GameController>().activeP.transform;
           // FindObjectOfType<GameController>().cameraCombat.transform.position = transform.position;
           // GameObject cible =  FindCible.findEnemyLePlusProche(transform);
         //   FindObjectOfType<GameController>().cameraCombat.transform.LookAt(cible.transform);
        }

        private int indexStrategieCurrent(){
         /*   Strategie[] stras =  GetComponent<PersonnageDataManager>().perso.strategies;
            Strategie current = GetComponent<PersonnageDataManager>().perso.strategieCurrent;
            for (int i = 0; i < 3; i++)
            {
                if(stras[i].name == current.name ){
                    return i;
                }
            }*/
            return 0;
        }

        public void chase2(float distance,GameObject cible){
            goPoint(cible,distance);
            StartCoroutine(prepareStopChase());
        }


        IEnumerator prepareStopChase(){
             yield return new WaitForSeconds(3f);
             goPointNull();
        }

        public void changeDePlace(float dist){
            float a = UnityEngine.Random.Range(-dist,dist);
            float b = UnityEngine.Random.Range(-dist,dist);
                      
            transform.position = positionDeBase.position;
          
             transform.Translate(a,b,0);
        }

        public bool hasEnchantement(string enchant){
            BuffScript[] bs = GetComponents<BuffScript>();
            foreach (BuffScript item in bs)
            {
                if(item.type == enchant){
                   return true;
                }
            }
            return false;
        }


    }
}