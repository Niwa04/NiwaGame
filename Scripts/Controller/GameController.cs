using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Doublsb.Dialog;
using ThirdPersonCamera;

namespace TPCWC{
	public class GameController : MonoBehaviour {

		public bool android;
		public GameData gameData;
		public GameObject joyesticks;
		public GameObject UiPc;

		public GameObject cameraAndroid;
		public GameObject cameraPC;

		#region Inspector Variables
		
		[Header("-- Game Controller Properties --")]
		[Tooltip("Both Player and Companion Input Managers")]
		public  InputManager[] players;
		[Tooltip("Setting current Companion as inactive Player")]
		private  List<InputManager> inactivePlayers = new List<InputManager>();

		[Tooltip("Setting current Player as active Player")]
		public InputManager activeP;	
		#endregion
		public int indexJ;

		public bool canSwitch = true;
		public float time;
    public List<GameObject> checkpoints;   
	private int currentCP;


		void Start () {
			InitializePlayers();
			indexJ = 0;
			currentCP = 0;
			try{
				if(!android){
					joyesticks.SetActive(false);
					cameraAndroid.SetActive(false);
					cameraPC.SetActive(true);
					UiPc.SetActive(true);
				}else{
					joyesticks.SetActive(true);
					cameraAndroid.SetActive(true);
					cameraPC.SetActive(false);
										UiPc.SetActive(false);

				}
			}catch{}
		}

		//Initialize
		void InitializePlayers(){
			activePlayer(activeP);
		}

		// Update is called once per frame
		void Update () {
		   	Time.timeScale = time; 

        	if(canSwitch && Input.GetKeyDown("t")){
				switchPlayer();
			}

			if(Input.GetKeyDown("u")){
				teleport();
			}

			if(Input.GetKeyDown("f")){
				ToggleFollowInactivePlayers();
			}

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		} 

		public void teleport(){
			activeP.GetComponent<NavMeshAgent>().enabled  = false;
				activeP.gameObject.transform.position = checkpoints[currentCP].transform.position;
				activeP.GetComponent<NavMeshAgent>().enabled  = true;
				currentCP++;
				if(currentCP == checkpoints.Count){
					currentCP =0;
				}
		}

		//Main method for switching players
		public void switchPlayer()
		{
			
			restAllPlayersSettings();

			InputManager persoActuel = players[indexJ]	;
			InputManager nexPerso = findNextPlayer();

			disablePersonnage(persoActuel);
			enablePersonnageAndSetAsMainPlayer(nexPerso);	
			activePlayer(nexPerso);

		}

		public InputManager findNextPlayer(){
			indexJ++;
			if(indexJ == players.Length){
				indexJ = 0;
			}
			return players[indexJ];
			
		}


		public GameObject findPlayer(string name){
			foreach (var item in players)
			{
				if(item.name == name){
					return item.gameObject;
				}
			}return null;
		}

		public void activePlayer(string name){
			InputManager p = null;
			foreach (var item in players)
			{
				if(item.name == name)	
					p = item;
			}
			enablePersonnageAndSetAsMainPlayer(p);
		}

		public void activePlayer(InputManager p){
			inactivePlayers.Clear();
			foreach (var item in players)
			{
				if(p == item){
					activeP = p;
				}else{
					inactivePlayers.Add(item);
					item.GetComponent<CompanionInput>().followTarget = p.transform;

				}
			}
		}
		
		public void restAllPlayersSettings(){
		foreach (var item in players)
			{
				restPlayerSettings(item);
			}
		}



		public void restPlayerSettings(InputManager p){
			p.GetComponent<CompanionInput>().canFollow = false;
			p.GetComponent<CompanionInput>().chase = false;
			p.GetComponent<CompanionInput>().attack = false;
			p.GetComponent<CharacterMotor>().run = false;
			p.GetComponentInChildren<Animator>().SetBool("run", false);
			p.GetComponent<CharacterMotor>().vertical = 0;
			p.GetComponent<CharacterMotor>().deltaMovement = 0;
			p.GetComponentInChildren<Animator>().SetFloat("vertical", 0);
		}

		public void disablePersonnage(InputManager p){
			p.enabled = false;
			p.vertical = 0;
			p.GetComponent<CharacterMotor>().run = false;
			p.charMotor.animator.SetFloat("vertical", p.vertical);
			p.GetComponent<CompanionInput>().enabled = true;
			p.GetComponent<Rigidbody>().isKinematic = true;
		}

		public void enablePersonnageAndSetAsMainPlayer(InputManager p){
			p.enabled = true;
			p.GetComponent<Rigidbody>().isKinematic = false;
			p.GetComponent<CompanionInput>().enabled = false;
			if(!android)
			FindObjectOfType<CameraController>().PlayerTarget = p.transform;
			else{
				try
				{
					cameraAndroid.GetComponent<ThirdPersonCamera.CameraController>().target = p.gameObject.GetComponentInChildren<LocationCameraFollow>().transform;
					cameraAndroid.GetComponent<ThirdPersonCamera.CameraController>().desiredDistance = 4f;
					//cameraAndroid.GetComponent<Animator>().SetTrigger("play");
				}
				catch (System.Exception)
				{
				}
			}
		}

		public void ToggleFollowInactivePlayers(){
			foreach (var item in inactivePlayers)
			{
				Debug.Log(transform.name+" follow");
				try
				{	
					float rdm =  UnityEngine.Random.Range(3f,5f);

					item.GetComponent<CompanionInput>().goPoint(activeP.gameObject, rdm);

				}
				catch (System.Exception)
				{

				}
			}
		}

		public void ChangeCameraFollow(){
			if(GameObject.Find("Camera Controller").GetComponent<CameraController>().enabled)
				GameObject.Find("Camera Controller").GetComponent<CameraController>().enabled = false;
			else
			{
				GameObject.Find("Camera Controller").GetComponent<CameraController>().enabled = true;

			}
		}
		public void changeActivationCinematique(Text text){
			gameData.cinematiqueActive = !gameData.cinematiqueActive;
			if(gameData.cinematiqueActive)
				text.text = "Cinematique Activé";
			else
			{
				text.text = "Cinatique Desactivé";
			}
		}

		public void changeActivationCombat(Text text){
			gameData.combatActive = !gameData.combatActive;
			if(gameData.combatActive)
				text.text = "Combat Activé";
			else
			{
				text.text = "Combat Desactivé";
			}
		}

		public void interaction(){
			StartCoroutine(ClickInteraction());
		}

		IEnumerator ClickInteraction(){
			gameData.interaction = true;
    		yield return new WaitForSeconds(1f);
			gameData.interaction = false;

		}
	}

}