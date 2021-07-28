using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;

public static class FindAction
{
      
     public static CompetenceData findCompetenceByNameIn(CompetenceData[] competences, string name){

        foreach (var c in competences)
        {
            if(c.name == name){
                return c;
            }
        }
        return null;
    }
    
}
