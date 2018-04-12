using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadManager : MonoBehaviour {

    KeypadTrigger keyPadTrigger;
    InputManager inputManager;    

    private void Start()
    {
        keyPadTrigger = FindObjectOfType<KeypadTrigger>();
        inputManager = FindObjectOfType<InputManager>();        
    }

    public void ExitKeypad()
    {
        keyPadTrigger.KeypadUI.SetActive(false);
        inputManager.ResumeGameplay();
    }
}
