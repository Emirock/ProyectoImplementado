using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public int contador=0;
    public Text J1, J2;
    public GameObject Button;
    public string n1, n2;

	// Use this for initialization
	void Start () {
        DeactivateChildren(Button, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel(string nombreBoton)
    {
        SceneManager.LoadScene(nombreBoton);
    }

    public void ElegirPersonaje(string nombreBoton)
    {
        if (contador%2 ==0)
        {
            J1.text ="Jugador 1: " + nombreBoton;
            contador += 1;
            n1 = nombreBoton;
        }else if(contador % 2 != 0)
        {
            J2.text = "Jugador 2: " + nombreBoton;
            DeactivateChildren(Button, true);
            contador += 1;
            n2 = nombreBoton;
        }
    }

    void DeactivateChildren(GameObject g, bool a)
    {
        g.SetActive(a);

    }

    

    /*public void setParameters()
    {
        MoverJ1 j1 = new MoverJ1();
        MoverJ2 j2 = new MoverJ2();

        j1.setJ1(n1);
        j2.setJ1(n1);
        j1.setJ2(n2);
        j2.setJ2(n2);
    }*/
}

