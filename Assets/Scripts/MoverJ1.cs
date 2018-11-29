using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MoverJ1 : MonoBehaviour {
    public float vida = 100f, hp=100f;
    public string TagEnemigo = "Enemigo";
    public bool Attack = false;
    public GameObject Enemigo;
    public GameObject SpecialAttack;
    public GameObject Particulas;
    public Text Win;
    public Image HealthBar;
    private bool IsRight = true;
    private bool CanDoSpecial = true;
    
    // Use this for initialization
    void Start () {
        hp = vida;
    }

    void Update()
    {

        //Caminar y salto
        if (Input.GetKey(KeyCode.D))
        {

            IsRight = true;

            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            GetComponent<Animator>().SetBool("Caminando", true);
            transform.Translate(0.07f, 0, 0);

        }
        if (Input.GetKey(KeyCode.A))
        {
            IsRight = false;

            if (GetComponent<SpriteRenderer>().flipX == false) 
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            GetComponent<Animator>().SetBool("Caminando", true);
            transform.Translate(-0.07f, 0, 0);

        }

        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Animator>().SetBool("Salto", true);
            transform.Translate(0, 0.15f, 0);
        }



        //************************************************************************

        //Golpes y ataques especiales
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Animator>().SetBool("GolpeMedio", true);
            Attack = true;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<Animator>().SetBool("Patada", true);
            Attack = true;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (CanDoSpecial) {

                CanDoSpecial = false;

                Invoke("ResetSpecial", 3);

                if (IsRight)
                {
                    GameObject go = Instantiate(SpecialAttack, new Vector3(transform.position.x + 5.7f, transform.position.y + 1.5f, 6.4f), Quaternion.identity) as GameObject;
                }
                else
                {
                    GameObject go = Instantiate(SpecialAttack, new Vector3(transform.position.x + 2.0f, transform.position.y + 1.5f, 6.4f), Quaternion.identity) as GameObject;
                    go.SendMessage("ChangeSide");
                }
            }
        }
        //*************************************************************************

        //Desactivar animaciones
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("Caminando", false);
            GetComponent<Animator>().SetBool("CaminandoReversa", false);
            GetComponent<Animator>().SetBool("Salto", false);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            GetComponent<Animator>().SetBool("GolpeMedio", false);

        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            GetComponent<Animator>().SetBool("Patada", false);

        }

    }

    //*****************************************************************************

    public void ResetSpecial()
    {
        CanDoSpecial = true;
    }

    //Hacer que se golpeen y reciban daño

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(TagEnemigo) && Attack)
        {

            Debug.Log("Entro al ataque");
            collision.gameObject.SendMessage("ApplyDamageP2", 5.0f);
            Attack = false;
            GetComponent<Animator>().SetBool("GolpeMedio", false);
            GetComponent<Animator>().SetBool("Patada", false);

        }
        else if (collision.tag.Equals("SpecialAlbert"))
        {
            vida -= 15.0f;
            HealthBar.transform.localScale = new Vector2(vida / hp, 1);
            GetComponent<Animator>().SetTrigger("Daño");
            Destroy(collision.gameObject);
            Instantiate(Particulas, transform.position, Quaternion.identity);
        }
    }


    public void ApplyDamageP1(float damage)
    {
        vida -= damage;
        
        Debug.Log("Si baja daño");
        HealthBar.transform.localScale = new Vector2(vida/hp,1);
        Debug.Log(vida + " / " + hp);
        GetComponent<Animator>().SetTrigger("Daño");
        if (vida <= 0)
        {
            GetComponent<Animator>().SetBool("Muerto", true);
            // Hacer que muestren unk
            Win.text ="Gana Einstein";
        }
        
    }

    
}
