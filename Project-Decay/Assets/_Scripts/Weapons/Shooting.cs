using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    //Scripts will derive from this class. Shooting > AssaultRifle
    #region variables
    //Rate of fire of the weapon, can be changed in the weapons personal script
    public GameObject projectile;

    [HideInInspector]
    public Transform muzzle;
    private WeaponReloader reloader;

    Camera cam;

    float timeToShoot;
    private bool canFire = true;
    #endregion

    void Awake()
    {
        muzzle = transform.Find("Muzzle");
        
        //Instantiation point of the projectile.
        reloader = FindObjectOfType<WeaponReloader>();

        cam = FindObjectOfType<Camera>();
    }

    public void ReloadGun()
    {
        //Checks if the reloader/WeaponReloader script is available
        if (reloader == null)
        {
            print("reloader was null");
            //if not it will return
            return;
        }
        reloader.Reload();
        //calls the Reload method which will then call the executeReload method
        print("reloader was not null and Reload has been called");
    }

    public void Update()
    {
        Aim();
    }

    public void Aim()
    {
        // Gets the mouse position on the screen and converts it to raycast
        Ray aimRay = cam.ScreenPointToRay(Input.mousePosition);        

        // Makes the weapon aim at the mouse position
        Vector3 target;

        target = aimRay.GetPoint(20);
        transform.LookAt(target);
    }

    public void Fire()
    {
        if (canFire)
        {
            
            canFire = false;
            // if can fire is false the player can not shoot
            if (Time.time < timeToShoot)
            {
                return;
            }

            if (reloader != null)
                {
                    if (reloader.IsReloading)
                        return;
                    if (reloader.RoundsRemainingInClip <= 0)
                        return;
                    reloader.TakeFromClip(1);
                    //Takes ammo from the clip when the player shoots, deducts 1 per shot.
                }

                timeToShoot = Time.time + reloader.rateOfFire;
                //instantiate the projectle        
                Instantiate(projectile, muzzle.position, muzzle.rotation);
                //print("Firing! : " + Time.time);
                //Debug.Log(reloader.firingType);

           
        }

        // Automatic shooting (Held down)
        if (reloader.firingType == FiringType.Automatic)
        {
            if (Input.GetMouseButton(0))
            {
                canFire = true;
            }
        }

        // Semi Automatic shooting (Click once)
        else if (reloader.firingType == FiringType.SemiAutomatic)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canFire = true;
            }
        }
    }
}
