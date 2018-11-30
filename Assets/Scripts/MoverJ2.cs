using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoverJ2 : MonoBehaviour {
    public float vida = 100f, hp = 100f, mana = 100f, mn = 100f;
    public string TagEnemigo = "Player", J1, J2;
    public bool Attack = false;
    public GameObject Enemigo;
    public GameObject SpecialAttack;
    public GameObject Particulas, other1, other2, other3;
    public Text Win;
    public Image HealthBar, ManaBar;
    private bool IsRight = true;
    private bool CanDoSpecial = true;
    // Use this for initialization
   

    public MoverJ2() { } 

    public void setJ1(string J1)
    {
        this.J1 = J1;
    }

    public void setJ2(string J2)
    {
        this.J2 = J2;
    }

 void Start() {
        if (J2 == "Tesla")
        {
            Destroy(other2, 0.1f);
            Destroy(other3, 0.1f);
            // Se quitan todos los jugadores de la escena que no son J1 del lado izquierdo, se setean los sprites, 
        }
        else if (J2 == "Marie")
        {
            Destroy(other2, 0.1f);
            Destroy(other1, 0.1f);
        }
        else if (J2 == "Einstein")
        {
            Destroy(other1, 0.1f);
            Destroy(other3, 0.1f);
        }
    }

    // Update is called once per frame
    void Update () {

        if (Win.text == "Gana "+J2)
        {
            GetComponent<Animator>().SetBool("Ganador", true);
        }

        //Caminar y salto
        if (Input.GetKey(KeyCode.RightArrow))
        {
            IsRight = false;
            if (GetComponent<SpriteRenderer>().flipX == false) ;
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            GetComponent<Animator>().SetBool("Caminar", true);
            transform.Translate(0.07f, 0, 0);

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            IsRight = true;
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

        if (Input.GetKeyUp(KeyCode.M))
        {
            GetComponent<Animator>().SetBool("GolpeEspecial", false);

        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (CanDoSpecial)
            {

                CanDoSpecial = false;

                Invoke("ResetSpecial", 3);

                if (IsRight)
                {
                    if (mana > 0){
                        GetComponent<Animator>().SetBool("GolpeEspecial", true);
                        GameObject go = Instantiate(SpecialAttack, new Vector3(transform.position.x - 5.7f, transform.position.y - 0.3f, 0), Quaternion.identity) as GameObject;
                        mana -= 20.0f;
                        ManaBar.transform.localScale = new Vector2(mana / mn, 1);
                    }
                    
                }
                else
                {
                    if(mana > 0) {
                        GetComponent<Animator>().SetBool("GolpeEspecial", true);
                        GameObject go = Instantiate(SpecialAttack, new Vector3(transform.position.x - 4.0f, transform.position.y - 0.3f, 0), Quaternion.identity) as GameObject;
                        mana -= 20.0f;
                        ManaBar.transform.localScale = new Vector2(mana / mn, 1);
                        go.SendMessage("ChangeSide");
                    }
                    
                }

            }




        }

    }
    public void ResetSpecial()
    {
        CanDoSpecial = true;
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
            HealthBar.transform.localScale = new Vector2(vida / hp, 1);
            GetComponent<Animator>().SetTrigger("Daño");
            Destroy(collision.gameObject);
            Instantiate(Particulas,transform.position,Quaternion.identity);
            HealthBar.transform.localScale = new Vector2(vida / hp, 1);
            if (vida <= 0)
            {
                GetComponent<Animator>().SetBool("Muerto", true);
                Win.text = "Gana Tesla";
                transform.Translate(0, 0, 10f);
            }
        }
    }


    public void ApplyDamageP2(float damage)
    {
        vida -= damage;
        
        if (vida <= 0)
        {
            GetComponent<Animator>().SetBool("Muerto", true);
            Win.text = "Gana Tesla";
            transform.Translate(0, 0, 10f);
        }
        else
        {
        HealthBar.transform.localScale = new Vector2(vida / hp, 1);
        GetComponent<Animator>().SetTrigger("Daño");
        }
    }

   
}
