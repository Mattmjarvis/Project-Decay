
using UnityEngine;
using System.Collections;

public class ControlOrbital : MonoBehaviour {

    private float vertical;
    private float turningSpeed = 2.0f;

    public float distanceFromTarget = 3.5f;
    public float clippingDistance = 3.5f;
    public float clippingAdjustPos = 0.5f;

    private GameObject compass;

    void Start ()
    {
        vertical = transform.eulerAngles.x;
        compass = GameObject.Find("Compass");
    }

    void Update ()
    {
        var mouseVertical = Input.GetAxis("Mouse Y");
        vertical = (vertical - turningSpeed * mouseVertical) % 360f;
        vertical = Mathf.Clamp(vertical, -10, 40);
        transform.localRotation = Quaternion.AngleAxis(vertical, Vector3.right);
    }

    void LateUpdate()
    {
        AdjustToEnvironment();

        if (compass != null)
        {
            compass.SendMessage("Move");
        }
    }

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
