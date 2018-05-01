using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClickControl : MonoBehaviour {

    //Script variables
    MissionCompletionInfo MCI;

    GateAccessHUD codeHUD;
    KeyPadManager keyPadManager;
    KeypadTrigger keypadTrigger;
    GameObject KeyPad;
    public GameObject KeyPadConsole;
    BoxCollider ConsoleCollider;
    public GameObject GateDoor;
    public bool codeGiven = false;

    //Correct code
    public static string correctCode = "7355608";
    public static string playerCode = "";
    public bool HUDHidden = false;
    public static int totalDigits = 0;

    public Text codeDisplayText;

	// Use this for initialization
	void Start ()
    {
        MCI = FindObjectOfType<MissionCompletionInfo>();
        codeHUD = FindObjectOfType<GateAccessHUD>();
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
        if (totalDigits == 7)
        {            
            if (playerCode == correctCode)
            {
                Debug.Log("Correct!");
                codeGiven = true;
                keyPadManager.ExitKeypad();
                    
                // Stops from reenabling HUD
                if (HUDHidden == false)
                {
                    HUDHidden = true;
                    codeHUD.ShowHideCodeHUD();
                }

                // Complete the mission
                MCI.gateOpened = true;
                MCI.MissionCompletionCheck();

                Destroy(KeyPad);
                ConsoleCollider = KeyPadConsole.GetComponent<BoxCollider>();
                ConsoleCollider.enabled = false;
                //keypadTrigger.enabled = false;
                Destroy(GateDoor);
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
