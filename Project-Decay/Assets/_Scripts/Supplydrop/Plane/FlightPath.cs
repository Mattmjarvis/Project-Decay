using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPath : MonoBehaviour {

    // List the points of flight paths (Horizontal letters will fly to horizontal numbers - likewise with vertical. Horizontal cannot fly to vertical)
    public GameObject plane;

    public GameObject[] horizontalLetters;
    public GameObject[] horizontalNumbers;
    public GameObject[] verticalLetters;
    public GameObject[] verticalNumbers;

    public GameObject flightStart;
    public GameObject flightEnd;


    private int startIndex;
    private int endIndex;

	// Use this for initialization
	void Start () {
        plane.GetComponent<Plane>();
        FlyPoints();
	}
	


    // Generates whether to fly vertical or horizontal. 
    private bool GenerateDirection()  // True = Horizontal Flight Path, False = Vertical Flight Path.
    {
        // Gets random value to decide direction of flight.

        float flightDirection = Random.value;

        if(flightDirection <= 0.4f)
        {
            // Vertical
            return false;
        }
        else
        {
            // Horizontal
            return true;
        }
    }

    // Generates points to fly between.
    public void FlyPoints()
    {
        float flipSpawnSides = Random.value; // Generates value to determine whether it will flip the sides of the spawn

        // Gets the direction of flight and generates the path
        if(GenerateDirection())        // Generates horizontal flight path
        {
            // Starts plane on West side
            if (flipSpawnSides <= 0.4f)
            {
                startIndex = Random.Range(0, horizontalLetters.Length);
                endIndex = Random.Range(0, horizontalNumbers.Length);
                flightStart = horizontalLetters[startIndex];
                flightEnd = horizontalNumbers[endIndex];
                Debug.Log("The flight path is between: " + flightStart + "and " + flightEnd);
                EnablePlane();
            }
            // Starts plane on East Side
            else
            {
                startIndex = Random.Range(0, horizontalNumbers.Length);
                endIndex = Random.Range(0, horizontalLetters.Length);
                flightStart = horizontalNumbers[startIndex];
                flightEnd = horizontalLetters[endIndex];
                Debug.Log("The flight path is between: " + flightStart + "and " + flightEnd);
                EnablePlane();
            }
        }
        else  // Generates a vertical flight path
        {
            // Starts plane on North Side
            if (flipSpawnSides <= 0.4f)
            {
                startIndex = Random.Range(0, verticalLetters.Length);
                endIndex = Random.Range(0, verticalNumbers.Length);
                flightStart = verticalLetters[startIndex];
                flightEnd = verticalNumbers[endIndex];
                //Debug.Log("The flight path is between: " + flightStart + "and " + flightEnd);
                EnablePlane();
            }
            // Starts plane on South side
            else
            {
                startIndex = Random.Range(0, verticalNumbers.Length);
                endIndex = Random.Range(0, verticalLetters.Length);
                flightStart = verticalNumbers[startIndex];
                flightEnd = verticalLetters[endIndex];
                //Debug.Log("The flight path is between: " + flightStart + "and " + flightEnd);
                EnablePlane();
            }
        }

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
