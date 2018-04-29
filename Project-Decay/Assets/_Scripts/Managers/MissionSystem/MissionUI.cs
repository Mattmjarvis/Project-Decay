using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{

    // Objects
    public Image HUDMissionImage;
    public Text missionCompleteOverlay;
    public Text HUDMissionObjective;

    public GameObject missionLog;
    MissionButtons missionButtons;
    public Image[] missionLogUI;
    InputManager inputManager;
    MissionManager mm;


    // Variables
    bool missionUIActive = false;
    public bool hasMission = false;
    bool enableHUD = false;

    private void Awake()
    {
        mm = FindObjectOfType<MissionManager>();
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

        // Hides missionHUD image
        HUDMissionImage.fillAmount = 0f;
        foreach (Text text in HUDMissionImage.GetComponentsInChildren<Text>())
        {
            text.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Opens/ Closes the mission UI
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(OpenCloseMissionUI());

            // Will enable/ disable the HUD mission ONLY if the player has a mission 
            if (hasMission)
            {
                StartCoroutine(EnableDisableHUDMission());
            }

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            mm.IncrementMissionObjective();
        }


    }

    // Sets mission log to active and adds fill amounts
    public void OpenMissionLog()
    {
        missionLog.SetActive(true);

        if (mm.currentMission.id == 0)
        {
            mm.IncrementMissionObjective();
        }

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
        }

        // Disables all text element
        foreach (Text text in missionLog.GetComponentsInChildren<Text>())
        {
            text.gameObject.SetActive(false);
        }

        // Disable mission log when no fill
        if (missionLogUI[missionLogUI.Length - 1].fillAmount == 0f)
        {
            missionLog.SetActive(false);
        }
    }


    // Shows or hides the onscreen mission 
    public void ShowHideHUDMission()
    {
        StartCoroutine(EnableDisableHUDMission());
    }

    IEnumerator EnableDisableHUDMission()
    {
        float time = HUDMissionImage.fillAmount;

        // Hides the HUD Mission Image
        if (enableHUD == true)
        {
            // Disables all text components in the HUD mission
            foreach (Text text in HUDMissionImage.GetComponentsInChildren<Text>())
            {
                text.gameObject.SetActive(false);
            }

            // Reduces fill amount of HUD image each update
            while (time > 0f)
            {
                HUDMissionImage.fillAmount -= 0.1f;
                time = HUDMissionImage.fillAmount;
                yield return new WaitForFixedUpdate();
            }
        }

        else if (enableHUD == false)
        {
            while (time < 1f)
            {
                HUDMissionImage.fillAmount += 0.1f;
                time = HUDMissionImage.fillAmount;
                yield return new WaitForFixedUpdate();
            }
        }

        if (time == 1f)
        {
            enableHUD = true;
            foreach (Text text in HUDMissionImage.GetComponentsInChildren<Text>(true))
            {
                text.gameObject.SetActive(true);
            }
            StopCoroutine(EnableDisableHUDMission());
        }

        else if (time == 0f)
        {
            enableHUD = false;
            StopCoroutine(EnableDisableHUDMission());

        }

    }

    // Numerator will open or close mission UI to opposite
    IEnumerator OpenCloseMissionUI()
    {
        missionLogUI = missionLog.GetComponentsInChildren<Image>(true);
        float time = missionLogUI[missionLogUI.Length - 1].fillAmount;

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
                missionButtons.CurrentMissionButton();
                OpenMissionLog();
                time = missionLogUI[missionLogUI.Length - 1].fillAmount;
                yield return new WaitForFixedUpdate();
            }
        }

        // Stops the coroutine if the missionUI is fully closed
        if (time == 0f)
        {
            missionUIActive = false;
            StopCoroutine(OpenCloseMissionUI());
        }

        // Stops the coroutine if the missionUI is fully open & pause game
        else if (time == 1f)
        {
            missionUIActive = true;
            inputManager.PauseGameplay();
            StopCoroutine(OpenCloseMissionUI());

        }

    }


    // Begins the fade for the complete mission overlay
    public void CompleteMissionPopout()
    {
        StartCoroutine(MissionCompletePopout());
    }

    // Shows then hides overlay
    IEnumerator MissionCompletePopout()
    {
        // Fades in overlay
        while (missionCompleteOverlay.color.a < 1f)
        {
            missionCompleteOverlay.color = new Color(missionCompleteOverlay.color.r, missionCompleteOverlay.color.g, missionCompleteOverlay.color.b, missionCompleteOverlay.color.a + Time.deltaTime / 1f);
            yield return null;
            if (missionCompleteOverlay.color.a >= 1)
            {
                break;
            }
        }

        // Wait for seconds until dissapearing
        yield return new WaitForSeconds(3);
        // Hides overlay with fade out
        while (missionCompleteOverlay.color.a > 0f)
        {
            missionCompleteOverlay.color = new Color(missionCompleteOverlay.color.r, missionCompleteOverlay.color.g, missionCompleteOverlay.color.b, missionCompleteOverlay.color.a - Time.deltaTime / 1f);
            yield return null;

            // Stop coroutine
            if (missionCompleteOverlay.color.a <= 0f)
            {
                break;
            }
        }



    }
}



