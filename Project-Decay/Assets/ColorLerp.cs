using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour {

    public Color textColor = Color.white;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        textColor = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 0.75f));

        GetComponent<Text>().color = textColor;
	}
}
