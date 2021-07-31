using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TPCWC;

public class UiManager : MonoBehaviour
{

    public Image[] imageStrategie;

    public Text text1;
    public Text text2;

    public Text dialogue;
    public GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
       gameData = FindObjectOfType<GameController>().gameData;
       initButton();
    }
    public void initButton(){
			if(gameData.cinematiqueActive)
				text1.text = "Cinematique Activé";
			else
			{
				text1.text = "Cinatique Desactivé";
			}
			if(gameData.combatActive)
				text2.text = "Combat Activé";
			else
			{
				text2.text = "Combat Desactivé";
			}
		}
   public IEnumerator afficheDialogue(string dialogue, float time){
        this.dialogue.text = dialogue;
         this.dialogue.gameObject.GetComponent<Animator>().SetTrigger("display");
        yield return new WaitForSeconds(time);
         this.dialogue.gameObject.GetComponent<Animator>().SetTrigger("masque");

    }

}
