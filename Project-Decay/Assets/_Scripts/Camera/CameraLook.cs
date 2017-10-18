using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour
{
    public Transform target;
    public float distanceFromTarget = 3.5f;
    public float clippingDistance = 3.5f;
    public float clippingAdjustPos = 0.5f;

    public float vertClampMin = -30.0f;
    public float vertClampMax = 90.0f;

    public Vector3 cameraPosOffset = Vector3.zero;

    private float horizontalRot = 0.0f;
    private float verticalRot = 0.0f;
    private GameObject compass;
    public bool movementEnabled = true;

    //private Crosshair my_Crosshair;
    //private Crosshair Crosshair
    //{
    //    get
    //    {
    //        if(my_Crosshair == null)
    //        {
    //            my_Crosshair = GetComponentInChildren<Crosshair>();
    //        }
    //        return my_Crosshair;
    //    }
    //}

    void Start()
    {
        compass = GameObject.Find("Compass");
    }
    void LateUpdate()
    {
        AdjustToEnvironment();

        /*
         * horizontalRot and verticalRot variables store the movemment of the mouse.
         * The function Mathf.Clamp() clamps the camera in the Y axis between a maximum and a minimum limits so the camera can't make a full vertical rotation.
         * The quaternion is a way to represent rotation in 3D space. This rotation is made acording the mouse movement using Quaternion.Euler(verticalRot, horizontalRot, 0);
         * Vector3 pos = target.position - (rot * Vector3.forward * distanceFromTarget); makes the camera rotate around the character from a determinate distance (distanceFromTarget).
         * The last two lines make the character rotate in the Y axis acording to the movement of the mouse.
         * */

        if (movementEnabled == true)
        {
            horizontalRot += Input.GetAxis("Mouse X") * 2f;

            verticalRot -= Input.GetAxis("Mouse Y") * 2f;
        }
        verticalRot = Mathf.Clamp(verticalRot, vertClampMin, vertClampMax); 

        Quaternion rot = Quaternion.Euler(verticalRot, horizontalRot, 0);
        transform.rotation = rot;

        if (target == null)
        {
            return;
        }

        Vector3 pos = target.position - (rot * Vector3.forward * distanceFromTarget);
        pos += cameraPosOffset;
        transform.position = pos;

        Vector3 targetEuler = target.eulerAngles;
        target.rotation = Quaternion.Euler(new Vector3(targetEuler.x, transform.eulerAngles.y, targetEuler.z));
        compass.SendMessage("Move", transform.eulerAngles.y);
    }

    /// <summary>
    /// The AdjustToEnvironment() function moves the camera closer to the character if there is an object between them in order to avoid going through the object.
    /// The first to lines of the code basically create a vector in the direction of the character to the camera and the next line makes the character the origin another vector.
    /// The variable dist refers to the base distance from the camera to the character.
    /// Next, the function detects the objects around the character by raycasting from it. The raycast calculates the distance of the object from the character (hit.distance) and if it is smaller
    /// than the base distance (dist) equals the last one to the hit one minus an adjustment (clippingAdjustPos) so the camera is not moved to the exact position of the object.
    /// The Mathf.Lerp() function makes the camera movement towards the character look smooth so it is still possible to see through the objects if the mouse is moved too fast. The camera movement
    /// speed can be adjusted modifying the amount multiplied by deltaTime.
    /// </summary>
    void AdjustToEnvironment()
    {
        Vector3 camForward = transform.forward;
        Vector3 InvCemForward = camForward * -1;

        Vector3 origin = transform.position + (camForward * distanceFromTarget);

        float dist = clippingDistance;

        RaycastHit hit;
        if(Physics.Raycast(origin, InvCemForward, out hit))
        {
            if (/*!hit.transform.CompareTag("PlayerObject") && */!hit.transform.CompareTag("Player"))
            {
                if (hit.distance < clippingDistance)
                {
                    dist = hit.distance - clippingAdjustPos;

                }
            }
            else
            {
                dist = clippingDistance;
            }
        }
        else
        {
            dist = clippingDistance;
        }

        distanceFromTarget = Mathf.Lerp(distanceFromTarget, dist, Time.deltaTime * 10f);

    }
}
