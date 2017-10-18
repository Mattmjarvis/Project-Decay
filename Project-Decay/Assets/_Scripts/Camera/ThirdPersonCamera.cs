using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public float distanceFromTarget = 3.5f;
    public float clippingDistance = 3.5f;
    public float clippingAdjustPos = 0.5f;

    [SerializeField]
    Vector3 cameraOffset;
    [SerializeField]
    float damping;

    public Transform cameraLookTarget;
    //stores a game object under Player within the variable cameraLookTarget

    PlayerController localPlayer;
    Vector3 targetPosition;

    private GameObject compass;

    // Use this for initialization
    void Awake ()
    {
        localPlayer = FindObjectOfType<PlayerController>();        
        compass = GameObject.Find("Compass");
    }

    // Update is called once per frame
    void Update ()
    {
        if(cameraLookTarget == null)
        {
            //if the player is dead and the cameras target is not available it will return and not execute the code.
            return;
        }
        //sets the target to the position of the cameraLookTarget, transforms forward of the player and offsets it behind the player using the x axis.
        targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z +
            localPlayer.transform.up * cameraOffset.y +
            localPlayer.transform.right * cameraOffset.x;

        Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);

        transform.position = Vector3.Lerp(transform.position, targetPosition, damping + Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * damping * Time.deltaTime);
    }

    void LateUpdate()
    {
        AdjustToEnvironment();

        if (compass != null)
        {
            compass.SendMessage("Move");
        }
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
        if (Physics.Raycast(origin, InvCemForward, out hit))
        {
            if (!hit.transform.CompareTag("Player"))
            {
                if (hit.distance < clippingDistance)
                {
                    print("something hit");
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
