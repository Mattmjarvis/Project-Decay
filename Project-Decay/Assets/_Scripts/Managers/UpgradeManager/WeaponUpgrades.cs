using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrades : MonoBehaviour {

    public WeaponStats weapon;

    public Image[] tier1Buttons;
    public Image[] tier2Buttons;
    public Image[] tier3Buttons;
    public Image[] tier4Buttons;

    Color redFade = new Color(255, 0, 0);
    // Use this for initialization
    void Start () {
		
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
        // Sets colours of buttons
        tier1Buttons[0].color = Color.green;
        tier1Buttons[1].color = redFade;
        tier1Buttons[2].color = redFade;
        // Disables buttons
        tier1Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier1Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier1Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }

    // Upgrades the Reload speed
    public void UpgradeT1Speed()
    {
        // Sets colours of buttons
        tier1Buttons[0].color = redFade;
        tier1Buttons[1].color = Color.green;
        tier1Buttons[2].color = redFade;
        // Disables buttons
        tier1Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier1Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier1Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }

    // Upgrades the weapon damage
    public void UpgradeT1Damage()
    {
        // Sets colours of buttons
        tier1Buttons[0].color = redFade;
        tier1Buttons[1].color = redFade;
        tier1Buttons[2].color = Color.green;
        // Disables buttons
        tier1Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier1Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier1Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }
    #endregion

    #region Tier 2 Upgrades
    // Upgrades the Magazine size
    public void UpgradeT2Size()
    {
        // Sets colours of buttons
        tier2Buttons[0].color = Color.green;
        tier2Buttons[1].color = redFade;
        tier2Buttons[2].color = redFade;
        // Disables buttons
        tier2Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier2Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier2Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }

    // Upgrades the Reload speed
    public void UpgradeT2Speed()
    {
        // Sets colours of buttons
        tier2Buttons[0].color = redFade;
        tier2Buttons[1].color = Color.green;
        tier2Buttons[2].color = redFade;
        // Disables buttons
        tier2Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier2Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier2Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }

    // Upgrades the weapon damage
    public void UpgradeT2Damage()
    {
        // Sets colours of buttons
        tier2Buttons[0].color = redFade;
        tier2Buttons[1].color = redFade;
        tier2Buttons[2].color = Color.green;
        // Disables buttons
        tier2Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier2Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier2Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }
    #endregion

    #region Tier 3 Upgrades
    // Upgrades the Magazine size
    public void UpgradeT3Size()
    {
        // Sets colours of buttons
        tier3Buttons[0].color = Color.green;
        tier3Buttons[1].color = redFade;
        tier3Buttons[2].color = redFade;
        // Disables buttons
        tier3Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier3Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier3Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }

    // Upgrades the Reload speed
    public void UpgradeT3Speed()
    {
        // Sets colours of buttons
        tier3Buttons[0].color = redFade;
        tier3Buttons[1].color = Color.green;
        tier3Buttons[2].color = redFade;
        // Disables buttons
        tier3Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier3Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier3Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }

    // Upgrades the weapon damage
    public void UpgradeT3Damage()
    {
        // Sets colours of buttons
        tier3Buttons[0].color = redFade;
        tier3Buttons[1].color = redFade;
        tier3Buttons[2].color = Color.green;
        // Disables buttons
        tier3Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier3Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier3Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }
    #endregion

    #region Tier 4 Upgrades
    // Upgrades the Magazine size
    public void UpgradeT4Size()
    {
        // Sets colours of buttons
        tier4Buttons[0].color = Color.green;
        tier4Buttons[1].color = redFade;
        tier4Buttons[2].color = redFade;
        // Disables buttons
        tier4Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier4Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier4Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }

    // Upgrades the Reload speed
    public void UpgradeT4Speed()
    {
        // Sets colours of buttons
        tier4Buttons[0].color = redFade;
        tier4Buttons[1].color = Color.green;
        tier4Buttons[2].color = redFade;
        // Disables buttons
        tier4Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier4Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier4Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }

    // Upgrades the weapon damage
    public void UpgradeT4Damage()
    {
        // Sets colours of buttons
        tier4Buttons[0].color = redFade;
        tier4Buttons[1].color = redFade;
        tier4Buttons[2].color = Color.green;
        // Disables buttons
        tier4Buttons[0].gameObject.GetComponent<Button>().enabled = false;
        tier4Buttons[1].gameObject.GetComponent<Button>().enabled = false;
        tier4Buttons[2].gameObject.GetComponent<Button>().enabled = false;
    }
    #endregion
}
