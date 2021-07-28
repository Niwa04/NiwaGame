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
    // Update is called once per frame
    void Update()
    {
        
    }

   public void UpdateButtonStrategie(){
        GameObject a = FindObjectOfType<GameController>().activeP.transform.gameObject;
       PersonnageDataManager perso = a.GetComponent<PersonnageDataManager>();
       string name = perso.perso.strategieCurrent.name;
        Strategie[] strats = perso.perso.strategies;
        for (int i = 0; i < 3; i++)
        {
            try
            {
                    imageStrategie[i].sprite = strats[i].logo;
        
            }
            catch (System.Exception)
            {
            }
        }
    }


}
