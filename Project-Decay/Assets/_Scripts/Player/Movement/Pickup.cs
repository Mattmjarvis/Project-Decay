using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    AssaultRifleAmmo,
    ShotgunAmmo,
    PistolAmmo,
    Uranium

}
public class Pickup : MonoBehaviour {

    UIManager uiManager;
    public PickupType pickupType;
    public int amountToGive;

    // Each stats are needed to be assigned manually so it can find inactive objects

    public WeaponStats[] allWeaponStats;
    public WeaponStats weaponStatsAR;
    public WeaponStats weaponStatsSG;

	// Use this for initialization
	void Start () {
        uiManager = FindObjectOfType<UIManager>();
        FindWeaponStats();
	}
	
    public void OnTriggerEnter(Collider other)
    {
        // Gives ammo if the collision is with the player
        if (other.CompareTag("Player"))
        {
            FindWeaponStats();
            GiveWeaponAmmo();            
            Destroy(gameObject);
            uiManager.updateAmmoTextbox();           
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
        }
    }

    // Gives the weapon ammo based on the enum type.
    public void GiveWeaponAmmo()
    {
        // Gives ammo to assault Rifle
        if (pickupType == PickupType.AssaultRifleAmmo)
        {
            weaponStatsAR.maxAmmo += amountToGive;
            print("MaxAmmo is " + weaponStatsAR.maxAmmo);
            uiManager.weaponHasAmmo(0); // Sets colour to show they have ammo
        }

        // Gives ammo to Shotgun
        else if(pickupType == PickupType.ShotgunAmmo)
        {
            weaponStatsSG.maxAmmo += amountToGive;
            uiManager.weaponHasAmmo(1); // Sets colour to show they have ammo
        }

        // Give player Uranium Currency
        else if(pickupType == PickupType.Uranium)
        {
            FindObjectOfType<Wallet>().IncreaseFunds(amountToGive);
        }


        
    }
}
