using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadTrigger : MonoBehaviour {

    public GameObject KeypadUI;
    InputManager inputManager;


    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {            
            //print("Press E to Access");
            if (Input.GetKeyDown(KeyCode.E))
            {
                inputManager.PauseGameplay();
                KeypadUI.SetActive(true);
            }            
        }
    }   
}
