using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    public GameObject radiationImage;

    // Weaponbox UI Variables
    public Image weaponBoxImage;
    WeaponReloader weaponReloader;
    // Text box for ammo Count
    public Text maxAmmoTextBox;
    public Text ammoInClipTextBox;
    private Color redColour =  new Color(255, 0, 0);
    private Color normalColour = new Color(255, 255, 255);

    // Use this for initialization
    void Start ()
    {
        weaponReloader = FindObjectOfType<WeaponReloader>();
    }

    /// <summary>
    ///  Changes the activate of the radiation symbol
    /// </summary>
    // Turns on radiation symbol
    public void turnOnRadiationSymbol()
    {        
        radiationImage.SetActive(true);       
    }

    // Turns off radiation symbol
    public void turnOffRadiationSymbol()
    {
        radiationImage.SetActive(false);

    }

    /// <summary>
    /// Changes the colours of the weapon box if they have no ammo
    /// </summary>
    // Changes the weaponbox colour to red if out of ammo
    public void weaponNoAmmo()
    {
        weaponBoxImage.color = Color.red;
    }

    // Changes the weaponbox colour to normal if has ammo
    public void weaponHasAmmo()
    {
        weaponBoxImage.color = normalColour;
    }

    public void updateAmmoTextbox()
    {
        ammoInClipTextBox.text = weaponReloader.currentWeapon.AmmoInClip.ToString();
        maxAmmoTextBox.text = weaponReloader.currentWeapon.maxAmmo.ToString();
    }
}
