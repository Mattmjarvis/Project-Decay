using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

    public GameObject upgradeInterface;
    private InputManager inputManger;

   
    public WeaponStats ARStats;
    public WeaponStats SGStats;
    public WeaponStats PSStats;

    // WEAPON SELECTION
    // 0 = ASSAULT RIFLE
    // 1 = SHOTGUN
    // 2 = PISTOL
    // 3 = SPECIAL
    // 4 = ARMOR
    public Image[] weaponSelection;
   
    // THESE ARE BUTTONS ONLY
    public Image[] Tier1;
    public Image[] Tier2;
    public Image[] Tier3;
    public Image[] Tier4;

    // So we know what is being upgraded..
    private Image currentlySelected;
    private Image previouslySelected;

    public Text WeaponName;
    //Colours for weaponselection
    private Color WeaponSelectedColor = new Color(255, 0, 0);
    private Color WeaponUnselectedColor = new Color(255, 255, 255);

    public void Start()
    {
        // Begin by setting weapon of menu to AR
        previouslySelected = weaponSelection[1];
        currentlySelected = weaponSelection[0];
        ClickUpgradeAR();
    }

    // Closes the upgrade interface
    public void clickExit()
    {
        upgradeInterface.SetActive(false);
        inputManger.ResumeGameplay();
    }

    // Changes to the upgrades for AR
    public void ClickUpgradeAR()
    {
        // Reset colours
        previouslySelected = currentlySelected;
        previouslySelected.color = WeaponUnselectedColor;

        // Apply new colours
        currentlySelected = weaponSelection[0];
        currentlySelected.color = WeaponSelectedColor;

        // Apply name of weapon
        WeaponName.text = "Assault Rifle";

    }

    // Changes to the upgrades for SG
    public void ClickUpgradeSG()
    {
        // Reset colours
        previouslySelected = currentlySelected;
        previouslySelected.color = WeaponUnselectedColor;

        // Apply new colours
        currentlySelected = weaponSelection[1];
        currentlySelected.color = WeaponSelectedColor;

        // Apply name of weapon
        WeaponName.text = "Shotgun";
    }

    // Changes to upgrades for PS
    public void ClickUpgradePS()
    {
        // Reset colours
        previouslySelected = currentlySelected;
        previouslySelected.color = WeaponUnselectedColor;

        // Apply new colours
        currentlySelected = weaponSelection[2];
        currentlySelected.color = WeaponSelectedColor;

        // Apply name of weapon
        WeaponName.text = "Pistol";
    }

    public void SetUpgradeBoxes()
    {
        // UPGRADE DB
            // WEAPON SELECTION
            // 0 = ASSAULT RIFLE
            // 1 = SHOTGUN
            // 2 = PISTOL
            // 3 = SPECIAL
            // 4 = ARMOR

        if(currentlySelected = weaponSelection[0])
        {

        }
    }



}
