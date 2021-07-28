using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "GameData")]
public class GameData : ScriptableObject
{
    public int SceneCourante;
    public int ScenePrecedente;
    public bool cinematiqueActive;
    public bool combatActive;

    public bool interaction;

    public bool newGame;
    public bool end;

    public List<string> evenements;
}
