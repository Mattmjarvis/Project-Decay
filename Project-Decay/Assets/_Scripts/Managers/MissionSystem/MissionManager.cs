﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionManager : MonoBehaviour {

    public static MissionManager missionManager;

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
    }

    // Get a mission
    public void GetNewMission(int MissionID)
    {
        // Sets next mission to the current mission
            if (nextMission.id == MissionID && nextMission.status == Mission.MissionStatus.NEXT)
            {
                currentMission = nextMission; // Set mission to current
                currentMission.status = Mission.MissionStatus.CURRENT; // Set quest to current

                // Ready up next quest if one is available
                if (currentMission.id < missionList.Count)
                {
                    nextMission = missionList[currentMission.id +1];
                    nextMission.status = Mission.MissionStatus.NEXT; 
                }
            }      
    }

    // Complete current  Mission
    public void CompleteMission()
    {
        currentMission.status = Mission.MissionStatus.COMPLETE; // Sets current mission to complete
        currentMission.missionsCompleted += 1;
        currentMission = null; // Clears current mission 
        //GetNewMission(nextMission.id); // Assigns new mission when previous is complete (Comment back in if we want this, else we can have a delay until next mission)

    }

    //  Check mission objective
    public void SetMissionObjective(string missionObjective)
    {
        // Makes sure quest is current and set mission objective
        if (currentMission.objective == missionObjective && currentMission.status == Mission.MissionStatus.CURRENT)
        {
            currentMission.objectiveCount += 1; // Count each finished objective

            // If all objectives are finished then complete mission
            if (currentMission.objectiveCount == currentMission.totalObjectives)
            { 
                CompleteMission(); // Mission completed
            }
        }
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
