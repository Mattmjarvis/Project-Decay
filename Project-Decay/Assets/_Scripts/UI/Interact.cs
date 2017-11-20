using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    UIManager uiManager;
    private float interactDistance = 5f;

    // Use this for initialization
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast Variables
        Ray interactRay = new Ray(transform.position, transform.forward) ;
        RaycastHit hit;

        // Checks for raycast
        if (Physics.Raycast(interactRay, out hit, interactDistance))
        {
           Debug.DrawRay(interactRay.origin, Vector3.forward);
            #region UpgradeMachine check
            // Checks to see if upgrade menu is infront of player
            if (hit.collider.CompareTag("Upgrade"))
            {
                uiManager.enableInteractTip();
                // Gets key input
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Opens the upgrade interface
                    uiManager.EnableUpgradeInterface();
                }
            }
            #endregion
            #region UpgradeMachine check
            // Checks see if lootpile is infront of player
            if (hit.collider.CompareTag("Lootpile"))
            {
                uiManager.enableSearchTip();
                // Gets key input
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Search Loot Pile");
                }
            }
            #endregion


        }
        // Turns of tooltips
        else
        {
            uiManager.disableInteractTip();
            uiManager.disableSearchTip();
        }


    }
}

    