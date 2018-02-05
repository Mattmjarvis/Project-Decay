﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButtons : MonoBehaviour {

    MissionManager missionManager;
    private ScrollRect scrollView;
    public GameObject scrollbar;

    // Text elements for missions
    private Text currentMissionTitle;
    private Text currentMissionObjective;
    private Text currentMissionBio;
    private Text currentMissionProgress;

    // Floats to keep track of the mission objectives
    private float objectivesCompleted;
    private float objectivesNeeded;
    private float progressTotal;
    private int missionsAdded;
    private int completedMissionsAdded;

    // GameObjects for each window element in the mission log
    public GameObject currentMissionCanvas;
    public GameObject currentMissionBackground;

    public GameObject allMissionCanvas;
    public GameObject[] totalMissions;
    public GameObject completedMissionCanvas;
    public GameObject[] completedMissions;

    // Gameobject ready to instantiate
    public GameObject missionBorder;

    // Use this for initialization
    void Awake ()
    {
        // Get missionmanager component
        missionManager = gameObject.GetComponent<MissionManager>();
        scrollView = FindObjectOfType<ScrollRect>();

        // Finds all the components for the current mission description
        currentMissionTitle = currentMissionCanvas.transform.Find("Mission Title").GetComponent<Text>();
        currentMissionObjective = currentMissionCanvas.transform.Find("Mission Objective").GetComponent<Text>();
        currentMissionBio = currentMissionCanvas.transform.Find("Mission Bio").GetComponent<Text>();
        currentMissionProgress = currentMissionCanvas.transform.Find("Mission Progress").GetComponent<Text>();
    }

    private void Start()
    {
        // Instantly set first mission for player
        CurrentMissionButton();

        // Sets size of the array to the maximum mission count
        totalMissions = new GameObject[missionManager.missionList.Count];
        completedMissions = new GameObject[missionManager.missionList.Count];
    }

    // Displays the players current mission
    public void CurrentMissionButton()
    {
        // Prevents scrolling
        scrollView.vertical = false;
        scrollbar.SetActive(false);

        // Set and disable appropriate canvas
        allMissionCanvas.SetActive(false);
        currentMissionCanvas.SetActive(true);
        completedMissionCanvas.SetActive(false);
        currentMissionBackground.SetActive(true);

    // Calculate percentage completed
    objectivesCompleted = missionManager.currentMission.objectiveCount;
        objectivesNeeded = missionManager.currentMission.totalObjectives;
        progressTotal = (objectivesCompleted / objectivesNeeded) * 100;

        // Sets all the text UI elements for the players current mission
        currentMissionTitle.text = missionManager.currentMission.title;
        currentMissionObjective.text = missionManager.currentMission.objective;
        currentMissionBio.text = missionManager.currentMission.bio;
        currentMissionProgress.text = progressTotal.ToString() + "% Complete";
    }

    // Displays all the missions available
    public void AllMissionsButton()
    {
        // Set and disable appropriate canvas
        allMissionCanvas.SetActive(true);
        currentMissionCanvas.SetActive(false);
        completedMissionCanvas.SetActive(false);
        currentMissionBackground.SetActive(false);

        // Allows user to scroll
        scrollView.vertical = true;
        scrollbar.SetActive(true);

        // Loops through to add missions
        for(int i = 0; i < missionManager.missionList.Count; i++)
        {

            // Stops making new instances if button is pressed more than once.
            if(i < missionsAdded)
            {
                return;
            }

            totalMissions[i] = Instantiate(missionBorder);
            totalMissions[i].transform.SetParent(allMissionCanvas.transform, false);

            // If mission is Unavailable or next change colour to red
            if(missionManager.missionList[i].status == Mission.MissionStatus.UNAVAILABLE || missionManager.missionList[i].status == Mission.MissionStatus.NEXT)
            {
                totalMissions[i].GetComponent<Image>().color = new Color(255, 0, 0, 255);            // Set the text UI to display missions properties.
                totalMissions[i].GetComponentInChildren<Text>().text = "Mission Unknown";
            }
            // If mission is current then change colour to orange
            else if (missionManager.missionList[i].status == Mission.MissionStatus.CURRENT)
            {
                totalMissions[i].GetComponent<Image>().color = new Color(255, 150, 0, 255);
                // Set the text UI to display missions properties.
                totalMissions[i].GetComponentInChildren<Text>().text = missionManager.missionList[i].title + "\n" + missionManager.missionList[i].objective;
            }
            // if mission is complete then change colour to green
            else if (missionManager.missionList[i].status == Mission.MissionStatus.COMPLETE)
            {
                totalMissions[i].GetComponent<Image>().color = new Color(0,  255, 0, 255);
                // Set the text UI to display missions properties.
                totalMissions[i].GetComponentInChildren<Text>().text = missionManager.missionList[i].title + "\n" + missionManager.missionList[i].objective;
            }




            // Increments missions added
            missionsAdded++;

            // Transform position of the missions to fit UI
            if (i > 0)
            {
                //Debug.Log(totalMissions[i].transform.position);
                totalMissions[i].transform.position = new Vector3(totalMissions[i].transform.position.x, totalMissions[i].transform.position.y - i * 80, totalMissions[i ].transform.position.z);
                //Debug.Log(totalMissions[i].transform.position);
            }
        }
    }

    public void CompletedMissionsButton()
    {
        // Set and disable appropriate canvas
        allMissionCanvas.SetActive(false);
        currentMissionCanvas.SetActive(false);
        completedMissionCanvas.SetActive(true);
        currentMissionBackground.SetActive(false);

        // Allows user to scroll
        scrollView.vertical = true;
        scrollbar.SetActive(true);

        // Loops through to add missions
        for (int i = 0; i < missionManager.missionList.Count; i++)
        {
            // Skip listID if mission is not complete
            if (missionManager.missionList[i].status != Mission.MissionStatus.COMPLETE)
            {
                return;
            }

            // Stops making new instances if button is pressed more than once.
            if (i < completedMissionsAdded)
            {
                return;
            }

            completedMissions[i] = Instantiate(missionBorder);
            completedMissions[i].transform.SetParent(completedMissionCanvas.transform, false);

            // Set mission colour to green
            completedMissions[i].GetComponent<Image>().color = new Color(0, 255, 0, 255);



            // Set the text UI to display missions properties.
            completedMissions[i].GetComponentInChildren<Text>().text = missionManager.missionList[i].title + "\n" + missionManager.missionList[i].objective;

            // Increments missions added
            completedMissionsAdded++;

            // Transform position of the missions to fit UI
            if (i > 0)
            {
                completedMissions[i].transform.position = new Vector3(completedMissions[i].transform.position.x, completedMissions[i].transform.position.y - i * 40, completedMissions[i].transform.position.z);
            }
        }

    }

}
