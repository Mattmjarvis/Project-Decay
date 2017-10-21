using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour {

    // Weapon variables
    public int currentWeapon;
    public Transform[] weapons;

    // UI Variables
    public GameObject[] weaponBoxImage;
    public GameObject[] weaponIcon;
    UIManager uiManager;

    WeaponReloader weaponReloader;

    private void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Checks if weapon is available
            if (weapons[1] != null)
            {
                changeWeapon(1);
            }
        }

   
        // Change to weapon 3
        if (Input.GetKeyDown(KeyCode.Alpha3))
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
            if (weapons[4] != null)
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
            Debug.Log("Already equipped");
            return;
        }

        // Passes the number of the current weapon
        currentWeapon = num;
        for (int i = 0; i < weapons.Length; i++)
        {
            // Change to the weapon related to the number
            if (i == num)
            {                              
                weapons[i].gameObject.SetActive(true);

                // Gets all the weapons stats and variables for the weapon reloader
                weaponReloader.GetWeaponVariables(weapons[i].gameObject.GetComponent<WeaponStats>().clipSize,
                weapons[i].gameObject.GetComponent<WeaponStats>().ReloadSpeed,
                weapons[i].gameObject.GetComponent<WeaponStats>().AmmoInClip,
                weapons[i].gameObject.GetComponent<WeaponStats>().rateOfFire,
                weapons[i].gameObject.GetComponent<WeaponStats>().firingType,
                weapons[i].gameObject.GetComponent<Shooting>(),
                weapons[i].gameObject.GetComponent<WeaponStats>());

                // Changes weapon icons according to weapon
                weaponBoxImage[i].gameObject.GetComponent<Image>().sprite = weapons[i].GetComponent<WeaponStats>().weaponBox_Selected;
                weaponIcon[i].gameObject.GetComponent<Image>().sprite = weapons[i].GetComponent<WeaponStats>().weaponIcon_Selected;
                uiManager.weaponBoxImage = weaponBoxImage[i].gameObject.GetComponent<Image>();

                Debug.Log("Weapon selected: " + weapons[i].name);
            }


            // Disables any other weapons
            else
            {
                weapons[i].gameObject.SetActive(false);

                // Changes the weaponUI and sets the icons appropriately
                weaponBoxImage[i].gameObject.GetComponent<Image>().sprite = weapons[i].GetComponent<WeaponStats>().weaponBox_Unselected;
                weaponIcon[i].gameObject.GetComponent<Image>().sprite = weapons[i].GetComponent<WeaponStats>().weaponIcon_Unselected;
            }
#endregion

        }
    }


}

