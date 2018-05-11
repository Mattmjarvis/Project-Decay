using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public GameObject pauseMenu;
    InputManager inputManager;

    private bool menuOpen = false;

	// Use this for initialization
	void Start () {
        inputManager = FindObjectOfType<InputManager>(); // Gets the input manager to allow pause and unpausing of the game
	}
	
	// Update is called once per frame
	void Update () {

        // Checks for input whether to pause or resume game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen == false)
            {
                PauseGame();
            }
            else if(menuOpen == true)
            {
                ResumeGame();
            }
        }
	}

    // Pauses the game
    public void PauseGame()
    {
        menuOpen = true;
        inputManager.PauseGameplay(); // Freezes time, shows mouse, and blocks player character input
        pauseMenu.SetActive(true); // Show the pause menu
    }

    public void ResumeGame()
    {
        menuOpen = false;
        pauseMenu.SetActive(false); // Resumes time, hides mouse, and allows player character input
        inputManager.ResumeGameplay();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); // Returns to the main menu
    }

    public void QuitGame()
    {
        Application.Quit(); // Quits the application
    }
}
