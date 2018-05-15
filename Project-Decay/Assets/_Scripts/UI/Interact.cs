using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    UIManager uiManager;
    MissionCompletionInfo MCI;
    private float interactDistance = 5f;

    // Variables for checking lootpiles
    public float distance;
    GameObject closestPile;
    GameObject[] lootpiles;

    // Use this for initialization
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        MCI = FindObjectOfType<MissionCompletionInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
        // Raycast Variables
        Ray interactRay = new Ray(transform.position, transform.forward);
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
                    uiManager.upgradeInterfaceOpen = true;
                    uiManager.EnableUpgradeInterface();

                }
            }
            #endregion

            
            #region Supply crate check
            // Checks see if lootpile is infront of player
            if (hit.collider.CompareTag("SupplyCrate"))
            {
                Debug.Log("I see a crate");
                if (hit.collider.GetComponentInParent<SearchCrate>().searched == false)
                {
                    uiManager.enableSearchTip();
                    // Gets key input
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.GetComponentInParent<SearchCrate>().Search(); // Searches the supply crate
                        uiManager.disableSearchTip(); // disabled the search tip UI
                    }
                }
            }
            #endregion


        }
        // Turns of tooltips
        else
        {
            uiManager.disableInteractTip();
        }
    }

    // Checks distance between player and lootpile
    public void DistanceCheck()
    {
        // Find all lootpiles in the scene
        lootpiles = GameObject.FindGameObjectsWithTag("Lootpile");

        // Get distance
        foreach (GameObject pile in lootpiles)
        {
            if (Vector3.Distance(pile.transform.position, gameObject.transform.position) < 10f && (pile.GetComponent<Lootpile>().searched == false)){
                closestPile = pile;
                uiManager.enableSearchTip(); // Show search tooltip

                if (Input.GetKeyDown(KeyCode.E))
                {
                    closestPile = null;
                    pile.GetComponent<Lootpile>().SpawnItems();
                }
            }
        }


        if (closestPile != null)
        {
            if (Vector3.Distance(closestPile.transform.position, gameObject.transform.position) > 10f || closestPile.GetComponent<Lootpile>().searched == true)
            {
                uiManager.disableSearchTip();
            }
        }

    }
}
   

    