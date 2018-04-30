using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCompletionInfo : MonoBehaviour
{

    // Get mission manager
    MissionManager MM;

    // Weapon check
    public bool hasShotgun;
    public bool hasAR;

    /// <summary>
    /// Mission Precompletion checks. This is incase the mission requirements have been completed before the mission has been reached.
    /// </summary>
    /// 

    #region Mission 2 Requirement - Go to boat
    public bool boatInvestigated = false;
    #endregion

    #region Mission 3 Requirement - Aquire Pistol
    public bool hasPistol = false;
    #endregion

    #region Mission 4 Requirement - Kill enemy by boat
    public bool startEnemyisDead = false;
    #endregion

    #region Mission 5 Requirement - Enter Forest
    public bool forestEntered = false;
    #endregion

    #region Mission 6 Requirement - Exit the forest
    public bool forestExit = false;
    #endregion

    #region Mission 7 Requirement - Kill group of 4 mutants
    public bool startKilling = false;
    public int killCount = 0;
    #endregion

    #region Mission 8 Requirement - Find the gate to the compound
    public bool gateFound = false;
    #endregion

    #region Mission 9 Requirement - Find the path to the bunker
    public bool pathFound = false;
    #endregion
    // Use this for initialization
    void Awake()
    {
        MM = FindObjectOfType<MissionManager>();
    }

    // This method checks if any completion has been met before the mission has been activated.
    public void MissionCompletionCheck()
    {
        #region Mission2 Check
        if (MM.currentMission.id == 1)
        {
            if (boatInvestigated == true)
            {
                MM.IncrementMissionObjective();
            }
        }
        #endregion

        #region Mission3 Check
        if (MM.currentMission.id == 2)
        {
            if (hasPistol == true)
            {
                MM.IncrementMissionObjective();
            }
        }
        #endregion

        #region Mission4 Check
        if (MM.currentMission.id == 3)
        {
            if (startEnemyisDead == true)
            {
                MM.IncrementMissionObjective();
            }
        }
        #endregion

        #region Mission5 Check
        if (MM.currentMission.id == 4)
        {
            if (forestEntered == true)
            {
                MM.IncrementMissionObjective();
            }
        }
        #endregion

        #region Mission 6 Check
        if (MM.currentMission.id == 5)
        {
            if (forestExit == true)
            {
                MM.IncrementMissionObjective();
            }
        }
        #endregion

        #region Mission7 Check
        if (MM.currentMission.id == 6)
        {
            if (killCount < 5 && startKilling == true)
            {
                MM.IncrementMissionObjective();
            }
        }
        #endregion

        #region Mission 8 Check
        if (MM.currentMission.id == 7)
        {
            if (gateFound == true)
            {
                MM.IncrementMissionObjective();
            }
        }
        #endregion

        #region Mission 9 Check
        if (MM.currentMission.id == 8)
        {
            if (pathFound == true)
            {
                MM.IncrementMissionObjective();
            }
        }
        #endregion
    }

}
