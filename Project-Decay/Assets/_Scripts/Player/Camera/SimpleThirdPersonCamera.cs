using UnityEngine;
using System.Collections;

public class SimpleThirdPersonCamera : MonoBehaviour 
{
    #region Variables
    public Transform target;
    private GameObject compass;
	public Vector3 targetOffset = Vector3.zero;
	public float minimumDistance = 0.5f;
	public float rotAxisSpeed = 0f;
	public bool alignTarget = true;
	private float distanceFromTarget = 5.0f;

	public Vector3 aimOffset = Vector3.zero;


	private float horizontalRot = 0.0f;
	private float verticalRot = 0.0f;

	public enum CamState {Normal, Aim};
	public CamState cameraState = CamState.Normal;
    #endregion
    SimpleThirdPerson playerController;

    void Start () 
	{
		Vector3 angles = transform.eulerAngles;
		horizontalRot = angles.y;
		verticalRot = angles.x;
        playerController = FindObjectOfType<SimpleThirdPerson>();
        // Gets compass GO
        compass = GameObject.Find("Compass");
    }
		
	void LateUpdate () 
	{
        if (playerController.blockControl == false)
        {
            if (target)
            {
                switch (cameraState)
                {
                    case CamState.Normal:
                        UpdateNormalCamera();
                        break;

                    case CamState.Aim:
                        UpdateAimCamera();
                        break;
                }
            }

		}
        //Switch statements between both camera states

        // Updates the Compass UI
        if (compass != null && compass.GetComponent<CompassController>().compassEnabled)
        {
            compass.SendMessage("Move");
        }
    }

	private void UpdateNormalCamera()
	{
		Camera cam = gameObject.GetComponent<Camera>();
//		if(cam.fieldOfView != 60f) cam.fieldOfView = 60f;
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60f, Time.deltaTime * 5f);
        //Changes field of view of the camera and lerps it, meaning it is a smooth transition

		AdjustToObstacles();

		//Change this to left mouse down
		//if(Input.GetKey(KeyCode.LeftCommand))
		//{
		horizontalRot += Input.GetAxis("Mouse X") * 1f;
		verticalRot -= Input.GetAxis("Mouse Y") * 1f;
		verticalRot = Mathf.Clamp(verticalRot, -30f, 50f);
        //Handles mouse input and clamp
		//}

		horizontalRot += Input.GetAxis("Horizontal") * rotAxisSpeed;

		Quaternion rot = Quaternion.Euler(verticalRot, horizontalRot, 0);
		transform.rotation = rot;

		Vector3 pos = target.position - (rot * Vector3.forward * distanceFromTarget);
		pos += targetOffset;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 20);


		if(alignTarget)
		{
			Vector3 targetEuler = target.eulerAngles;
			target.rotation = Quaternion.Euler(new Vector3(targetEuler.x, transform.eulerAngles.y, targetEuler.z));
		}

//		AdjustToObstacles();
	}

    private void UpdateAimCamera()
    {
        AdjustToObstacles();

        Camera cam = gameObject.GetComponent<Camera>();
        //		if(cam.fieldOfView != 30f) cam.fieldOfView = 30f;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60f, Time.deltaTime * 5f);

        //Change this to left mouse down
        //if(Input.GetKey(KeyCode.LeftCommand))
        //{
        horizontalRot += Input.GetAxis("Mouse X") * 1f;
        verticalRot -= Input.GetAxis("Mouse Y") * 1f;
        verticalRot = Mathf.Clamp(verticalRot, -60f, 60f);
        //Handles mouse input and clamp
        //}

        horizontalRot += Input.GetAxis("Horizontal") * rotAxisSpeed;

        Quaternion rot = Quaternion.Euler(verticalRot, horizontalRot, 0);
        transform.rotation = rot;

        Vector3 pos = target.position - (rot * Vector3.forward * distanceFromTarget);
        pos += targetOffset;

        Vector3 offsetX = (target.TransformDirection(Vector3.right) * aimOffset.x);
        Vector3 offsetY = (target.TransformDirection(Vector3.up) * aimOffset.y);
        Vector3 camDestPos = pos + offsetX + offsetY;

        transform.position = Vector3.Lerp(transform.position, camDestPos, Time.deltaTime * 20);               

        Vector3 targetEuler = target.eulerAngles;
        target.rotation = Quaternion.Euler(new Vector3(targetEuler.x, transform.eulerAngles.y, targetEuler.z));

    }

    private void AdjustToObstacles()
	{
		Vector3 camForward = this.transform.forward;
		Vector3 camInvertedFwd = camForward * -1;

		Vector3 origin = transform.position + (camForward * distanceFromTarget);


		Color debugColor = Color.blue;
		float dist = 3.5f;
//		verticalRot = 18f;


		RaycastHit hit;
		if(Physics.Raycast(origin, camInvertedFwd, out hit))
		{
			debugColor = Color.red;

			float currentDist = Vector3.Distance(origin, hit.point);
			if(hit.distance < 3.5f)
			{
				dist = hit.distance - 0.5f;

				if(dist < minimumDistance)
					dist = 0.0f;
			}
		}
		else
		{
			dist = 3.5f;
//			verticalRot = 18f;
		}



		distanceFromTarget = Mathf.Lerp(distanceFromTarget, dist, Time.deltaTime * 10f);

		Vector3 camBack = transform.position + camInvertedFwd * distanceFromTarget;
		Debug.DrawLine(origin, camBack, debugColor);

        //Sending a raycast behind the player, if the raycast hits a collider that is closer than the distanceFromTarget variable,
        //it will put the camera forward, making sure it does not go through the object.
	}


}
