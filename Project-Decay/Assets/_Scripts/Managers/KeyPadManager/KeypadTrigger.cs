using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadTrigger : MonoBehaviour {

    public GameObject KeypadUI;
    InputManager inputManager;
    UIManager uIManager;
    public GameObject KeyPadPrompt;


    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        inputManager = FindObjectOfType<InputManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            uIManager.enableInteractTip();
            //KeyPadPrompt.SetActive(true);
            //print("Press E to Access");
            if (Input.GetKeyDown(KeyCode.E))
            {
                inputManager.PauseGameplay();
                KeypadUI.SetActive(true);
                uIManager.disableInteractTip();
            }
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            uIManager.disableInteractTip();
            
        }
    }
}
