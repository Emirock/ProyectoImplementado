using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaBall : MonoBehaviour {

    public int IsRight = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(1.0f, 1.0f,1.0f);
        transform.Translate(Time.deltaTime*5.0f*IsRight,0,0);
	}

    void ChangeSide()
    {
        IsRight = -1;
    }

    

}
