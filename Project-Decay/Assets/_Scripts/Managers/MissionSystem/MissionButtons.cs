﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButtons : MonoBehaviour {

    MissionManager missionManager;

    // Elements for scrolling
    private ScrollRect scrollView;
    public GameObject scrollbar;

    // Canvas title text
    public Text canvasTitle;

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
    public GameObject[] allMissions;
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
        allMissions = new GameObject[missionManager.missionList.Count];
        completedMissions = new GameObject[missionManager.missionList.Count];
    }

    // Displays the players current mission
    public void CurrentMissionButton()
    {
        // Set title
        canvasTitle.text = "Current Mission";

        // Prevents scrolling
        scrollView.vertical = false;
        scrollbar.SetActive(false);
        scrollView.verticalNormalizedPosition = 1f;

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
        currentMissionProgress.text = progressTotal.ToString("F2") + "% Complete";
    }

    // Displays all the missions available
    public void AllMissionsButton()
    {
        // Set title
        canvasTitle.text = "All Missions";

        // Set and disable appropriate canvas
        allMissionCanvas.SetActive(true);
        currentMissionCanvas.SetActive(false);
        completedMissionCanvas.SetActive(false);
        currentMissionBackground.SetActive(false);

        // Allows user to scroll
        scrollView.vertical = true;
        scrollbar.SetActive(true);
        scrollView.verticalNormalizedPosition = 1f;

        // Loops through to add missions
        for (int i = 0; i < missionManager.missionList.Count; i++)
        {

            // Stops making new instances if button is pressed more than once.
            if(i < missionsAdded)
            {
                return;
            }

            allMissions[i] = Instantiate(missionBorder);
            allMissions[i].transform.SetParent(allMissionCanvas.transform, false);

            // If mission is Unavailable or next change colour to red
            if (missionManager.missionList[i].status == Mission.MissionStatus.UNAVAILABLE || missionManager.missionList[i].status == Mission.MissionStatus.NEXT)
            {

                allMissions[i].gameObject.GetComponent<Image>().color = Color.red;
                allMissions[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = "Mission Unavailable";
                allMissions[i].gameObject.transform.GetChild(1).GetComponent<Text>().text = "";
                allMissions[i].gameObject.transform.GetChild(2).GetComponent<Text>().text = "Status: Unavailable";

                allMissions[i].gameObject.transform.GetChild(0).GetComponent<Outline>().effectColor = Color.red;
                allMissions[i].gameObject.transform.GetChild(1).GetComponent<Outline>().effectColor = Color.red;
                allMissions[i].gameObject.transform.GetChild(2).GetComponent<Outline>().effectColor = Color.red;
            }

            else if (missionManager.missionList[i].status == Mission.MissionStatus.CURRENT)
            {
                allMissions[i].GetComponent<Image>().color = new Color(255, 150, 0, 255);
                // Set the text UI to display missions properties.

                allMissions[i].gameObject.GetComponent<Image>().color = Color.black;
                allMissions[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = missionManager.currentMission.title;
                allMissions[i].gameObject.transform.GetChild(1).GetComponent<Text>().text = missionManager.currentMission.objective;
                allMissions[i].gameObject.transform.GetChild(2).GetComponent<Text>().text = "Status: " +missionManager.currentMission.status.ToString();

                allMissions[i].gameObject.transform.GetChild(0).GetComponent<Outline>().effectColor = Color.white;
                allMissions[i].gameObject.transform.GetChild(1).GetComponent<Outline>().effectColor = Color.white;
                allMissions[i].gameObject.transform.GetChild(2).GetComponent<Outline>().effectColor = Color.white;
            }

            // if mission is complete then change colour to green
            else if (missionManager.missionList[i].status == Mission.MissionStatus.COMPLETE)
            {
                allMissions[i].GetComponent<Image>().color = Color.green;
                // Set the text UI to display missions properties.
                allMissions[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = missionManager.missionList[i].title;
                allMissions[i].gameObject.transform.GetChild(1).GetComponent<Text>().text = missionManager.currentMission.objective;
                allMissions[i].gameObject.transform.GetChild(2).GetComponent<Text>().text = "Status: " + missionManager.missionList[i].status.ToString();

                allMissions[i].gameObject.transform.GetChild(0).GetComponent<Outline>().effectColor = Color.green;
                allMissions[i].gameObject.transform.GetChild(1).GetComponent<Outline>().effectColor = Color.green;
                allMissions[i].gameObject.transform.GetChild(2).GetComponent<Outline>().effectColor = Color.green;
            }

            // Increments missions added
            missionsAdded++;

            // Transform position of the missions to fit UI
            if (i > 0)
            {
                //Debug.Log(allMissions[i].transform.position);
                allMissions[i].transform.position = new Vector3(allMissions[i].transform.position.x, allMissions[i].transform.position.y - i * 55, allMissions[i ].transform.position.z);
                //Debug.Log(allMissions[i].transform.position);
            }
        }
    }

    public void CompletedMissionsButton()
    {
        // Set title
        canvasTitle.text = "Completed Missions";

        // Set and disable appropriate canvas
        allMissionCanvas.SetActive(false);
        currentMissionCanvas.SetActive(false);
        completedMissionCanvas.SetActive(true);
        currentMissionBackground.SetActive(false);

        // Allows user to scroll
        scrollView.vertical = true;
        scrollbar.SetActive(true);
        scrollView.verticalNormalizedPosition = 1f;

        // Loops through to add missions
        for (int i = 0; i < missionManager.missionList.Count; i++)
        {
            // Skip listID if mission is not complete
            if (missionManager.missionList[i].status != Mission.MissionStatus.COMPLETE)
            {
                continue;
            }

            // Stops making new instances if button is pressed more than once.
            if (i < completedMissionsAdded)
            {
                return;
            }


            completedMissions[i] = Instantiate(missionBorder);
            completedMissions[i].transform.SetParent(completedMissionCanvas.transform, false);

            // Set the text UI to display missions properties.
            completedMissions[i].GetComponent<Image>().color = Color.green;
            // Set the text UI to display missions properties.
            completedMissions[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = missionManager.missionList[i].title;
            completedMissions[i].gameObject.transform.GetChild(1).GetComponent<Text>().text = missionManager.currentMission.objective;
            completedMissions[i].gameObject.transform.GetChild(2).GetComponent<Text>().text = "Status: " + missionManager.missionList[i].status.ToString();

            completedMissions[i].gameObject.transform.GetChild(0).GetComponent<Outline>().effectColor = Color.green;
            completedMissions[i].gameObject.transform.GetChild(1).GetComponent<Outline>().effectColor = Color.green;
            completedMissions[i].gameObject.transform.GetChild(2).GetComponent<Outline>().effectColor = Color.green;

            // Increments missions added
            completedMissionsAdded++;

            // Transform position of the missions to fit UI
            if (i > 0)
            {
                completedMissions[i].transform.position = new Vector3(completedMissions[i].transform.position.x, completedMissions[i].transform.position.y - i * 55, completedMissions[i].transform.position.z);
            }
        }

    }

}
