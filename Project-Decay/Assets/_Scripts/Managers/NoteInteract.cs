using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteract : MonoBehaviour
{

    public GameObject interactPrompt;
    public GameObject noteDisplay;

    InputManager inputManager;
    GateAccessHUD codeHUD;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        codeHUD = FindObjectOfType<GateAccessHUD>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            noteDisplay.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            noteDisplay.SetActive(false);
            codeHUD.ShowHideCodeHUD();
        }
    }
}

