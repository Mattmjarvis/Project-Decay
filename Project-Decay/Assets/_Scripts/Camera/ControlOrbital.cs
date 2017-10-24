
using UnityEngine;
using System.Collections;

public class ControlOrbital : MonoBehaviour {

    private float vertical;
    private float turningSpeed = 4.0f;

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
        vertical = Mathf.Clamp(vertical, -30, 60);
        transform.localRotation = Quaternion.AngleAxis(vertical, Vector3.right);
    }

    void LateUpdate()
    {
        if (compass != null)
        {
            compass.SendMessage("Move");
        }
    }
}
