using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType
{
    AssaultRifle,
    Shotgun,
    Pistol
}
public class GiveAmmo : MonoBehaviour {

    UIManager uiManager;
    public AmmoType ammoType;
    public int ammoToGive;

    // Each stats are needed to be assigned manually so it can find inactive objects
    public WeaponStats weaponStatsAR;
    public WeaponStats weaponStatsSG;
    public WeaponStats weaponStatsPS;

    WeaponReloader reloader;
    private string sWeaponType;

	// Use this for initialization
	void Start () {
        uiManager = FindObjectOfType<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        // Gives ammo if the collision is with the player
        if (other.CompareTag("Player"))
        {
            GiveWeaponAmmo();            
            Destroy(gameObject);
            uiManager.updateAmmoTextbox();

            
        }
    }

    // Gives the weapon ammo based on the enum type.
    public void GiveWeaponAmmo()
    {
        // Gives ammo to assault Rifle
        if (ammoType == AmmoType.AssaultRifle)
        {
            weaponStatsAR.maxAmmo += ammoToGive;
            print("MaxAmmo is " + weaponStatsAR.maxAmmo);
            uiManager.weaponHasAmmo(0);
        }

        // Gives ammo to Shotgun
        else if(ammoType == AmmoType.Shotgun)
        {
            weaponStatsSG.maxAmmo += ammoToGive;
            uiManager.weaponHasAmmo(1);
        }

        // Gives ammo to pistol
        else if(ammoType == AmmoType.Pistol)
        {
            weaponStatsPS.maxAmmo += ammoToGive;
            uiManager.weaponHasAmmo(2);
        }


        
    }
}
