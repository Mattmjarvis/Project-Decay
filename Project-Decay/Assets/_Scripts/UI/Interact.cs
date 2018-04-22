using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    UIManager uiManager;
    private float interactDistance = 5f;
    //public bool searchRange;

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

            // OLD CODE
            //#region Lootpile check
            //// Checks see if lootpile is infront of player
            //if (hit.collider.CompareTag("Lootpile"))
            //{
            //    if (hit.collider.GetComponentInParent<Lootpile>().searched == false)
            //    {
            //        uiManager.enableSearchTip();
            //        // Gets key input
            //        if (Input.GetKeyDown(KeyCode.E))
            //        {
            //            hit.collider.GetComponentInParent<Lootpile>().SpawnItems(); // Spawns loot items
            //            uiManager.disableSearchTip(); // disabled the search tip UI
            //        }
            //    }
            //}
            //#endregion


        }
        // Turns of tooltips
        else
        {
            uiManager.disableInteractTip();
            uiManager.disableSearchTip();
        }


    }
}

    