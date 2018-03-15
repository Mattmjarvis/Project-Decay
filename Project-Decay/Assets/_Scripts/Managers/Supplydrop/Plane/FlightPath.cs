using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPath : MonoBehaviour {

    // List the points of flight paths (Horizontal letters will fly to horizontal numbers - likewise with vertical. Horizontal cannot fly to vertical)
    public GameObject plane;

    public GameObject flightStart;
    public GameObject flightEnd;


    private int startIndex;
    private int endIndex;

	// Use this for initialization
	void Start () {
        EnablePlane();
        plane.GetComponent<Plane>();
	}
	
    // Enables the plane and sets start position
    public void EnablePlane()
    {
        plane.SetActive(true);
        plane.transform.position = new Vector3(flightStart.transform.position.x, flightStart.transform.position.y, flightStart.transform.position.z);
         //Debug.Log("Planes position is at: " + flightStart);
        plane.GetComponent<Plane>().planeFlightStart = flightStart;
        plane.GetComponent<Plane>().planeFlightEnd = flightEnd;
        plane.GetComponent<Plane>().lookAtTarget = flightEnd.transform;
    }
}
