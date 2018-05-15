using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    #region UI Images
    public GameObject radiationImage;
    public GameObject interactImage;
    public GameObject searchImage;
    public GameObject crosshairImage;
    public Image reloadProgressBar;
    #endregion

    #region WeaponUI Variables
    public Image[] weaponBoxImage;
    WeaponReloader weaponReloader;
    // Text boxes for ammo Count
    public Text maxAmmoTextBox;
    public Text ammoInClipTextBox;
    private Color normalColour = new Color(255, 255, 255);

    public GameObject outOfAmmoText;
    #endregion

    #region WalletUI Variables
    public Text walletTextbox;
    Wallet wallet;
    #endregion  

    public GameObject UpgradeInterface;

    // Components
    SimpleThirdPerson playerController;
    InputManager inputManager;
    FadeManager fader;

    public bool upgradeInterfaceOpen = false;
    public float reloadCurrentTime;

    // Use this for initialization
    void Start ()
    {
        fader = FindObjectOfType<FadeManager>();
        fader.SceneFadeInBlack();

        // Disable crosshait
        crosshairImage.SetActive(false);

        // Get components
        wallet = FindObjectOfType<Wallet>().GetComponent<Wallet>() ;
        weaponReloader = FindObjectOfType<WeaponReloader>();
        playerController = FindObjectOfType<SimpleThirdPerson>();
        inputManager = FindObjectOfType<InputManager>();
        updateAmmoTextbox();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Makes the reload progress bar animate
        if (weaponReloader.IsReloading == true && playerController.gunActive)
        {
            ReloadingProgressBar();
        }
    }

    /// <summary>
    ///  Changes the activate of the crosshair
    /// </summary>
    // Turns on crosshair
    #region CrosshairUI
    public void turnOnCrosshair()
    {
        crosshairImage.SetActive(true);
    }

    // Turns off radiation symbol
    public void turnOffCrosshair()
    {
        crosshairImage.SetActive(false);
    }
    #endregion

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
        ammoInClipTextBox.text = weaponReloader.currentWeapon.ammoInClip.ToString();

        // If the weapon has infinite ammo then show infinite ammo symbol
        if (weaponReloader.currentWeapon.infiniteAmmo)
        {
            maxAmmoTextBox.text = "∞";
        }
        // Else show max ammo accordingly
        else
        {
            maxAmmoTextBox.text = weaponReloader.currentWeapon.maxAmmo.ToString();
        }
    }
    #endregion

    ///<summary>
    /// Updates the weapon UI currency textbox
    /// </summary>
    #region WalletTextBoxUI
    public void UpdateWalletTextBox()
    {
        //Debug.Log(wallet.Funds);
        //walletTextbox.text = wallet.Funds.ToString();
    }
    #endregion

    /// <summary>
    /// Enables the upgrade interface
    /// </summary>
    #region UpgradeInterface
    public void EnableUpgradeInterface()
    {
        UpgradeInterface.SetActive(true);
        inputManager.PauseGameplay();       
    }
    #endregion

    /// <summary>
    /// Opens and closes the search UI image
    /// </summary>
    #region searchTip
    public void enableSearchTip()
    {
        searchImage.SetActive(true);
    }
    public void disableSearchTip()
    {
        searchImage.SetActive(false);
    }
#endregion

    ///<summary> 
    /// Opens and closes the interact UI image
    ///</summary>
    #region interactTip
    public void enableInteractTip()
    {
        interactImage.SetActive(true);
    }
    public void disableInteractTip()
    {
        interactImage.SetActive(false);
    }
    #endregion

    ///<summary>
    /// Sets the fill amount of the reload progress bar
    /// </summary>
    #region ReloadProgress
    // Enables the reload progress bar
    public void TurnOnReloadProgressBar()
    {
        reloadProgressBar.gameObject.SetActive(true);
    }
    // Disables the reload progress bar
    public void TurnOffReloadProgressBar()
    {
        reloadProgressBar.gameObject.SetActive(false);
    }

    // Sets the current time for the reload Progress bar
    public void SetReloadProgress()
    {
        reloadCurrentTime = playerController.weaponStats.reloadSpeed;
        TurnOnReloadProgressBar();
    }

    // This function is used in the update
    public void ReloadingProgressBar()
    {
        // Sets the image fill amount based on how far into reload player is
        if (reloadCurrentTime > 0)
        {
            reloadCurrentTime -= Time.deltaTime;
            reloadProgressBar.GetComponent<Image>().fillAmount = reloadCurrentTime / playerController.weaponStats.reloadSpeed;
        }
        
        // Reenable the crosshair if the player has his weapon drawn
        if(reloadCurrentTime <= 0f && playerController.gunActive)
        {
            DisableOutOfAmmoUI();
            turnOnCrosshair();
        }
    }
#endregion

    ///<summary>
    /// Enables or disabled the out of ammo UI
    ///</summary>
    #region OutOfAmmoUI
    // EnableUI
    public void EnableOutOfAmmoUI()
    {
        outOfAmmoText.SetActive(true);
    }
    // DisableUI
    public void DisableOutOfAmmoUI()
    {
        outOfAmmoText.SetActive(false);
    }
#endregion

}
