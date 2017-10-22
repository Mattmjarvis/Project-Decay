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
    WeaponStats weaponStats;
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
            weaponStats = GameObject.Find("AssaultRifle").GetComponent<WeaponStats>();
            weaponStats.maxAmmo += ammoToGive;
        }

        // Gives ammo to Shotgun
        else if(ammoType == AmmoType.Shotgun)
        {
            weaponStats = GameObject.Find("Shotgun").GetComponent<WeaponStats>();
            weaponStats.maxAmmo += ammoToGive;
        }

        // Gives ammo to pistol
        else if(ammoType == AmmoType.Pistol)
        {
            weaponStats = GameObject.Find("Pistol").GetComponent<WeaponStats>();
            weaponStats.maxAmmo += ammoToGive;
        }

        
    }
}
