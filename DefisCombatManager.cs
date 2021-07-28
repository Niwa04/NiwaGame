using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefisCombatManager : MonoBehaviour
{
    public CombatData combatData;

    public void LoadCombat(){
        SceneManager.LoadSceneAsync("Demo/Combat/Combat");
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
