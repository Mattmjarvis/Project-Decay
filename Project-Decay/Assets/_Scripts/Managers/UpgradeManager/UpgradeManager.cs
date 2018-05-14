using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

    public GameObject upgradeInterface;
    public GameObject[] upgradeMenus;
    FlightPath Supplycrate;
    private InputManager inputManger;


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

    // Text properties
    public Text WeaponName;
    public Text ArmyDudeText;
    int timesClicked = 0;

    //Colours for weaponselection
    private Color WeaponSelectedColor = new Color(255, 0, 0);
    private Color WeaponUnselectedColor = new Color(255, 255, 255);

    public void Start()
    {
        // Begin by setting weapon of menu to AR
        previouslySelected = weaponSelection[1];
        currentlySelected = weaponSelection[0];
        inputManger = FindObjectOfType<InputManager>();
        Supplycrate = FindObjectOfType<FlightPath>();

        ClickUpgradeAR();
    }

    // Closes the upgrade interface
    public void clickExit()
    {
        upgradeInterface.SetActive(false);
        inputManger.ResumeGameplay();
        Supplycrate.EnablePlane();
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

        // Enables the correct upgrade menu
        for (int i = 0; i < upgradeMenus.Length; i++)
        {
            if (i == 0)
            {
                upgradeMenus[i].SetActive(true);
            }
            else
            {
                upgradeMenus[i].SetActive(false);
            }
        }
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

        // Enables the correct upgrade menu
        for (int i = 0; i < upgradeMenus.Length; i++)
        {
            if (i == 1)
            {
                upgradeMenus[i].SetActive(true);
            }
            else
            {
                upgradeMenus[i].SetActive(false);
            }
        }
    }

    // Changes to the upgrades for PS
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

        // Enables the correct upgrade menu
        for (int i = 0; i < upgradeMenus.Length; i++)
        {
            if (i == 2)
            {
                upgradeMenus[i].SetActive(true);
            }
            else
            {
                upgradeMenus[i].SetActive(false);
            }
        }
    }

    // Changes the speecbubble of the army guy when the bubble is clicked
    public void ClickArmyDudeSpeech()
    {
        timesClicked++;

        if (timesClicked == 1)
        {
            ArmyDudeText.text = "Welcome to the Upgrade O Matic, Soldier!";
        }

        else if (timesClicked == 2)
        {
            ArmyDudeText.text = "Pick a weapon, then pick an Upgrade!";
        }

        else if (timesClicked == 3)
        {
            ArmyDudeText.text = "Tip: There's only one upgrade per tier!";
        }

        else if (timesClicked == 4)
        {
            ArmyDudeText.text = "Well? What are you waitin' for?";
        }

        else if (timesClicked == 5)
        {
            ArmyDudeText.text = "And by the way.. NO REFUNDS!!!";
        }
    }

    // Makes the Army man say some witty comments after a purchase
    public void ArmyDudeJoke()
    {
        // List of quotes to choose from
        string previousJoke;
        string[] quotes = {"You call that a weapon?",
            "My grandma can shoot better than you!",
            "Good purchase....NOT!",
            "When in doubt, empty the magazine.",
            "Cough it up, Maggot!",
            "Upgrade? More like downgrade!",
            "If you miss... Pretend it's the target"
            };

        // Choose a random quote
        previousJoke = ArmyDudeText.text;
        ArmyDudeText.text = quotes[Random.Range(1, quotes.Length)];
        while (ArmyDudeText.text == previousJoke)
        {
            ArmyDudeText.text = quotes[Random.Range(1, quotes.Length)];
        }

    }

    // If the player can't afford then display message
    public void ArmyDudeInsufficientFunds()
    {
        Debug.Log("insufficient funds");
        ArmyDudeText.text = "Well.. Looks like you're all out of upgrades!";
    }
}
