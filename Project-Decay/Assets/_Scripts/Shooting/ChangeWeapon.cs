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

    WeaponReloader weaponReloader;

    private void Start()
    {
        weaponReloader = gameObject.GetComponent<WeaponReloader>();
        changeWeapon(2);
    }

    // Gets the user input to change weapon
    private void Update()
    {
        // Checks for input in order to change weapon
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("Weapon changed - Assault Rifle");
            changeWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("Weapon changed - Shotgun");
            changeWeapon(1);
        }

        // Enable this code after more weapons have been added

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            print("Weapon changed - Pistol");
            changeWeapon(2);
        }

    }

    public void changeWeapon(int num)
    {
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
            }


            // Disables any other weapons
            else
            {
                weapons[i].gameObject.SetActive(false);

                // Changes the weaponUI and sets the icons appropriately
                weaponBoxImage[i].gameObject.GetComponent<Image>().sprite = weapons[i].GetComponent<WeaponStats>().weaponBox_Unselected;
                weaponIcon[i].gameObject.GetComponent<Image>().sprite = weapons[i].GetComponent<WeaponStats>().weaponIcon_Unselected;
            }

                    
        }
    }


}

