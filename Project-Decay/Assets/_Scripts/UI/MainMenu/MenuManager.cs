using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour { 

    // Objects
    public GameObject menuButtons;
    public GameObject controlOverlay;

    //Setting
    bool controlsOpen = false;
    bool canClick = true;

    public void Start()
    {
        // moves buttons onto screen at start
        StartCoroutine(MoveButtons());
    }

    // Begins the game
    public void newGame()
    {
        SceneManager.LoadScene(1);
    }

    // Opens the control window panel
    public void ControlMenu()
    {
        // Stops user from pressing button (While mid transition)
        if (!canClick)
        {
            return;
        }

        StartCoroutine(ShowControls());
    }

    // Exits game
    public void Exit()
    {
        Application.Quit();
    }

    // Transitions the menu buttons onto the screen
    public IEnumerator MoveButtons()
    {
        // Infinite loop
        for(; ;)
        {
            // Stop coroutine when buttons reach correct place
            if(menuButtons.transform.localPosition.x >= 0)
            {
                yield break;
            }
            // Move buttons position
            else
            {
                menuButtons.transform.localPosition = new Vector3(menuButtons.transform.localPosition.x + 20f, menuButtons.transform.localPosition.y, menuButtons.transform.localPosition.z);
            }

            yield return new WaitForSeconds(0.005f);
        }
    }

    // Transitions control menu on/ off screen
    public IEnumerator ShowControls()
    {
        canClick = false;
        // Check if menu is already open
        if(controlsOpen == false)
        {
            controlsOpen = true; // Set to open
            for (; ; ) // Infinite loop
            {
                // Stop coroutine if overlay reaches position 0
                if (controlOverlay.transform.localPosition.x <= 0)
                {
                    canClick = true;// User can now press button again
                    yield break;
                }
                // Transitions the overlay across screen
                else
                {
                    controlOverlay.transform.localPosition = new Vector3(controlOverlay.transform.localPosition.x - 50f, controlOverlay.transform.localPosition.y, controlOverlay.transform.localPosition.z);
                }
                yield return new WaitForSeconds(0.005f);
            }
        }

        // Check if menu is already open
        else if(controlsOpen == true)
        {
            // Set opened to false
            controlsOpen = false;
            for (; ; ) // Infinite Loop
            {
                // If overlay reachess position 1400 then stop coroutine
                if (controlOverlay.transform.localPosition.x >=1400)
                {
                    canClick = true; // User can now press button again
                    yield break;
                }

                // Move overlay across screen
                else
                {
                    controlOverlay.transform.localPosition = new Vector3(controlOverlay.transform.localPosition.x + 50f, controlOverlay.transform.localPosition.y, controlOverlay.transform.localPosition.z);
                }

                yield return new WaitForSeconds(0.005f);
            }
        }

    }
}
