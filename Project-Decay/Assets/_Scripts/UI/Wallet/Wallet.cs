using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wallet : MonoBehaviour {

    private int funds;

    UIManager uiManager;
    // Sets properties for funds
    public int Funds
    {
        get
        {
            return funds;
        }
        set
        {
            funds = value;
        }
    }

	// Use this for initialization
	void Start () {
        uiManager = FindObjectOfType<UIManager>(); //Gets UI Manager to update textbox
        IncreaseFunds(12);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Reduce the amount of funds in the player wallet
    public void ReduceFunds(int amount)
    {
        // Checks to see if player can afford funds
        if (amount > funds)
        {
            return;
        }
        else
        {
            funds -= amount;
            uiManager.UpdateWalletTextBox();
        }
    }

    // Increases amount of funds in the player wallet
    public void IncreaseFunds(int amount)
    {
        funds += amount;
        uiManager.UpdateWalletTextBox();
    }
}
