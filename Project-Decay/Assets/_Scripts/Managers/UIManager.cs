using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject radiationImage;

    // Weaponbox UI Variables
    public Image weaponBoxImage;
    private Color redColour =  new Color(255, 0, 0);
    private Color normalColour = new Color(255, 255, 255);

    // Use this for initialization
    void Start ()
    {
        weaponNoAmmo();
	}

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
}
