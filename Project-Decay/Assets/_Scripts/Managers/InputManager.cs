using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    SimpleThirdPerson playerController;
	// Use this for initialization
	void Start () {
        playerController = FindObjectOfType<SimpleThirdPerson>();
	}
	
	// Update is called once per frame
	void Update () {


	}

    // Pauses game and unlocks mouse control
    public void PauseGameplay()
    {
        // If the mouse is enabled at any point then the game should be paused (Subject to change)
        playerController.blockControl = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }

    // Resumes game and locks mouse control
    public void ResumeGameplay()
    {
        // Resume game and freeze mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerController.blockControl = false;
        Time.timeScale = 1f;
    }
}
