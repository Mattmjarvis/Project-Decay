using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteract : MonoBehaviour {

    public GameObject interactPrompt;
    public GameObject noteDisplay;

    InputManager inputManager;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(gameObject.tag == "Player")
        {
            interactPrompt.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                noteDisplay.SetActive(true);
                inputManager.PauseGameplay();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "Player")
        {
            interactPrompt.SetActive(false);

            
        }
    }
}
