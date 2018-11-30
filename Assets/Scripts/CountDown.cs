    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class CountDown : MonoBehaviour
{
    public int timeLeft = 60, contador; //Seconds Overall
    public Text countdown; //UI Text Object
    public Text Win, t1, t2;
    public string cero;
    void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }
    void Update()
    {
        

        if (Win.text != "Gana Einstein" || Win.text != "Gana Tesla")
        {
            countdown.text = (cero + timeLeft); //Showing the Score on the Canvas
        }
        else
        {
            string cont = contador.ToString();
            countdown.text = (cont);
        }

    }
    //Simple Coroutine
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Win.text != "Gana Einstein" || Win.text != "Gana Tesla") {
                if (timeLeft < 11)
                {
                    cero = "0";
                }
                else {
                    cero = "";
                }
                if (timeLeft > 0)
                {
                    timeLeft--;
                    contador = timeLeft;
                }
                else if (timeLeft == 0)
                {
                    // Si se acaba el tiempo gana quien tenga mas vida
                    //float j1 = float.Parse(t1);
                    

                    
                }
            }
           
        }
    }

}   


