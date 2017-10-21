using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponReloader : MonoBehaviour {
    
     //Player max ammo, will deduct clip size from this when clip is empty.        
    int ammoInClip;
    public int shotsFiredInClip;
    bool isReloading;
    public float reloadSpeed;
    public int clipSize;
    public FiringType firingType;
    public float rateOfFire;

    // Text box for ammo Count
    public Text maxAmmoTextBox;
    public Text ammoInClipTextBox;

    UIManager uiManager;
    PlayerShoot playerShoot;
    Shooting shooting;
    [SerializeField] WeaponStats currentWeapon;

    void Start()
    {
        playerShoot = FindObjectOfType<PlayerShoot>();
        uiManager = FindObjectOfType<UIManager>();
    }

    void update()
    {

    }

    public int RoundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    public void Reload()
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

        StartCoroutine("ReloadTime", reloadSpeed);        
    }

    IEnumerator ReloadTime(float reloadTime)
    {
        print("Reload Started!");
        yield return new WaitForSeconds(reloadTime);
        //will wait for the time given in the reloadTime variable to run this code
        print("Reload Executed!");
        isReloading = false;
        //ammoInClip -= shotsFiredInClip;

        // Reduces max ammo
        
        currentWeapon.maxAmmo -= shotsFiredInClip;
        if(currentWeapon.maxAmmo < 0)
        {
            currentWeapon.maxAmmo = 0;
            maxAmmoTextBox.text = currentWeapon.maxAmmo.ToString();
        }

        //Deducts shots fired from the clip
        shotsFiredInClip = 0;
        currentWeapon.AmmoInClip = currentWeapon.clipSize;

        // Sets text box for ammo
        ammoInClipTextBox.text = currentWeapon.AmmoInClip.ToString();
        maxAmmoTextBox.text = currentWeapon.maxAmmo.ToString();


        if (ammoInClip < 0)
        {
            ammoInClip = 0;
            shotsFiredInClip -= ammoInClip;
        }
    }

    // Gets the corresponding weapon variables for when the weapon gets changed
    public void GetWeaponVariables(int ClipSize, float ReloadSpeed, int AmmoInClip, float RateOfFire,
        FiringType ft, Shooting Shooting, WeaponStats CurrentWeapon)
    {
        clipSize = ClipSize;
        reloadSpeed = ReloadSpeed;
        ammoInClip = AmmoInClip;
        rateOfFire = RateOfFire;
        firingType = ft;
        shotsFiredInClip = ClipSize - AmmoInClip;
        playerShoot.shooting = Shooting;
        currentWeapon = CurrentWeapon;

        // Sets the ammo textbox indicator
        maxAmmoTextBox.text = currentWeapon.maxAmmo.ToString();
        ammoInClipTextBox.text = AmmoInClip.ToString();
    }



    // Reduces the amount off ammo in the clip and saves remaining ammo for weapon change.
    public void TakeFromClip(int amount)
    {
        shotsFiredInClip += amount;
        currentWeapon.AmmoInClip -= amount;
        ammoInClipTextBox.text = currentWeapon.AmmoInClip.ToString();
        if (currentWeapon.AmmoInClip == 0 && currentWeapon.maxAmmo == 0)
        {
            uiManager.weaponNoAmmo();
        }
    }
}
