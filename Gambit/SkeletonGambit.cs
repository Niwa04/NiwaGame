using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPCWC;


public class SkeletonGambit : Gambit
{

    private bool enchantement;
    private int index = 0;
    public override void preparAction(){
       
        switch (index)
      {
          case 0:
              break;
        case 8:
                break;
         case 5:
              
              break;
          case 10:
              break;
        case 15:
                index = 0;
              break;
          default:
              break;
      }
       index++;
        animator.SetTrigger("action");

    }


      public override MyAction useCrystal(string cristal){
       

       return null;
    }


}
