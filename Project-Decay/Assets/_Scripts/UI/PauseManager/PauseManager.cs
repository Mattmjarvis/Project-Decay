using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public GameObject pauseMenu;
    InputManager inputManager;
    UIManager UIM;

    public bool pauseMenuOpen = false;

	// Use this for initialization
	void Start () {
        inputManager = FindObjectOfType<InputManager>(); // Gets the input manager to allow pause and unpausing of the game
        UIM = FindObjectOfType<UIManager>(); // Get UI manager to change behaviour based on variables
	}
	
	// Update is called once per frame
	void Update () {

        // Checks for input whether to pause or resume game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuOpen == false)
            {
                PauseGame();
            }
            else if(pauseMenuOpen == true)
            {
                    ResumeGame();
            }
        }
	}

    // Pauses the game
    public void PauseGame()
    {
        pauseMenuOpen = true;
        inputManager.PauseGameplay(); // Freezes time, shows mouse, and blocks player character input
        pauseMenu.SetActive(true); // Show the pause menu
    }

    // Resumes the game
    public void ResumeGame()
    {
        pauseMenuOpen = false;
        pauseMenu.SetActive(false); // Resumes time, hides mouse, and allows player character input

        // If the upgrade menu is open then keep game paused
        if (UIM.upgradeInterfaceOpen == true)
        {
            return;
        }
        else
        {
            inputManager.ResumeGameplay();
        }

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
