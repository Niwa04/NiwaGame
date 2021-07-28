    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcusGambit : Gambit
{
    private int index = 0;
    public float a;
    public override void preparAction()
    {
        actionDeBase();
        //actionEnAttente = hitScript.action;
    }

    private void actionDeBase()
    {
        CompetenceData co = perso.comps.defaut;
        cibleCurrent = FindCible.Find(transform,co.cible);

        if (cibleCurrent == null)
        {
            hitScript.action = null;
            return;
        }
        float dis = Vector3.Distance(cibleCurrent.transform.position, transform.position);
        if (dis > co.range && dis < (co.range+1) && co.preAction == EnumPreAction.SeRapprocher)
        {
            companion.chase2(co.range, cibleCurrent);
            hitScript.action = null;
        }
        else
        {   
            if(dis <= (co.range+0.1f)){
                actionManager.prepareAction(co, co.animationIndex, co.apparition.ToString(), cibleCurrent);
            }
            else{
                                hitScript.action = null;
            }
        }
    }

    public override MyAction useCrystal(string name)
    {
        GameObject cible = FindCible.findEnemyLePlusProche(transform);
        float dis = Vector3.Distance(cible.transform.position, transform.position);
        actionManager.prepareAction("Foudre", 5f, "", cible);
        
        CompetenceData co = null;
         switch (name)
        {
            case "Feu+":
                co = perso.comps.feuX;
                break;
            case "Eau+":
                co = perso.comps.eauX;
                break;
            case "Vent+":
                co = perso.comps.ventX;
                break;
            case "Terre+":
                co = perso.comps.terreX;
                break;
             case "Feu":
                co = perso.comps.feu;
                break;
            case "Eau":
                co = perso.comps.eau;
                break;
            case "Vent":
                co = perso.comps.vent;
                break;
            case "Terre":
                co = perso.comps.terre;
                break;
            default:
                co = perso.comps.defaut;
                break;
        }
        if(co == null)
            co = perso.comps.defaut;
        cibleCurrent = FindCible.Find(transform,co.cible);

        actionManager.prepareAction(co,co.animationIndex,co.apparition.ToString(),cibleCurrent);

        animator.SetTrigger("Action");

        hitScript.afficheCercleMagic();
        useCristal = true;
        GetComponent<CristalScript>().deleteCrystal(name);
     //   StartCoroutine(actionManager.useCristal(hitScript.action.competence.timeCanalisation));
        return null;
    }


}
