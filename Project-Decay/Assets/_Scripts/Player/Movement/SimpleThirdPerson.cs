﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleThirdPerson : MonoBehaviour 
{
    #region Variables
    public Camera cam;
	public Transform camera;
	private Animator animator;
	private float speed;
    //Controls speed of player movement

    // Weapon Variables
    public GameObject gun; // the weapon gameObject
    public WeaponStats weaponStats;
    private bool canFire = true;  
    
	public bool ikActive = false;

    //Set positions for hands to animate to when holding guns
    public Transform handleRight;
	public Transform handleLeft;

    //Checks if gun is active
    public bool gunActive = false;

    //Sound effects
    private AudioClip gunshotAudio;
    [HideInInspector]
    public AudioSource playerAudio;

    GameObject lookAtGO;
	public GameObject bulletPr;
    public GameObject bulletPrMetal;
    //bullet prefab

    public bool blockControl;

	private Vector3 hitPoint = Vector3.zero;
	private enum WeaponType {Gun, Grenade};
	private WeaponType currentWeaponType = WeaponType.Gun;
    public float timeToShoot = 0.5f;
    //Changes firing type

    // Get components
    UIManager uiManager;
    WeaponReloader reloader;
    ChangeWeapon changeWeapon;
    #endregion

    public void Start()
	{
        playerAudio = GetComponent<AudioSource>();
        uiManager = FindObjectOfType<UIManager>();
        reloader = FindObjectOfType<WeaponReloader>();
		animator = GetComponent<Animator> ();
        changeWeapon = FindObjectOfType<ChangeWeapon>();

		gun.SetActive(false);
		gunActive = false;


		lookAtGO = new GameObject();
		lookAtGO.transform.name = "lookAt";

        blockControl = false;
    }

    public void Update()
	{
        Move();
        //Strafe();
        WalkBackwards();

        if (gunActive)
		{
			AimWeapon();
		}

        // If the weapon is automatic then player can hold down MB1(leftclick) to shoot bullets
        if(Input.GetMouseButton(0) && gunActive && weaponStats.firingType == FiringType.Automatic) 
        {                        
            // Stop bullets from shooting all at once
            if(Time.time < timeToShoot)
            {
                return;
            }
            // Fire 
            else
            {
                FireWeapon();                
            }
            // Debug checks that weapon is shooting at correct rate of fire
            //Debug.Log(Time.time);
            //Debug.Log("Before: " + timeToShoot);

            // Set time to shoot next bullet
            timeToShoot = Time.time + 0.1f;

            // Debug checks that weapon is shooting at correct rate of fire
            //Debug.Log(Time.time);
            //Debug.Log("After: "+ timeToShoot);

        }

        // If weapon is semi automatic then one click = fire once
        if (Input.GetMouseButtonDown(0) && gunActive && weaponStats.firingType == FiringType.SemiAutomatic)
		{
            // Fire weapon
			FireWeapon();
		}
        
       
        switchCameraStates(); // Set camera state
        ReloadPressed(); //Reload
    }

    #region Camera
    private void switchCameraStates()
    {
        //Switches to aiming gun state
        if (Input.GetMouseButtonDown(1)) // (Rightclick)
        {
            // If the player does not have a weapon active and a weapon is available then switch weapon
            if (!gunActive && weaponStats.weaponAvailable == true)
            {
                GameObject.Find("Main Camera").GetComponent<SimpleThirdPersonCamera>().cameraState = SimpleThirdPersonCamera.CamState.Aim;
                gun.SetActive(true);
                gunActive = true;
                uiManager.turnOnCrosshair();


                // Sets weapon selected icon to on
                changeWeapon.weaponBoxImage[changeWeapon.currentWeapon].gameObject.GetComponent<Image>().sprite = changeWeapon.weapons[changeWeapon.currentWeapon].GetComponentInChildren<WeaponStats>().weaponBox_Selected;
                changeWeapon.weaponIcon[changeWeapon.currentWeapon].gameObject.GetComponent<Image>().sprite = changeWeapon.weapons[changeWeapon.currentWeapon].GetComponentInChildren<WeaponStats>().weaponIcon_Selected;
            }

            //switches to normal camerastate
            else
            {
                GameObject.Find("Main Camera").GetComponent<SimpleThirdPersonCamera>().cameraState = SimpleThirdPersonCamera.CamState.Normal;
                gun.SetActive(false);
                gunActive = false;
                reloader.StopReload();

                // Disables UI elements
                uiManager.turnOffCrosshair();
                uiManager.TurnOffReloadProgressBar();

                // Sets weapon selected icon to off
                changeWeapon.weaponBoxImage[changeWeapon.currentWeapon].gameObject.GetComponent<Image>().sprite = changeWeapon.weapons[changeWeapon.currentWeapon].GetComponentInChildren<WeaponStats>().weaponBox_Unselected;
                changeWeapon.weaponIcon[changeWeapon.currentWeapon].gameObject.GetComponent<Image>().sprite = changeWeapon.weapons[changeWeapon.currentWeapon].GetComponentInChildren<WeaponStats>().weaponIcon_Unselected;

            }
        }
    }
    #endregion

    #region Movement
    private void Move()
    {
        Quaternion camRot = Quaternion.Euler(new Vector3(transform.eulerAngles.x,
            camera.eulerAngles.y,
            transform.eulerAngles.z));
        //Handles rotation of camera

        float maxSpeed = 3.5f;
        if (Input.GetKey(KeyCode.LeftShift)) maxSpeed = 8.0f;
        //Shift to sprint

        speed = maxSpeed;

        float inputVert = Input.GetAxis("Vertical");

        float velocity = inputVert * speed;      

        //Animation controls walking forward.        
        animator.SetFloat("Speed", velocity, 0.25f, Time.deltaTime);   

        //Vertical input controls the rotation of the player when A or D is held
        //if (inputVert > 0)
        //    transform.rotation = Quaternion.Slerp(transform.rotation, camRot, Time.deltaTime * 5.0f);      

        Dive();
    }

    //private void Strafe()
    //{
    //    if ((Input.GetAxis("Strafe")) > 0)
    //    {
    //        this.gameObject.GetComponent<CharacterController>().SimpleMove(transform.TransformDirection(Vector3.right) * Input.GetAxis("Strafe") * 4);
    //    }
    //    else if ((Input.GetAxis("Strafe")) < 0)
    //    {
    //        this.gameObject.GetComponent<CharacterController>().SimpleMove(transform.TransformDirection(Vector3.right) * Input.GetAxis("Strafe") * 4);
    //    }
    //}

    private void WalkBackwards()
    {
        if ((Input.GetAxis("WalkBackwards")) > 0)
        {
            this.gameObject.GetComponent<CharacterController>().SimpleMove(transform.TransformDirection(Vector3.back) * Input.GetAxis("WalkBackwards") * 4);
        }
    }

    private void Dive()
	{
		if(animator.GetBool("Dive"))
		{
			animator.SetBool("Dive", false);
		}

		if(Input.GetKeyDown(KeyCode.Space) && speed == 8.0f)
		{
			animator.SetBool("Dive", true);
		}
        //Diving animation
	}
    #endregion

    #region Shooting, Aiming, Reloading
    private void FireWeapon()
	{
        weaponStats = reloader.currentWeapon;      // Gets the stats when player shoots (Changed this to apply when change weapon has been reimplemented)
  
        // Stops player from shooting if no ammo
        if (weaponStats.ammoInClip == 0 || canFire == false)
        {
            playerAudio.PlayOneShot(weaponStats.noAmmoSound, 1f);
            return;
        }
        
        if(reloader != null)
        {
            if(reloader.IsReloading)
            {
                return;
            }
            if (reloader.RoundsRemainingInClip <= 0)
            {
                return;
            }
            //Takes ammo from the clip when the player shoots, deducts 1 per shot.
        }

        if (currentWeaponType == WeaponType.Gun)
		{
            reloader.TakeFromClip(1);
            playerAudio.PlayOneShot(weaponStats.shotSound, 1f);

            if (hitPoint != Vector3.zero)
			{                
				GameObject bullet = (GameObject)Instantiate(bulletPr, hitPoint, gun.transform.rotation);
                //instantiates the bullet prefab wherever the raycast is interupted (the hitPoint)                
			}
		}
		else if(currentWeaponType == WeaponType.Grenade)
		{
			GameObject bullet = (GameObject)Instantiate(bulletPr, gun.transform.position, gun.transform.rotation);
			bullet.GetComponent<Rigidbody>().velocity = gun.transform.forward * 100f;
            //Instantiates the bullet prefab and add velocity to it to send it through the world along the raycast to the hitPoint.
		}


    }

    public void ReloadPressed()
    {
        if (Input.GetKeyDown(KeyCode.R) && gunActive)
        {
            //Debug.Log("Reload Pressed");
            reloader.ReloadCheck();
        }
    }

    private void AimWeapon()
	{
        // Checks if the player is out of ammo. True/false enables/disables out of ammo text
        if (weaponStats.ammoInClip == 0)
        {
            uiManager.EnableOutOfAmmoUI();
        }
        else
        {
            uiManager.DisableOutOfAmmoUI();
        }

        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        //converts the cameras viewport to worldspace
		Vector3 lookAtVector;
		Quaternion camRot;

		lookAtGO.transform.position = ray.GetPoint(50f);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 50f))
		{
			Debug.DrawLine(gun.transform.position, hit.point, Color.red);
            //Creates a raycast and visualises it

			hitPoint = hit.point;
            //sets hitpoint to the point in the world space where the ray has hit a collider

			lookAtVector = hit.point - gun.transform.position;
			camRot = Quaternion.LookRotation(lookAtVector);
			gun.transform.rotation = camRot;
            //The ray is sent from the camera, when the ray interacts with a collider it finds the position of the gun and sends a bullet to meet the hitPoint along the raycast.
		}
		else
		{
			hitPoint = Vector3.zero;

			lookAtVector = ray.GetPoint(50f) - ray.origin;
			camRot = Quaternion.LookRotation(lookAtVector);
			gun.transform.rotation = camRot;
		}
	}
    #endregion

    #region IKAnimations
    public void OnAnimatorIK()
	{
		if(!gunActive)
			return;

		if(animator)
		{
			if(ikActive)
			{
				if(handleRight != null)
				{
//					animator.SetLookAtWeight(1.0f, 0.5f, 0.5f);
//					animator.SetLookAtPosition(lookAtGO.transform.position);

					animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
					animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
					animator.SetIKPosition(AvatarIKGoal.RightHand,handleRight.position);
					animator.SetIKRotation(AvatarIKGoal.RightHand,handleRight.rotation);
				}

				if(handleLeft != null)
				{
					animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
					animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);  
					animator.SetIKPosition(AvatarIKGoal.LeftHand,handleLeft.position);
					animator.SetIKRotation(AvatarIKGoal.LeftHand,handleLeft.rotation);
				}
			}
			else
			{
//				animator.SetLookAtWeight(0);

				animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,0); 

				animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
				animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
				animator.SetLookAtWeight(0);
			}
            //Controls AnimatorIK which moves the gun and hands of the character in a realistic way.
		}
	}

	//returns -1 when to the left, 1 to the right, and 0 for forward/backward
	int AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) 
	{
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);

		if (dir > 0) {
			return 1;
		} else if (dir < 0) {
			return -1;
		} else {
			return 0;
		}
	}
    #endregion

}

