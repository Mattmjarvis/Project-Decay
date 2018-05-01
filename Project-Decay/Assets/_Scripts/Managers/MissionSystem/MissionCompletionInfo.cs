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
    public bool M7startKilling = false;
    public int M7killCount = 0;
    public int M7amountAdded = 0;
    #endregion

    #region Mission 8 Requirement - Find the gate to the compound
    public bool gateFound = false;
    #endregion

    #region Mission 9 Requirement - Find the path to the bunker
    public bool pathFound = false;
    #endregion

    #region Mission 10 Requirement - Reach the forest base
    public bool forestBaseReached = false;
    #endregion

    #region Mission 11 Requirement - Kill 4 mutants in forest base
    public bool M11startKilling = false;
    public int M11killCount = 0;
    public int M11amountAdded = 0;
    #endregion

    #region Mission 12 Requirement - Find access code
    public bool codeFound = false;
    #endregion

    #region Mission 13 Requirement - Open the gate to the compound
    public bool gateOpened = false;
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
            if (M7killCount > 0)
            {
                for (int i = M7amountAdded; i < M7killCount; i++)
                {
                    M7amountAdded++;
                    MM.IncrementMissionObjective();
                }
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

        #region Mission 10 Check
        if (MM.currentMission.id == 9)
        {
            if (forestBaseReached == true)
            {
                MM.IncrementMissionObjective();
            }
        }
        #endregion

        #region Mission11 Check
        if (MM.currentMission.id == 10)
        {
            if (M11killCount > 0)
            {
                for (int i = M11amountAdded; i < M11killCount; i++)
                {
                    M11amountAdded++;
                    MM.IncrementMissionObjective();
                }
            }

        }
        #endregion

        #region Mission12 Check
        if (MM.currentMission.id == 11)
        {
            if (codeFound == true)
            {
                MM.IncrementMissionObjective();
            }
        }
        #endregion

        #region Mission13 Check
        if (MM.currentMission.id == 12)
        {
            if (gateOpened == true)
            {
                MM.IncrementMissionObjective();
            }
        }
        #endregion
    }

}
