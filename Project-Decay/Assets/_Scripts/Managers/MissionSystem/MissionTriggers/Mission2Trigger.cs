using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2Trigger : MonoBehaviour
{

    MissionManager mm;

    // Use this for initialization
    void Start()
    {
        mm = FindObjectOfType<MissionManager>(); // Get mission manager
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Complete a mission objective if player activates trigger
    private void OnTriggerEnter(Collider other)
    {
        // Complete mission 1
        if (other.tag == "Player" && mm.currentMission.id == 1)
        {
            mm.IncrementMissionObjective();
        }
    }
}
