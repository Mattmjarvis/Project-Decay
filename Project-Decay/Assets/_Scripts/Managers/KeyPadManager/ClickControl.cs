﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClickControl : MonoBehaviour {

    KeyPadManager keyPadManager;
    KeypadTrigger keypadTrigger;
    GameObject KeyPad;
    GameObject KeyPadConsole;
    public bool codeGiven = false;

    public static string correctCode = "1234";
    public static string playerCode = "";

    public static int totalDigits = 0;

    public Text codeDisplayText;

	// Use this for initialization
	void Start ()
    {
        KeyPad = GameObject.FindGameObjectWithTag("KeyPadDestroy");
        keyPadManager = FindObjectOfType<KeyPadManager>();
        //keypadTrigger = FindObjectOfType<KeypadTrigger>();
        KeyPadConsole = GameObject.FindGameObjectWithTag("KeyPadConsole");

    }
	
	// Update is called once per frame
	void Update ()
    {
        codeDisplayText.text = " " + playerCode;
        //Debug.Log(playerCode);
        if (totalDigits == 4)
        {            
            if (playerCode == correctCode)
            {
                Debug.Log("Correct!");
                codeGiven = true;
                keyPadManager.ExitKeypad();
                Destroy(KeyPad);
                Destroy(KeyPadConsole);
                //keypadTrigger.enabled = false;
            }
            else
            {
                codeDisplayText.text = "";
                playerCode = "";
                totalDigits = 0;
                Debug.Log("Incorrect");
            }
        }
	}

    public void OnMouseDown()
    {
        //Checks the name of the button object and add its to the current code
        playerCode += this.gameObject.name;      
        totalDigits += 1;
    }
}
