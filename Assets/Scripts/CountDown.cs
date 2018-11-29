    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class CountDown : MonoBehaviour
{
    public int timeLeft = 60; //Seconds Overall
    public Text countdown; //UI Text Object
    public string cero;
    void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }
    void Update()
    {
        countdown.text = (cero + timeLeft); //Showing the Score on the Canvas
    }
    //Simple Coroutine
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (timeLeft < 11)
            {
                cero = "0";
            }
            else {
                cero = "";
            }
            if (timeLeft>0)
            {
                timeLeft--;
            }
            else
            {
                // Si se acaba el tiempo gana quien tenga mas vida
            }
            
        }
    }

}   


