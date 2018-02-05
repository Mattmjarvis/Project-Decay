using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour {

    // Objects
    public GameObject missionLog;
    MissionButtons missionButtons;
    public Image[] missionLogUI;
    InputManager inputManager;

    // Variables
    bool missionUIActive = false;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        // Gets all images components from the missionlogUI
        missionLogUI = missionLog.GetComponentsInChildren<Image>();
        missionButtons = FindObjectOfType<MissionButtons>();

        // Hides missionLOGUI 
        foreach (Image image in missionLogUI)
        {
            image.fillAmount = 0f;
        }
        missionLog.gameObject.SetActive(false);
              
    }

    // Update is called once per frame
    void Update () {

        // Opens/ Closes the mission UI
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(OpenCloseMissionUI());
        }
        
	}

    // Sets mission log to active and adds fill amounts
    public void OpenMissionLog()
    {
        missionLog.SetActive(true);
        missionButtons.CurrentMissionButton();
        // Increase fill value of each array element.
        for (int i = 0; i < missionLogUI.Length; i++)
        {
            missionLogUI[i].fillAmount += 0.1f;

            // Enables all text elements when UI is to the full fill amount
            if (missionLogUI[i].fillAmount == 1f)
            {
                foreach (Text text in missionLog.GetComponentsInChildren<Text>(true))
                {
                    text.gameObject.SetActive(true);
                }
            }
        }
    }

    // Reduces fill amount then sets fill amount to false.
    public void CloseMissionLog()
    {
        // Resumes the gameplay as normal
        inputManager.ResumeGameplay(); 

        // Decrease fill value of each array element.
        for (int i = 0; i < missionLogUI.Length; i++)
        {
            missionLogUI[i].fillAmount -= 0.1f;

            // Disables all text element
            foreach(Text text in missionLog.GetComponentsInChildren<Text>())
            {
                text.gameObject.SetActive(false);
            }
        }

        // Disable mission log when no fill
        if (missionLogUI[missionLogUI.Length - 1].fillAmount == 0f)
        {
            missionLog.SetActive(false);
        }
    }

    // Numerator will open or close mission UI to opposite
    IEnumerator OpenCloseMissionUI()
    {
        missionLogUI = missionLog.GetComponentsInChildren<Image>();
        float time = missionLogUI[missionLogUI.Length- 1].fillAmount;

        // Call the mission log to fully close. Decrease fill amount each update.
        if (missionUIActive == true)
        {
            while (time > 0f)
            {
                CloseMissionLog();
                time = missionLogUI[missionLogUI.Length - 1].fillAmount;
                yield return new WaitForFixedUpdate();
            }
        }

        // Call the mission log to fully open. Increase fill amount each update.
        else
        {
            while (time != 1f)
            {
                OpenMissionLog();
                time = missionLogUI[missionLogUI.Length - 1].fillAmount;
                yield return new WaitForFixedUpdate();
            }
        }

        // Stops the coroutine if the missionUI is fully closed
        if(time == 0f)
        {
            missionUIActive = false;
            StopCoroutine(OpenCloseMissionUI());                
        }

        // Stops the coroutine if the missionUI is fully open & pause game
        else if(time == 1f)
        {
            missionUIActive = true;
            inputManager.PauseGameplay();
            StopCoroutine(OpenCloseMissionUI());

        }

    }

}

    

