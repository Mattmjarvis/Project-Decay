using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PickupType
{
    AssaultRifleAmmo,
    ShotgunAmmo,
    PistolAmmo,
    Uranium,
    Weapon_AR,
    Weapon_SG,
    Weapon_PS
}



public class Pickup : MonoBehaviour {

    UIManager uiManager;
    MissionManager mm;
    public PickupType pickupType;

    public int amountToGive;

    // Each stats are needed to be assigned manually so it can find inactive objects
    private WeaponStats[] allWeaponStats;
    private WeaponStats weaponStatsAR;
    private WeaponStats weaponStatsSG;
    private WeaponStats weaponStatsPS;

    // Weapon slot UI to change color
    public Image weaponslotAR;
    public Image weaponslotSG;
    public Image weaponslotPS;

	// Use this for initialization
	void Start () {
        uiManager = FindObjectOfType<UIManager>();
        mm = FindObjectOfType<MissionManager>();
        FindWeaponStats();

        weaponslotAR = GameObject.FindGameObjectWithTag("ARUI").GetComponent<Image>();
        weaponslotSG = GameObject.FindGameObjectWithTag("ShotgunUI").GetComponent<Image>();
        weaponslotPS = GameObject.FindGameObjectWithTag("PistolUI").GetComponent<Image>();
    }
	
    public void OnTriggerEnter(Collider other)
    {
        // Gives ammo if the collision is with the player
        if (other.CompareTag("Player"))
        {
            FindWeaponStats();
            PickupObject();            
            Destroy(gameObject);
 


        }
    }


    // This function is in place due to prefabs not being to able to be set another gameobject component before instantiating. When player collides it will find all weapon stats and get scripts accordingly.
    public void FindWeaponStats()
    {
        allWeaponStats = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<WeaponStats>(true); // Finds all weapon stats and their components in children (including inactive)

        // Checks through each weapon stat and set to correct weapon
        for(int i = 0; i < allWeaponStats.Length; i++)
        {
            // Sets AR Stats
            if(allWeaponStats[i].gameObject.name == ("ARStats"))
            {
                weaponStatsAR = allWeaponStats[i];
            }

            // Sets SG Stats
            if (allWeaponStats[i].gameObject.name == ("SGStats"))
            {
                weaponStatsSG = allWeaponStats[i];
            }

            // Sets PS Stats
            if (allWeaponStats[i].gameObject.name == ("PSStats")) 
            {
                weaponStatsPS = allWeaponStats[i];
            }
        }
    }

    // Gives the weapon ammo based on the enum type.
    public void PickupObject()
    {
        // Gives access to assault rifle
        if (pickupType == PickupType.Weapon_AR)
        {
            weaponStatsAR.weaponAvailable = true;
            weaponslotAR.color = Color.white;
            
        }

        // Gives access to shotgun
        else if (pickupType == PickupType.Weapon_SG)
        {
            weaponStatsSG.weaponAvailable = true;
            weaponslotSG.color = Color.white;
        }

        // Gives access to pistol
        if (pickupType == PickupType.Weapon_PS)
        {
            weaponStatsPS.weaponAvailable = true;
            weaponslotPS.color = Color.white;
            mm.hasPistol = true;
            if(mm.currentMission.id == 2)
            {
                mm.IncrementMissionObjective();
            }
        }

        // Gives ammo to assault Rifle
        else if (pickupType == PickupType.AssaultRifleAmmo)
        {
            weaponStatsAR.maxAmmo += amountToGive;
            print("MaxAmmo is " + weaponStatsAR.maxAmmo);

            if (weaponStatsAR.weaponAvailable == true)
            {
                uiManager.weaponHasAmmo(0); // If the weapon is available then set colour to show they have ammo
            }
        }

        // Gives ammo to Shotgun
        else if(pickupType == PickupType.ShotgunAmmo)
        {
            weaponStatsSG.maxAmmo += amountToGive;

            if (weaponStatsSG.weaponAvailable == true)
            {
                uiManager.weaponHasAmmo(1); // If the weapon is available then set colour to show they have ammo
            }
        }

        // Give player Uranium Currency
        else if(pickupType == PickupType.Uranium)
        {
            FindObjectOfType<Wallet>().IncreaseFunds(amountToGive);
        }


        uiManager.updateAmmoTextbox(); // Updates the ammo for the player
    }
}
