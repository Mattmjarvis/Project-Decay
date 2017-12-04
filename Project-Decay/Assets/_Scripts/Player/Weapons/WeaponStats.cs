using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Allows to set the weapon to either automatic or semiautomatic
public enum FiringType
{
    Automatic,
    SemiAutomatic
}

// This class contains all the stats for each weapon so we can access weapon variables directly
public class WeaponStats : MonoBehaviour {

    public bool infiniteAmmo;

   
    public int weaponID;
    public int bulletDamage;
    public int clipSize;
    public int maxAmmo;    
    public float reloadSpeed;
    public int ammoInClip;
    public float rateOfFire;
    public FiringType firingType;
    public AudioClip shotSound;
    public AudioClip noAmmoSound;
    public Sprite weaponBox_Unselected;
    public Sprite weaponBox_Selected;
    public Sprite weaponIcon_Unselected;
    public Sprite weaponIcon_Selected;
    public Transform handleRight;
    public Transform handleLeft;
 }
