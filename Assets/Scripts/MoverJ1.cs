using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverJ1 : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        
    }
   
   
    // Update is called once per frame
    void Update () {
        
        //Caminar y salto
        if (Input.GetKey(KeyCode.D)) {
            
            if (GetComponent<SpriteRenderer>().flipX == true) ;
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            GetComponent<Animator>().SetBool("Caminando", true);
            transform.Translate(0.07f, 0, 0);
            
        }
        if (Input.GetKey(KeyCode.A)) {
            if (GetComponent<SpriteRenderer>().flipX == false) ;
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            GetComponent<Animator>().SetBool("Caminando", true);
            transform.Translate(-0.07f, 0, 0);
           
        }

        if (Input.GetKey(KeyCode.W)) {
            GetComponent<Animator>().SetBool("Salto", true);
            transform.Translate(0, 0.15f, 0);
        }

        

        //************************************************************************

        //Golpes
        if (Input.GetKeyDown(KeyCode.R)) {   
            GetComponent<Animator>().SetBool("GolpeMedio", true);
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            GetComponent<Animator>().SetBool("Patada", true);
        }


        //*************************************************************************

        //Desactivar animaciones
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W)) {
            GetComponent<Animator>().SetBool("Caminando", false);
            GetComponent<Animator>().SetBool("CaminandoReversa", false);
            GetComponent<Animator>().SetBool("Salto", false);
        }

        if (Input.GetKeyUp(KeyCode.R)){
            GetComponent<Animator>().SetBool("GolpeMedio", false);
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            GetComponent<Animator>().SetBool("Patada", false);
        }

    }
}
