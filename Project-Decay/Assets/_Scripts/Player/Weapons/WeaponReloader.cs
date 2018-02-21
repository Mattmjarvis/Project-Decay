using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponReloader : MonoBehaviour {
    
     //Player max ammo, will deduct clip size from this when clip is empty.        
    private int shotsFiredInClip;
    bool isReloading;

    UIManager uiManager;    
    
    public WeaponStats currentWeapon;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
   
    public int RoundsRemainingInClip
    {
        get
        {
            shotsFiredInClip = currentWeapon.clipSize - currentWeapon.ammoInClip;
            //Debug.Log(shotsFiredInClip);
            return currentWeapon.clipSize - shotsFiredInClip;
        }
    }
    public void ReloadGun()
    {       
       ReloadCheck();
        //calls the Reload method which will then call the executeReload method
        print("reloader was not null and Reload has been called");
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

        StartCoroutine("Reload", currentWeapon.reloadSpeed);        
    }

    public void StopReload()
    {
        StopCoroutine(Reload(0f));
        isReloading = false;
        uiManager.TurnOffReloadProgressBar(); // Turns off the progress bar
        uiManager.turnOnCrosshair(); // Turns on the crosshair
    }

    // Coroutine reloads the weapon after the weapons reload speed variable
    public IEnumerator Reload(float reloadTime)
    {
        uiManager.turnOffCrosshair(); // Disables the crosshair whilst reloading
        uiManager.SetReloadProgress();
        //print("Reload Started!");
        yield return new WaitForSeconds(reloadTime);
        //will wait for the time given in the reloadTime variable to run this code
        //print("Reload Executed!");
        isReloading = false;
        shotsFiredInClip = currentWeapon.clipSize - currentWeapon.ammoInClip;

        // Only effect the current ammo if weapon has infinite ammo (This should only be the pistol)
        if (currentWeapon.infiniteAmmo)
        {
            shotsFiredInClip = 0;
            currentWeapon.ammoInClip = currentWeapon.clipSize;
        }

        else
        {
            // If the player doesn't have infinite ammo then update max ammo accordingly
            // If player has max ammo, check if max ammo is less than the clip size, and add ammo accordingly
            if ((currentWeapon.maxAmmo <= currentWeapon.clipSize) && currentWeapon.maxAmmo > 0 && (currentWeapon.maxAmmo + currentWeapon.ammoInClip) < currentWeapon.clipSize)
            {
                Debug.Log(currentWeapon.maxAmmo + currentWeapon.ammoInClip);
                Debug.Log("Ammo in clip before: " + currentWeapon.ammoInClip);

                currentWeapon.ammoInClip += currentWeapon.maxAmmo;
                Debug.Log("Ammo in clip after : " + currentWeapon.ammoInClip);

                // Sets max ammo to 0 as all has been used
                currentWeapon.maxAmmo = 0;

                //Resets the shots fired in clip based on how much ammo is remaining
                shotsFiredInClip = currentWeapon.clipSize - currentWeapon.ammoInClip;

            }

            // Deducts max ammo based on how many shots have been fired and reset
            else
            {
                currentWeapon.maxAmmo -= shotsFiredInClip; // Reduces max ammo
                currentWeapon.ammoInClip = currentWeapon.clipSize; // Sets current ammo to full clipsize
                shotsFiredInClip = 0; // Reset shots fired
            }

            // Prevents ammo in clip from going below 0
            if (currentWeapon.ammoInClip < 0)
            {
                currentWeapon.ammoInClip = 0;
            }

            // Prevents max ammo from going below 0
            if (currentWeapon.maxAmmo < 0)
            {
                currentWeapon.maxAmmo = 0;
            }
        }

        uiManager.updateAmmoTextbox();        // Sets text box for ammo

        //uiManager.DisableOutOfAmmoUI();        // Stop displaying out of ammo
    }

    // Gets the corresponding weapon variables for when the weapon gets changed
    public void GetWeaponStats(WeaponStats CurrentWeapon)
    {
        currentWeapon = CurrentWeapon; // Gets the weapon stats from current weapon
        shotsFiredInClip = currentWeapon.clipSize - currentWeapon.ammoInClip; // Checks how many shots have been fired


        // Sets the ammo textbox indicator
        uiManager.updateAmmoTextbox();
    }

    // Reduces the amount of ammo in the clip and saves remaining ammo for weapon change.
    public void TakeFromClip(int amount)
    {
        shotsFiredInClip += amount;
        currentWeapon.ammoInClip -= amount;
 
        uiManager.updateAmmoTextbox();
        if (currentWeapon.ammoInClip == 0 && currentWeapon.maxAmmo == 0)
        {
            uiManager.weaponNoAmmo(currentWeapon.weaponID);
        }
    }
}
