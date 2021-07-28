using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TPCWC{
	
	[CustomEditor(typeof(CompanionInput))]
	public class CompanionInputEditor : Editor {

		bool showHelp;
		public override void OnInspectorGUI(){
			EditorGUILayout.BeginHorizontal("box");
        	
        	GUILayout.Label("Companion Input Script", EditorStyles.helpBox);

        	GUIStyle bStyle = new GUIStyle("box");
        	bStyle.normal.textColor = Color.yellow;
 
        	if(GUILayout.Button("[?]", bStyle)){
        		showHelp = !showHelp;
        	}

        	EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginVertical("box");

			if(showHelp){
				EditorGUILayout.HelpBox("This script controls the player with artificial intelligence and make decisions based on the surroundings like when to fight or follow the player.\n\nYou can easily manipulate the following variables of this particular Companion to make your own unique AI :\n\nTag To Attack :\tDefine which tag you want your companion to attack.(By Default it is set to 'Enemy').\n\nFollow Target :\tThe target you want this companion to follow. Set it to the player or you can change this via script to make him follow something else.\n\nAttack Target :\tIt wil be autoasigned to the nearby 'Tag To Attack' when it comes in the attack distance.\n\nCompanion Sensor : The trigger collider of this companion which is responsible for all the senses.It will be disabled if this Companion becomes Player.\n\nAwareness Radius : The radius of the Companion Sensor Trigger.\n\nFollow Speed :\tDefine how fast the companion follows the 'Follow Target'\n\nStopping Distance : Define how far the companion stops from the 'Follow Target'.\n\nAttack Distance :\tDefine from how far you want the Companion to attack at 'Tag To Attack'\n(This should always be less then 'Awareness Radius').\n\nAttack Delay :\tDefine how much this Companion waits between his Attacks.\n\nCan Follow, Chase and Attack are the AI states. These are here for debug purposes so you can see at RUNTIME in which state the Companion is currently in!", MessageType.Info);
    		}

			base.DrawDefaultInspector();

			EditorGUILayout.EndVertical();
		}
    
	}
}