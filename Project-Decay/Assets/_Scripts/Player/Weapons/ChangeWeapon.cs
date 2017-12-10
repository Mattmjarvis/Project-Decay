using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour {

    // Weapon variables
    public int currentWeapon;
    public GameObject[] weapons;

    // UI Variables
    public GameObject[] weaponBoxImage;
    public GameObject[] weaponIcon;

    // Components
    WeaponReloader weaponReloader;
    UIManager uiManager;
    SimpleThirdPerson playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<SimpleThirdPerson>();
        weaponReloader = gameObject.GetComponent<WeaponReloader>();
        uiManager = FindObjectOfType<UIManager>();
        changeWeapon(2); // Sets the starting weapon to pistol
    }

    // Gets the user input to change weapon
    private void Update()
    {
        GetWeaponChangeInput();
    }

#region Function to get input from the user to change weapon
    // Gets the input to change weapon
    public void GetWeaponChangeInput()
    {     
        // Change to weapon 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Checks if weapon is available
            if (weapons[0] != null)
            {
                changeWeapon(0);
            }
        }

        // Change to weapon 2
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Checks if weapon is available
            if (weapons[1] != null)
            {
                changeWeapon(1);
            }
        }

   
        // Change to weapon 3
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
                if (weapons[2] != null)
                {
                    changeWeapon(2);
                }        
        }

        // Change to Special weapon
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Checks if weapon is available
            if (weapons[3] != null)
            {
                changeWeapon(3);
            }
        }
    }
    #endregion

#region Function to change the weapon and pass stats to the weapon reloader
    public void changeWeapon(int num)
    {
   
        // Returns if number pressed is already same weapon being held
        if(num == currentWeapon)
        { 
            return;
        }

        // Passes the number of the current weapon
        currentWeapon = num;
        for (int i = 0; i < weapons.Length; i++)
        {
            // Change to the weapon related to the number
            if (i == num)
            {
                weaponReloader.StopReload();
                playerController.gun = weapons[i];
                playerController.handleLeft = weapons[i].GetComponentInChildren<WeaponStats>().handleLeft;
                playerController.handleRight = weapons[i].GetComponentInChildren<WeaponStats>().handleRight;
                playerController.weaponStats = weapons[i].GetComponentInChildren<WeaponStats>();

                // Stops showing out of ammo if the player has ammo
                if(playerController.weaponStats.ammoInClip > 0)
                {
                    uiManager.DisableOutOfAmmoUI();
                }

                // if the player has an active gun then replace weapon
                if (playerController.gunActive)
                {
                    weapons[i].gameObject.SetActive(true);
                }

                // Gets all the weapons stats and variables for the weapon reloader
                weaponReloader.GetWeaponStats(weapons[i].gameObject.GetComponentInChildren<WeaponStats>());

                // Changes weapon icons according to weapon
                weaponBoxImage[i].gameObject.GetComponent<Image>().sprite = weapons[i].GetComponentInChildren<WeaponStats>().weaponBox_Selected;
                weaponIcon[i].gameObject.GetComponent<Image>().sprite = weapons[i].GetComponentInChildren<WeaponStats>().weaponIcon_Selected;
            }


            // Disables any other weapons
            else
            {
                if(weapons[i] != null)
                {
                    weapons[i].gameObject.SetActive(false);

                    // Changes the weaponUI and sets the icons appropriately
                    weaponBoxImage[i].gameObject.GetComponent<Image>().sprite = weapons[i].GetComponentInChildren<WeaponStats>().weaponBox_Unselected;
                    weaponIcon[i].gameObject.GetComponent<Image>().sprite = weapons[i].GetComponentInChildren<WeaponStats>().weaponIcon_Unselected;
                }



            }
#endregion

        }
    }


}

