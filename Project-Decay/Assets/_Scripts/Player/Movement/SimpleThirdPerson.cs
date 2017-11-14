using UnityEngine;
using System.Collections;

public class SimpleThirdPerson : MonoBehaviour 
{
    public Camera cam;
	public Transform camera;
	private Animator animator;
	private float speed;

	public GameObject gun;
	public bool ikActive = false;
	public Transform handleRight;
	public Transform handleLeft;

	private bool gunActive = false;
	GameObject lookAtGO;
	public GameObject bulletPr;

	private Vector3 hitPoint = Vector3.zero;
	private enum Weapon {Firearm, Throwing};
	private Weapon currentWeapon = Weapon.Firearm;

	public GameObject crosshair;

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
		Vector3 relPos = camRot * Vector3.forward;

		float angle = Vector3.Angle (transform.forward, relPos);
		int dir = AngleDir(transform.forward, relPos, Vector3.up);

		float maxSpeed = 3.5f;
		if(Input.GetKey(KeyCode.LeftShift)) maxSpeed = 8.0f;

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

		if(Input.GetKeyDown(KeyCode.E))
		{
			if(!gunActive)
			{
				GameObject.Find("Main Camera").GetComponent<SimpleThirdPersonCamera>().cameraState = SimpleThirdPersonCamera.CamState.Aim;
				gun.SetActive(true);
				gunActive = true;
				crosshair.SetActive(true);
			}
			else
			{
				GameObject.Find("Main Camera").GetComponent<SimpleThirdPersonCamera>().cameraState = SimpleThirdPersonCamera.CamState.Normal;
				gun.SetActive(false);
				gunActive = false;
				crosshair.SetActive(false);
			}
		}

		if(gunActive)
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
		if(currentWeapon == Weapon.Firearm)
		{
			if(hitPoint != Vector3.zero)
			{
				GameObject bullet = (GameObject)Instantiate(bulletPr, hitPoint, gun.transform.rotation);
			}
		}
		else if(currentWeapon == Weapon.Throwing)
		{
			GameObject bullet = (GameObject)Instantiate(bulletPr, gun.transform.position, gun.transform.rotation);
			bullet.GetComponent<Rigidbody>().velocity = gun.transform.forward * 100f;
		}
	}

	private void AimWeapon()
	{
		Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
		Vector3 lookAtVector;
		Quaternion camRot;

		lookAtGO.transform.position = ray.GetPoint(100f);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 100f))
		{
			Debug.DrawLine(gun.transform.position, hit.point, Color.red);

			hitPoint = hit.point;

			lookAtVector = hit.point - gun.transform.position;
			camRot = Quaternion.LookRotation(lookAtVector);
			gun.transform.rotation = camRot;
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

