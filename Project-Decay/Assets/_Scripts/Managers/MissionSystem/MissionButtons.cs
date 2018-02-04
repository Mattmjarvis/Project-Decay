using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButtons : MonoBehaviour {

    MissionManager missionManager;
    private ScrollRect scrollView;

    // Text elements for missions
    public Text missionTitle;

    // Floats to keep track of the mission objectives
    private float objectivesCompleted;
    private float objectivesNeeded;
    private float progressTotal;

    // GameObjects for each window element in the mission log
    public GameObject currentMission;


    public GameObject allMissionCanvas;
    public GameObject[] totalMissions;
    public GameObject missionBorder;

    public GameObject completedMissionCanvas;
    public GameObject[] completedMissions;


    // Use this for initialization
    void Awake ()
    {
        // Get missionmanager component
        missionManager = gameObject.GetComponent<MissionManager>();
        scrollView = FindObjectOfType<ScrollRect>();
	}

    private void Start()
    {
        // Instantly set first mission for player
        CurrentMissionButton();
        totalMissions = new GameObject[missionManager.missionList.Count];
    }

    // Displays the players current mission
    public void CurrentMissionButton()
    {
        scrollView.vertical = false;
        // Calculate percentage completed


        objectivesCompleted = missionManager.currentMission.objectiveCount;
        objectivesNeeded = missionManager.currentMission.totalObjectives;
        progressTotal = (objectivesCompleted / objectivesNeeded) * 100;

        // Sets all the text UI elements for the players current mission
        missionTitle.text = missionManager.currentMission.title + "\n" + missionManager.currentMission.objective + "\n \n" + progressTotal.ToString() + "% Complete";
    }

    // Displays all the missions available
    public void AllMissionsButton()
    {
        scrollView.vertical = true;

        for(int i = 0; i < missionManager.missionList.Count; i++)
        {
            Debug.Log(i);
            totalMissions[i] = Instantiate(missionBorder, allMissionCanvas.transform);

            totalMissions[i].GetComponentInChildren<Text>().text = missionManager.missionList[i].title + "\n" + missionManager.missionList[i].objective;

            if (i > 0)
            {
                totalMissions[i].transform.position = new Vector3(totalMissions[i-1].transform.position.x, totalMissions[i-1].transform.position.y - 82, totalMissions[i-1].transform.position.z);
            }
        }

    }

}
