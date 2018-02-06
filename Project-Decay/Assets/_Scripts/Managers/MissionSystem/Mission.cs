using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission
{

    public enum MissionStatus { UNAVAILABLE, CURRENT, NEXT, COMPLETE }

    public string title;      // Mission Title
    public int id;              // Mission ID
    public MissionStatus status; // State of the mission
    public string objective; // description of the mission
    [TextArea]
    public string bio; // Small biography of the mission detail
    public int objectiveCount; // Counter for each objective of the mission
    public int totalObjectives; // How many objectives the player must complete

    public string reward; // String for the player reward

    [HideInInspector]
    public int missionsCompleted; // Counter for how many missions the player has completed



}
