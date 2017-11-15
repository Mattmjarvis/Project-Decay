using UnityEngine;
using System.Collections;

public class SimpleThirdPerson : MonoBehaviour 
{
    #region Variables
    public Camera cam;
	public Transform camera;
	private Animator animator;
	private float speed;
    //Controls speed of player movement

	public GameObject gun;
    //Sets the current gun
	public bool ikActive = false;
	public Transform handleRight;
	public Transform handleLeft;
    //Set positions for hands to animate to when holding guns

	private bool gunActive = false;
    //Checks if gun is active
	GameObject lookAtGO;
	public GameObject bulletPr;
    //bullet prefab

	private Vector3 hitPoint = Vector3.zero;
	private enum WeaponType {Gun, Grenade};
	private WeaponType currentWeaponType = WeaponType.Gun;
    //Changes firing type

	public GameObject crosshair;
    #endregion

    public void Start()
	{
		animator = GetComponent<Animator> ();

		gun.SetActive(false);
		gunActive = false;
		crosshair.SetActive(false);

		lookAtGO = new GameObject();
		lookAtGO.transform.name = "lookAt";

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	public void Update()
	{
		Quaternion camRot = Quaternion.Euler (new Vector3(transform.eulerAngles.x,
			camera.eulerAngles.y,
			transform.eulerAngles.z));
        //Handles rotation of camera

		//Vector3 relPos = camRot * Vector3.forward;

		//float angle = Vector3.Angle (transform.forward, relPos);
		//int dir = AngleDir(transform.forward, relPos, Vector3.up);

		float maxSpeed = 3.5f;
		if(Input.GetKey(KeyCode.LeftShift)) maxSpeed = 8.0f;
        //Shift to sprint

		speed = maxSpeed;

		float inputVert = Input.GetAxis ("Vertical");
		float vel = inputVert * speed;

//		float rot;
//		rot = (vel > 0)? angle * dir : 0.0f;

		animator.SetFloat ("Speed", vel, 0.25f, Time.deltaTime);

		Dive();

		//this.gameObject.GetComponent<CharacterController>().SimpleMove(transform.TransformDirection(Vector3.forward) * vel);

		if(inputVert > 0)
			transform.rotation = Quaternion.Slerp (transform.rotation, camRot, Time.deltaTime * 5.0f);

        if (Input.GetMouseButtonDown(1))
		{
            if (!gunActive)
            {
                GameObject.Find("Main Camera").GetComponent<SimpleThirdPersonCamera>().cameraState = SimpleThirdPersonCamera.CamState.Aim;
                gun.SetActive(true);
                gunActive = true;
                crosshair.SetActive(true);
                //Switches to aiming gun state
            }

            else
            {
                GameObject.Find("Main Camera").GetComponent<SimpleThirdPersonCamera>().cameraState = SimpleThirdPersonCamera.CamState.Normal;
                gun.SetActive(false);
                gunActive = false;
                crosshair.SetActive(false);
                //switches to normal camerastate
            }
        }

        if (gunActive)
		{
			AimWeapon();
		}

		if(Input.GetMouseButtonDown(0) && gunActive)
		{
			FireWeapon();
		}
	}

	private void FireWeapon()
	{
		if(currentWeaponType == WeaponType.Gun)
		{
			if(hitPoint != Vector3.zero)
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

	private void AimWeapon()
	{
		Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        //converts the cameras viewport to worldspace
		Vector3 lookAtVector;
		Quaternion camRot;

		lookAtGO.transform.position = ray.GetPoint(100f);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 100f))
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

			lookAtVector = ray.GetPoint(100f) - ray.origin;
			camRot = Quaternion.LookRotation(lookAtVector);
			gun.transform.rotation = camRot;
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

}

