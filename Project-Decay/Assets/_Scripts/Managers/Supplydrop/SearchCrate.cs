using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SearchCrate : MonoBehaviour {

    // Get components
    UIManager uiManager;
    Interact interact;
    MissionCompletionInfo MCI;



    public bool searched = false;

    public void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        interact = FindObjectOfType<Interact>();
        MCI = FindObjectOfType<MissionCompletionInfo>();
    }

    // Gives the player radiation suit and checks missioncompletioninfo
    public void Search()
    {
        // Complete mission 14
        MCI.supplyCrateSearched = true;
        MCI.hasRadSuit = true;
        MCI.MissionCompletionCheck();

        this.GetComponent<SearchCrate>().enabled = false;
            searched = true;
            uiManager.disableSearchTip();
        }
    }

