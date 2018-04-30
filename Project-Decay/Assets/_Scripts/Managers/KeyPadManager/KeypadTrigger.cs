using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadTrigger : MonoBehaviour {

    public GameObject KeypadUI;
    InputManager inputManager;
    public GameObject KeyPadPrompt;


    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            KeyPadPrompt.SetActive(true);
            //print("Press E to Access");
            if (Input.GetKeyDown(KeyCode.E))
            {
                inputManager.PauseGameplay();
                KeypadUI.SetActive(true);
                KeyPadPrompt.SetActive(false);
            }
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            KeyPadPrompt.SetActive(false);
        }
    }
}
