using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MoverJ1 : MonoBehaviour
{
    public int timeLeft = 100, contador; //Seconds Overall
    public float vida = 100f, hp = 100f, mana = 100f, mn = 100f;
    public string TagEnemigo = "Enemigo", vid;
    public bool Attack = false;
    public GameObject Enemigo;
    public GameObject SpecialAttack;
    public GameObject Particulas;
    public Text Win;
    public Text vidaT1;
    public Image HealthBar, ManaBar;
    private bool IsRight = true;
    private bool CanDoSpecial = true;

    // Use this for initialization
    void Start()
    {
        hp = vida;
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            if (timeLeft > 0)
            {
                timeLeft--;

            }
            else if (timeLeft == 0)
            {
                SceneManager.LoadScene("MainMenu");
            }



        }
    }

    void Update()
    {
        vid = string.Format("{0:G}", vida);
        vidaT1.text = (vid);
        if (Win.text == "Gana Tesla")
        {
            GetComponent<Animator>().SetBool("Ganador", true);
            StartCoroutine("LoseTime");
            Time.timeScale = 1; //Just making sure that the timeScale is right
        }
        //Caminar y salto
        if (Input.GetKey(KeyCode.D))
        {

            IsRight = true;

            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            GetComponent<Animator>().SetBool("Caminar", true);
            transform.Translate(0.07f, 0, 0);

        }
        if (Input.GetKey(KeyCode.A))
        {
            IsRight = false;

            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            GetComponent<Animator>().SetBool("Caminar", true);
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
            if (CanDoSpecial)
            {

                CanDoSpecial = false;

                Invoke("ResetSpecial", 3);

                if (IsRight)
                {
                    if (mana > 0)
                    {
                        GetComponent<Animator>().SetBool("GolpeEspecial", true);
                        GameObject go = Instantiate(SpecialAttack, new Vector3(transform.position.x + 1.5f, transform.position.y + 0.5f, 6.4f), Quaternion.identity) as GameObject;
                        mana -= 20.0f;
                        ManaBar.transform.localScale = new Vector2(mana / mn, 1);
                    }
                }
                else
                {
                    if (mana > 0)
                    {
                        GetComponent<Animator>().SetBool("GolpeEspecial", true);
                        GameObject go = Instantiate(SpecialAttack, new Vector3(transform.position.x + 2.0f, transform.position.y + 1.5f, 6.4f), Quaternion.identity) as GameObject;
                        go.SendMessage("ChangeSide");
                        mana -= 20.0f;
                        ManaBar.transform.localScale = new Vector2(mana / mn, 1);
                    }
                }
            }
        }
        //*************************************************************************

        //Desactivar animaciones
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("Caminar", false);
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

        if (Input.GetKeyUp(KeyCode.G))
        {
            GetComponent<Animator>().SetBool("GolpeEspecial", false);
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
            if (vida <= 0)
            {
                GetComponent<Animator>().SetBool("Muerto", true);
                // Hacer que muestren unk
                Win.text = "Gana Einstein";
                transform.Translate(0, 0, 10f);
            }
        }
    }


    public void ApplyDamageP1(float damage)
    {
        vida -= damage;


        if (vida <= 0)
        {
            GetComponent<Animator>().SetBool("Muerto", true);
            // Hacer que muestren unk
            Win.text = "Gana Einstein";
            transform.Translate(0, 0, 10f);
        }
        else
        {
            HealthBar.transform.localScale = new Vector2(vida / hp, 1);
            GetComponent<Animator>().SetTrigger("Daño");
        }

    }


}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MoverJ1 : MonoBehaviour {
    public float vida = 100f, hp=100f, mana = 100f, mn = 100f;
    public string TagEnemigo = "Enemigo", J1, J2;
    public bool Attack = false;
    public GameObject SpecialAttack;
    public GameObject Particulas, other1, other2, other3;
    public Text Win;
    public Image HealthBar, ManaBar;
    private bool IsRight = true;
    private bool CanDoSpecial = true;

    public MoverJ1()
    {
    } 

    public void setJ1(string J1)
    {
        this.J1 = J1;
    }

    public void setJ2(string J2)
    {
        this.J2 = J2;
    }

    // Use this for initialization
    void Start () {

        DeactivateChildren(other2, false);
        DeactivateChildren(other3, false);
        if (J1 == "Tesla")
        {
            DeactivateChildren(other2, false);
            DeactivateChildren(other3, false);

        }
        else if (J1 == "Marie")
        {
            DeactivateChildren(other2, false);
            DeactivateChildren(other1, false);
        }
        else if (J1 == "Einstein")
        {
            DeactivateChildren(other1, false);
            DeactivateChildren(other3, false);
        }

    }

    void DeactivateChildren(GameObject g, bool a)
    {
        g.SetActive(a);

    }

    void Update()
    {
        if (Win.text == "Gana "+J1)
        {
            GetComponent<Animator>().SetBool("Ganador", true);
        }
        //Caminar y salto
        if (Input.GetKey(KeyCode.D))
        {

            IsRight = true;

            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            GetComponent<Animator>().SetBool("Caminar", true);
            transform.Translate(0.07f, 0, 0);

        }
        if (Input.GetKey(KeyCode.A))
        {
            IsRight = false;

            if (GetComponent<SpriteRenderer>().flipX == false) 
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            GetComponent<Animator>().SetBool("Caminar", true);
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
                    if (mana > 0)
                    {
                        GetComponent<Animator>().SetBool("GolpeEspecial", true);
                        GameObject go = Instantiate(SpecialAttack, new Vector3(transform.position.x + 2.7f, transform.position.y + 1.8f, 6.4f), Quaternion.identity) as GameObject;
                        mana -= 20.0f;
                        ManaBar.transform.localScale = new Vector2(mana / mn, 1);
                    }
                }
                else
                {
                    if (mana > 0)
                    {
                        GetComponent<Animator>().SetBool("GolpeEspecial", true);
                        GameObject go = Instantiate(SpecialAttack, new Vector3(transform.position.x + 2.0f, transform.position.y + 1.8f, 6.4f), Quaternion.identity) as GameObject;
                        go.SendMessage("ChangeSide");
                        mana -= 20.0f;
                        ManaBar.transform.localScale = new Vector2(mana / mn, 1);
                    }
                }
            }
        }
        
        //*************************************************************************

        //Desactivar animaciones
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("Caminar", false);
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
        
       if (Input.GetKeyUp(KeyCode.G))
        {
            GetComponent<Animator>().SetBool("GolpeEspecial", false);
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

        } //&& J2 == "Einstein" && collision.tag.Equals(TagEnemigo)) || (collision.tag.Equals("SpecialMarie") && J2 == "Marie" && collision.tag.Equals(TagEnemigo)) || (collision.tag.Equals("TeslaBall") && J2 == "Tesla" && collision.tag.Equals(TagEnemigo))
        else if ((collision.tag.Equals("SpecialEinstein"))){
            vida -= 15.0f;
            HealthBar.transform.localScale = new Vector2(vida / hp, 1);
            GetComponent<Animator>().SetTrigger("Daño");
            Destroy(collision.gameObject);
            Instantiate(Particulas, transform.position, Quaternion.identity);
            if (vida <= 0)
            {
                GetComponent<Animator>().SetBool("Muerto", true);
                // Hacer que muestren unk
                Win.text = "Gana "+J2;
                transform.Translate(0, 0, 10f);
            }
        }
    }


    public void ApplyDamageP1(float damage)
    {
        vida -= damage;
        
        
        if (vida <= 0)
        {
            GetComponent<Animator>().SetBool("Muerto", true);
            // Hacer que muestren unk
            Win.text ="Gana "+J2;
            transform.Translate(0, 0, 10f);
        }
        else
        {
        HealthBar.transform.localScale = new Vector2(vida/hp,1);
        GetComponent<Animator>().SetTrigger("Daño");
        }
        
    }

}
*/
