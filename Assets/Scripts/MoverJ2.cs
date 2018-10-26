using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverJ2 : MonoBehaviour {
    public float vida = 100f;
    public string TagEnemigo = "Player";
    public bool Attack = false;
    public GameObject Enemigo;
    public GameObject Particulas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        //Caminar y salto
        if (Input.GetKey(KeyCode.RightArrow))
        {

            if (GetComponent<SpriteRenderer>().flipX == false) ;
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            GetComponent<Animator>().SetBool("Caminar", true);
            transform.Translate(0.07f, 0, 0);

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (GetComponent<SpriteRenderer>().flipX == true) ;
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            GetComponent<Animator>().SetBool("Caminar", true);
            transform.Translate(-0.07f, 0, 0);

        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Animator>().SetBool("Salto", true);
            transform.Translate(0, 0.15f, 0);
        }



        //************************************************************************

        //Golpes
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponent<Animator>().SetBool("GolpeMedio", true);
            Attack = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetComponent<Animator>().SetBool("Patada", true);
            Attack = true;
        }


        //*************************************************************************

        //Desactivar animaciones
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            GetComponent<Animator>().SetBool("Caminar", false);
            GetComponent<Animator>().SetBool("Salto", false);
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            GetComponent<Animator>().SetBool("GolpeMedio", false);
            
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            GetComponent<Animator>().SetBool("Patada", false);
            
        }
        
    }

    //*****************************************************************************

    //Hacer que se golpeen y reciban daño

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(TagEnemigo) && Attack) {
            
            Debug.Log("Entro al ataque");
            collision.gameObject.SendMessage("ApplyDamageP1", 5.0f);
            Attack = false;
            GetComponent<Animator>().SetBool("GolpeMedio", false);
            GetComponent<Animator>().SetBool("Patada", false);

        }
        else if (collision.tag.Equals("SpecialTesla"))
        {
            vida -= 15.0f;
            GetComponent<Animator>().SetTrigger("Daño");
            Destroy(collision.gameObject);
            Instantiate(Particulas,transform.position,Quaternion.identity);
        }
    }


    public void ApplyDamageP2(float damage)
    {
        vida -= damage;
        Debug.Log("Si baja daño");
        GetComponent<Animator>().SetTrigger("Daño");
        if (vida <= 0)
        {
            GetComponent<Animator>().SetBool("Muerto", true);

        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(950, 0, 100, 50), new GUIContent("Player 2: " + vida));
    }
}
