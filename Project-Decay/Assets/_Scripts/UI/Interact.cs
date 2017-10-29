using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    UIManager uiManager;
    Camera cam;
    public GameObject crosshair;
    private float interactDistance = 6f;

    // Use this for initialization
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast Variables
        Ray aimRay = cam.ScreenPointToRay(crosshair.transform.position);
        RaycastHit hit;

        // Checks for raycast
        if (Physics.Raycast(aimRay, out hit, interactDistance))
        {
            #region UpgradeMachine check
            // Checks door to see if its open
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
        }
        // Turns of tooltips
        else
        {
            uiManager.disableInteractTip();
        }


    }
}

    