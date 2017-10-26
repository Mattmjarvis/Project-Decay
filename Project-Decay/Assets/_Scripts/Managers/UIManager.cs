using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    public GameObject radiationImage;


    #region WeaponUI Variables
    public Image[] weaponBoxImage;
    WeaponReloader weaponReloader;
    // Text boxes for ammo Count
    public Text maxAmmoTextBox;
    public Text ammoInClipTextBox;
    private Color normalColour = new Color(255, 255, 255);
    #endregion

    #region WalletUI Variables
    public Text walletTextbox;
    Wallet wallet;
    #endregion  

    public GameObject UpgradeInterface;

    // Use this for initialization
    void Start ()
    {
        // Get components
        wallet = FindObjectOfType<Wallet>().GetComponent<Wallet>() ;
        weaponReloader = FindObjectOfType<WeaponReloader>();
        updateAmmoTextbox();
    }

    /// <summary>
    ///  Changes the activate of the radiation symbol
    /// </summary>
    // Turns on radiation symbol
    #region RadiationUI
    public void turnOnRadiationSymbol()
    {        
        radiationImage.SetActive(true);       
    }

    // Turns off radiation symbol
    public void turnOffRadiationSymbol()
    {
        radiationImage.SetActive(false);
    }
#endregion

    /// <summary>
    /// Changes the colours of the weapon box if they have no ammo
    /// </summary>
    // Changes the weaponbox colour to red if out of ammo
    #region WeaponUI
    public void weaponNoAmmo(int weaponNo)
    {
        weaponBoxImage[weaponNo].color = Color.red;
    }

    // Changes the weaponbox colour to normal if has ammo
    public void weaponHasAmmo(int weaponNo)
    {
        weaponBoxImage[weaponNo].color = normalColour;
    }

    // Updates the player UI to display their ammo
    public void updateAmmoTextbox()
    {
        ammoInClipTextBox.text = weaponReloader.currentWeapon.AmmoInClip.ToString();
        maxAmmoTextBox.text = weaponReloader.currentWeapon.maxAmmo.ToString();
    }
    #endregion

    ///<summary>
    /// Updates the weapon UI currency textbox
    /// </summary>
    #region WalletTextBoxUI
    public void UpdateWalletTextBox()
    {
        Debug.Log(wallet.Funds);
        walletTextbox.text = wallet.Funds.ToString();
    }
    #endregion

    // Enables the upgrade interface
    public void EnableUpgradeInterface()
    {
        UpgradeInterface.SetActive(true);
    }
}
