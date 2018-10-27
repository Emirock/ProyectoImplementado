using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EinsteinBomb : MonoBehaviour {
    public int IsRight = -1;

    // Use this for initialization
    void Update()
    {
        //transform.Rotate(1.0f, 1.0f,1.0f);
        transform.Translate(Time.deltaTime * 10.0f * IsRight, 0, 0);
    }

    void ChangeSide()
    {
        IsRight = 1;
    }

}
