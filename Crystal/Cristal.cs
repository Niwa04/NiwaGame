using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal
{

    public string type;

    public Cristal(string type){
        this.type = type;
    }
    
        public Cristal(){
        this.type = "";
    }
    

      public override string ToString()
    {
        return "Cristal : " + this.type;
    }

}
