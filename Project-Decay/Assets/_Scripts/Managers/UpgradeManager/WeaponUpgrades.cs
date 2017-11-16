using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrades : MonoBehaviour {

    public WeaponStats weapon;
    public GameObject[] bullet;
    private Wallet wallet;
    private UpgradeManager upgradeManager;

    public Image[] tier1Buttons;
    public Image[] tier2Buttons;
    public Image[] tier3Buttons;
    public Image[] tier4Buttons;

    private int tier1Price = 1000;
    private int tier2Price = 5000;
    private int tier3Price = 10000;
    private int tier4Price = 20000;

    Color redFade = new Color(255, 0, 0);
    // Use this for initialization
    void Start () {
        wallet = FindObjectOfType<Wallet>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // TIER UPGRADES
    // 0 = MAGAZINE SIZE
    // 1 = RELOAD SPEED
    // 2 = WEAPON DAMAGE

    #region Tier1 Upgrades
    // Upgrades the Magazine size
    public void UpgradeT1Size()
    {
        if (wallet.Funds >= tier1Price)
        {
            // Sets colours of buttons
            tier1Buttons[0].color = Color.green;
            tier1Buttons[1].color = redFade;
            tier1Buttons[2].color = redFade;
            // Disables buttons
            tier1Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier1Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier1Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier1Price);

            // Upgrade stat depending on weapon
            if(weapon.gameObject.name == "AssaultRifle")
            {
                weapon.clipSize += 10;
            }
            else if(weapon.gameObject.name == "Shotgun")
            {
                weapon.clipSize += 2;
            }
            else if(weapon.gameObject.name == "Pistol")
            {
                weapon.clipSize += 6;
            }
            
            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }
        
    }

    // Upgrades the Reload speed
    public void UpgradeT1Speed()
    {
        // Check if player can afford
        if (wallet.Funds >= tier1Price)
        {
            // Sets colours of buttons
            tier1Buttons[0].color = redFade;
            tier1Buttons[1].color = Color.green;
            tier1Buttons[2].color = redFade;
            // Disables buttons
            tier1Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier1Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier1Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier1Price);

            // Upgrade Stats
            weapon.reloadSpeed -= 0.5f;

            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }
    }

    // Upgrades the weapon damage
    public void UpgradeT1Damage()
    {
        // Check if player can afford
        if (wallet.Funds >= tier1Price)
        {
            // Sets colours of buttons
            tier1Buttons[0].color = redFade;
            tier1Buttons[1].color = redFade;
            tier1Buttons[2].color = Color.green;
            // Disables buttons
            tier1Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier1Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier1Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Upgrade stat
            weapon.bulletDamage += 15;

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier1Price);
            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }
    }
    #endregion

    #region Tier 2 Upgrades
    // Upgrades the Magazine size
    public void UpgradeT2Size()
    {
        // Check if player can afford
        if (wallet.Funds >= tier2Price)
        {
            // Sets colours of buttons
            tier2Buttons[0].color = Color.green;
            tier2Buttons[1].color = redFade;
            tier2Buttons[2].color = redFade;
            // Disables buttons
            tier2Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier2Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier2Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Upgrade stat depending on weapon
            if (weapon.gameObject.name == "AssaultRifle")
            {
                weapon.clipSize += 10;
            }
            else if (weapon.gameObject.name == "Shotgun")
            {
                weapon.clipSize += 2;
            }
            else if (weapon.gameObject.name == "Pistol")
            {
                weapon.clipSize += 6;
            }

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier2Price);
            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }
    }

    // Upgrades the Reload speed
    public void UpgradeT2Speed()
    {
        // Check if player can afford
        if (wallet.Funds >= tier2Price)
        {
            // Sets colours of buttons
            tier2Buttons[0].color = redFade;
            tier2Buttons[1].color = Color.green;
            tier2Buttons[2].color = redFade;
            // Disables buttons
            tier2Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier2Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier2Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier2Price);

            // Upgrade Stats
            weapon.reloadSpeed -= 0.5f;

            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }

    }

    // Upgrades the weapon damage
    public void UpgradeT2Damage()
    {
        // Check if player can afford
        if (wallet.Funds >= tier2Price)
        {
            // Sets colours of buttons
            tier2Buttons[0].color = redFade;
            tier2Buttons[1].color = redFade;
            tier2Buttons[2].color = Color.green;
            // Disables buttons
            tier2Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier2Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier2Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier2Price);

            // Upgrade Stat
            weapon.bulletDamage += 15;

            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }
    }
    #endregion

    #region Tier 3 Upgrades
    // Upgrades the Magazine size
    public void UpgradeT3Size()
    {
        // Check if player can afford
        if (wallet.Funds >= tier3Price)
        {
            // Sets colours of buttons
            tier3Buttons[0].color = Color.green;
            tier3Buttons[1].color = redFade;
            tier3Buttons[2].color = redFade;
            // Disables buttons
            tier3Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier3Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier3Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier3Price);

            // Upgrade stat depending on weapon
            if (weapon.gameObject.name == "AssaultRifle")
            {
                weapon.clipSize += 10;
            }
            else if (weapon.gameObject.name == "Shotgun")
            {
                weapon.clipSize += 2;
            }
            else if (weapon.gameObject.name == "Pistol")
            {
                weapon.clipSize += 6;
            }

            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }
    }

    // Upgrades the Reload speed
    public void UpgradeT3Speed()
    {
        // Check if player can afford
        if (wallet.Funds >= tier3Price)
        {
            // Sets colours of buttons
            tier3Buttons[0].color = redFade;
            tier3Buttons[1].color = Color.green;
            tier3Buttons[2].color = redFade;
            // Disables buttons
            tier3Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier3Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier3Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier3Price);

            // Upgrade Stats
            weapon.reloadSpeed -= 0.5f;

            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }
    }

    // Upgrades the weapon damage
    public void UpgradeT3Damage()
    {
        // Check if player can afford
        if (wallet.Funds >= tier3Price)
        {
            // Sets colours of buttons
            tier3Buttons[0].color = redFade;
            tier3Buttons[1].color = redFade;
            tier3Buttons[2].color = Color.green;
            // Disables buttons
            tier3Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier3Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier3Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier3Price);

            // Upgrade Stat
            weapon.bulletDamage += 15;

            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }
    }
    #endregion

    #region Tier 4 Upgrades
    // Upgrades the Magazine size
    public void UpgradeT4Size()
    {
        // Check if player can afford
        if (wallet.Funds >= tier4Price)
        {
            // Sets colours of buttons
            tier4Buttons[0].color = Color.green;
            tier4Buttons[1].color = redFade;
            tier4Buttons[2].color = redFade;
            // Disables buttons
            tier4Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier4Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier4Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier4Price);

            // Upgrade stat depending on weapon
            if (weapon.gameObject.name == "AssaultRifle")
            {
                weapon.clipSize += 10;
            }
            else if (weapon.gameObject.name == "Shotgun")
            {
                weapon.clipSize += 2;
            }
            else if (weapon.gameObject.name == "Pistol")
            {
                weapon.clipSize += 6;
            }

            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }
    }

    // Upgrades the Reload speed
    public void UpgradeT4Speed()
    {
        // Check if player can afford
        if (wallet.Funds >= tier4Price)
        {
            // Sets colours of buttons
            tier4Buttons[0].color = redFade;
            tier4Buttons[1].color = Color.green;
            tier4Buttons[2].color = redFade;
            // Disables buttons
            tier4Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier4Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier4Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier4Price);

            // Upgrade Stats
            weapon.reloadSpeed -= 0.5f;

            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }
    }

    // Upgrades the weapon damage
    public void UpgradeT4Damage()
    {
        // Check if player can afford
        if (wallet.Funds >= tier4Price)
        {
            // Sets colours of buttons
            tier4Buttons[0].color = redFade;
            tier4Buttons[1].color = redFade;
            tier4Buttons[2].color = Color.green;
            // Disables buttons
            tier4Buttons[0].gameObject.GetComponent<Button>().enabled = false;
            tier4Buttons[1].gameObject.GetComponent<Button>().enabled = false;
            tier4Buttons[2].gameObject.GetComponent<Button>().enabled = false;

            // Payment accepted - reduce price
            wallet.ReduceFunds(tier4Price);

            // Upgrade Stat
            weapon.bulletDamage += 15;

            // Display joke
            upgradeManager.ArmyDudeJoke();
        }
        // Dont upgrade and display message
        else
        {
            upgradeManager.ArmyDudeInsufficientFunds();
        }
    }
    #endregion
}
