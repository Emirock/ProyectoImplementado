using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ManagerEscenarios : MonoBehaviour {

    public GameObject SueloTesla, EscTesla, EscEinstein, EscMarie;
    Random r = new Random();
    public int al;
    // Use this for initialization
    void Start () {

        System.Random randomGenerate = new System.Random();
        al = randomGenerate.Next(1,100);

        if (al <=33 )
        {
            //Escenario de Tesla
            DeactivateChildren(EscEinstein, false);
            DeactivateChildren(EscMarie, false);

        }
        else if (al <= 66 && al >33)
        {
            //Escenario de Marie
            DeactivateChildren(EscEinstein, false);
            DeactivateChildren(EscTesla, false);
            DeactivateChildren(SueloTesla, false);
        }
        else if (al <= 100 && al > 66)
        {
            //Escenario de Einstein
            DeactivateChildren(EscMarie, false);
            DeactivateChildren(EscTesla, false);
            DeactivateChildren(SueloTesla, false);
        }
        //DeactivateChildren(Button, false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void DeactivateChildren(GameObject g, bool a)
    {
        g.SetActive(a);

    }
}
