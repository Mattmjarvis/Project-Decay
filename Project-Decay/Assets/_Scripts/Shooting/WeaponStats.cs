using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FiringType
{
    Automatic,
    SemiAutomatic
}

public class WeaponStats : MonoBehaviour {

    public int clipSize;
    public int maxAmmo;    
    public float ReloadSpeed;
    public int AmmoInClip;
    public float rateOfFire;
    public FiringType firingType;
    public Sprite weaponBox_Unselected;
    public Sprite weaponBox_Selected;
    public Sprite weaponIcon_Unselected;
    public Sprite weaponIcon_Selected;




	// Use this for initialization
	void Start ()
    {
		        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
