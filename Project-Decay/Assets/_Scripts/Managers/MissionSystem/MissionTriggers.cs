using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTriggers : MonoBehaviour
{
    // Components
    MissionCompletionInfo MCI;

    // Which mission is affected
    public int missionBeingTriggered;

    // Use this for initialization
    void Start()
    {
        MCI = FindObjectOfType<MissionCompletionInfo>(); // Gets mission completion info
    }

    // Complete a mission objective if player activates trigger
    private void OnTriggerEnter(Collider other)
    {

        #region Complete mission  2; 
        if (other.tag == "Player" && missionBeingTriggered == 1)
        {
            MCI.boatInvestigated = true;
            MCI.MissionCompletionCheck();
        }
        #endregion

        #region Complete Mission 5
        if (other.tag == "Player" && missionBeingTriggered == 4)
        {
            MCI.forestEntered = true;
            MCI.MissionCompletionCheck();
        }
        #endregion

        #region Complete Mission 6
        if (other.tag == "Player" && missionBeingTriggered == 5)
        {
            MCI.forestExit = true;
            MCI.MissionCompletionCheck();
        }
        #endregion

        #region Complete Mission 8
        if (other.tag == "Player" && missionBeingTriggered == 7)
        {
            MCI.gateFound = true;
            MCI.MissionCompletionCheck();
        }
        #endregion

        #region Complete Mission 9
        if (other.tag == "Player" && missionBeingTriggered == 8)
        {
            MCI.pathFound = true;
            MCI.MissionCompletionCheck();
        }
        #endregion

        #region Complete Mission 10
        if (other.tag == "Player" && missionBeingTriggered == 9)
        {
            MCI.forestBaseReached = true;
            MCI.MissionCompletionCheck();
        }
        #endregion

        #region Complete Mission 12
        if (other.tag == "Player" && missionBeingTriggered == 11)
        {
            MCI.codeFound = true;
            MCI.MissionCompletionCheck();
        }
        #endregion

        #region Complete Mission 14
        if (other.tag == "Player" && missionBeingTriggered == 13)
        {
            MCI.bunkerInvestigated = true;
            MCI.MissionCompletionCheck();
        }
        #endregion

        #region Complete Mission 17
        if (other.tag == "Player" && missionBeingTriggered == 16)
        {
            MCI.officeBulidingReached = true;
            MCI.MissionCompletionCheck();
        }
        #endregion
    }
}