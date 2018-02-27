﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionManager : MonoBehaviour {

    public static MissionManager missionManager;
    public MissionUI missionUI;

    public List<Mission> missionList = new List<Mission>();     // Contains a list of the missions
    public Mission currentMission = new Mission();
    public Mission nextMission = new Mission(); // The next mission

 // Make sure that the mission manager is not null and creates the mission manager.
    void Awake()
    {
        if (missionManager == null)
        {
            missionManager = this;
        }
        else if (missionManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        missionUI = FindObjectOfType<MissionUI>(); // Finds the mission UI object
        // Sets the start mission and next mission
        SetStartMission();
    }

    // Get a mission
    public void GetNewMission()
    { 
        // Exits if there are no more missions
        if(nextMission == null)
        {
            return;
        }

        currentMission = nextMission; // Set mission to current
        currentMission.status = Mission.MissionStatus.CURRENT; // Set mission to current
        missionList[currentMission.id].status = Mission.MissionStatus.CURRENT;

        missionUI.HUDMissionObjective.text = currentMission.objective;
        missionUI.ShowHideHUDMission(); // Enable the mission image on the HUD

        // Ready up next quest if one is available
        if (currentMission.id < missionList.Count - 1)
        {
            nextMission = missionList[currentMission.id + 1];
            missionList[currentMission.id + 1].status = Mission.MissionStatus.NEXT;
            nextMission.status = Mission.MissionStatus.NEXT;
        }
    }

    // Complete current  Mission
    public void CompleteMission()
    {
        currentMission.status = Mission.MissionStatus.COMPLETE; // Sets current mission to complete
        missionList[currentMission.id].status = Mission.MissionStatus.COMPLETE;
        currentMission.missionsCompleted += 1;
        missionUI.ShowHideHUDMission(); // Disable the mission image on the HUD

        Debug.Log(currentMission.id + "ml.count: " + missionList.Count);
        if(currentMission.id == missionList.Count - 1)
        {
            nextMission = null;
        }

        currentMission = null; // Clears current mission 

        if (nextMission != null)
        {
            GetNewMission(); // Assigns new mission when previous is complete (Comment back in if we want this, else we can have a delay until next mission)
        }


    }

    //  Check mission objective
    public void IncrementMissionObjective()
    {
        currentMission.objectiveCount++;

        // Makes sure quest is current and set mission objective
        if (currentMission.status == Mission.MissionStatus.CURRENT)
        {
            currentMission.objectiveCount += 1; // Count each finished objective

            // If all objectives are finished then complete mission
            if (currentMission.objectiveCount == currentMission.totalObjectives)
            { 
                CompleteMission(); // Mission completed
            }
        }
    }

    // Sets the start mission to the first mission in the mission list. Sets next mission to mission after
    public void SetStartMission()
    {
        currentMission = missionList[0];
        nextMission = missionList[currentMission.id + 1];
        missionUI.HUDMissionObjective.text = currentMission.objective;
    }

    // When getting a new mission check for completion of previous mission
        public bool CheckForCompletedMission(int missionID)
        {
            for (int i = 0; i < missionList.Count; i++)
            {
                if (missionList[i].id == missionID && missionList[i].status == Mission.MissionStatus.COMPLETE)
                {
                    return true;
                }
            }
            return false;
        }
    
}
