using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponReloader : MonoBehaviour {
    
     //Player max ammo, will deduct clip size from this when clip is empty.        
    public int shotsFiredInClip;
    bool isReloading;
    public int clipSize;
    public FiringType firingType;
    public float rateOfFire;

    UIManager uiManager;
    PlayerShoot playerShoot;
    
    public WeaponStats currentWeapon;

    void Start()
    {
        playerShoot = FindObjectOfType<PlayerShoot>();
        uiManager = FindObjectOfType<UIManager>();
    }
   
    public int RoundsRemainingInClip
    {
        get
        {
            shotsFiredInClip = currentWeapon.clipSize - currentWeapon.AmmoInClip;
            //Debug.Log(shotsFiredInClip);
            return currentWeapon.clipSize - shotsFiredInClip;
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    public void ReloadCheck()
    {
        if (isReloading)
        {
            return;
            //checks if the player is reloading, if so, return.
        }

        isReloading = true;
        ExecuteReload();      
        //Execute Reload
    }

    private void ExecuteReload()
    {
        if(currentWeapon.maxAmmo == 0)
        {
            print("Out of Ammo!");
            isReloading = false;
            return;
        }

        StartCoroutine("Reload", currentWeapon.ReloadSpeed);        
    }

    public void StopReload()
    {
        StopCoroutine(Reload(0f));
    }

    // Coroutine reloads the weapon after the weapons reload speed variable
    public IEnumerator Reload(float reloadTime)
    {
        //print("Reload Started!");
        yield return new WaitForSeconds(reloadTime);
        //will wait for the time given in the reloadTime variable to run this code
        //print("Reload Executed!");
        isReloading = false;
        shotsFiredInClip = currentWeapon.clipSize - currentWeapon.AmmoInClip;

        // Only effect the current ammo if weapon has infinite ammo (This should only be the pistol)
        if (currentWeapon.infiniteAmmo)
        {
            shotsFiredInClip = 0;
            currentWeapon.AmmoInClip = currentWeapon.clipSize;
        }

        else
        {
            // If the player doesn't have infinite ammo then update max ammo accordingly
            // If player has max ammo, check if max ammo is less than the clip size, and add ammo accordingly
            if ((currentWeapon.maxAmmo <= currentWeapon.clipSize) && currentWeapon.maxAmmo > 0 && (currentWeapon.maxAmmo + currentWeapon.AmmoInClip) < currentWeapon.clipSize)
            {
                Debug.Log(currentWeapon.maxAmmo + currentWeapon.AmmoInClip);
                Debug.Log("Ammo in clip before: " + currentWeapon.AmmoInClip);

                currentWeapon.AmmoInClip += currentWeapon.maxAmmo;
                Debug.Log("Ammo in clip after : " + currentWeapon.AmmoInClip);

                // Sets max ammo to 0 as all has been used
                currentWeapon.maxAmmo = 0;

                //Resets the shots fired in clip based on how much ammo is remaining
                shotsFiredInClip = currentWeapon.clipSize - currentWeapon.AmmoInClip;

            }

            // Deducts max ammo based on how many shots have been fired and reset
            else
            {
                currentWeapon.maxAmmo -= shotsFiredInClip; // Reduces max ammo
                currentWeapon.AmmoInClip = currentWeapon.clipSize; // Sets current ammo to full clipsize
                shotsFiredInClip = 0; // Reset shots fired
            }

            // Prevents ammo in clip from going below 0
            if (currentWeapon.AmmoInClip < 0)
            {
                currentWeapon.AmmoInClip = 0;
            }

            // Prevents max ammo from going below 0
            if (currentWeapon.maxAmmo < 0)
            {
                currentWeapon.maxAmmo = 0;
            }
        }
        // Sets text box for ammo
        uiManager.updateAmmoTextbox();
    }

    // Gets the corresponding weapon variables for when the weapon gets changed
    public void GetWeaponVariables(int ClipSize, float ReloadSpeed, int AmmoInClip, float RateOfFire,
        FiringType ft, Shooting Shooting, WeaponStats CurrentWeapon)
    {
        clipSize = ClipSize;
        rateOfFire = RateOfFire;
        firingType = ft;
        shotsFiredInClip = ClipSize - AmmoInClip;
        playerShoot.shooting = Shooting;
        currentWeapon = CurrentWeapon;

        // Sets the ammo textbox indicator
        uiManager.updateAmmoTextbox();
    }



    // Reduces the amount of ammo in the clip and saves remaining ammo for weapon change.
    public void TakeFromClip(int amount)
    {
        shotsFiredInClip += amount;
        currentWeapon.AmmoInClip -= amount;
 
        uiManager.updateAmmoTextbox();
        if (currentWeapon.AmmoInClip == 0 && currentWeapon.maxAmmo == 0)
        {
            uiManager.weaponNoAmmo(currentWeapon.weaponID);
        }
    }
}
