using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SearchCrate : MonoBehaviour {

    // Get components
    UIManager uiManager;
    Interact interact;
    MissionCompletionInfo MCI;

    // Distance check
    public GameObject player;
    public float distance;

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
            MCI.hasRadSuit = true;
         
            this.GetComponent<SearchCrate>().enabled = false;
            searched = true;
            uiManager.disableSearchTip();
        }
    }
}
