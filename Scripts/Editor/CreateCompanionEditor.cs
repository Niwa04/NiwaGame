using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;


namespace TPCWC{
	public class CreateCompanionEditor : EditorWindow {

		AnimatorController animator;
		GameObject model;
		Transform p1_im;
		string companionName = "TPCWC Companion";

		[MenuItem("TPCWC/Create Character/Companion #&c", false, 2)]
		public static void OpenCompanionCreatorWindow(){

			GetWindow<CreateCompanionEditor>("Companion");
			GetWindow<CreateCompanionEditor>("Companion").minSize = new Vector2(400,300);

		}

		void OnGUI(){

			GUILayout.Label("Companion Creator Window!", EditorStyles.centeredGreyMiniLabel);

			GUILayout.Space(15);
			GUILayout.BeginVertical("box");
			
			animator = (AnimatorController)EditorGUILayout.ObjectField("Animator", animator, typeof(AnimatorController), true) ;
			model = (GameObject) EditorGUILayout.ObjectField("Companion Model", model, typeof(GameObject),true);

			if(model && !model.GetComponent<Animator>())
				EditorGUILayout.LabelField("Please Select a valid GameObject as 'Companion Model' with Animator Attached!", EditorStyles.helpBox);
			else
			{
				if(model != null && animator != null){

					if(GUILayout.Button("Create Companion"))
					{
									companionName = model.name;

						AddComponentsToCompanion();
					}

				}else
				{
					GUILayout.Label("Assign all fields to proceed.", EditorStyles.centeredGreyMiniLabel);
				}
			}

			CreateOtherButtons();

			GUILayout.EndVertical();

		}


		void AddComponentsToCompanion(){

			//Create Empty Instance
			GameObject companionRes;
			GameObject childModel;

			//Instantiate Proper Objects
			companionRes = Instantiate(Resources.Load("Player/CompanionResource")) as GameObject;
			childModel = Instantiate(model) as GameObject;

			//Apply animator Controller 
			childModel.GetComponent<Animator>().runtimeAnimatorController = animator;
			childModel.GetComponent<Animator>().applyRootMotion = false;	
			
			//Set it as child
			childModel.transform.SetParent(companionRes.transform);
			childModel.transform.localPosition = Vector3.zero;
			childModel.transform.localRotation = Quaternion.identity;

			//Set reference in Script
			companionRes.GetComponent<CharacterMotor>().modelMesh =  childModel;

			//change Name
			companionRes.name = companionName;

			//Load other needed components
			GameObject gameController;

			gameController = FindObjectOfType<GameController>().gameObject;
			//gameController.GetComponent<GameController>().p2 = companionRes.GetComponent<InputManager>();
			gameController.name = "Game Controller";


			GetWindow<CreateCompanionEditor>("companion").Close();

			//Debug appear if everything is succesful
			Debug.Log(companionName + " Created!");

		}

		void CreateOtherButtons(){

			GUILayout.BeginHorizontal(EditorStyles.helpBox);

			if(GUILayout.Button("Watch Tutorial")){

				Application.OpenURL("https://youtu.be/AUrzYjLVfjQ");
			}

			if(GUILayout.Button("Visit Forum")){

				Application.OpenURL("www.forum.unity3d.com");
			}

			GUILayout.EndHorizontal();
		}

	}
}