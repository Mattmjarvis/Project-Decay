using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // Sets the flight path and speed
    public GameObject planeFlightStart;
    public GameObject planeFlightEnd;
    CompassController compassController;

    public float speed;

    // Make the plane look at it's destination
    public Transform lookAtTarget;

    // Air Supply Variables
    public bool readyToDrop = false; // Tells plane that it is able to drop
    public bool hasDropped = false; // crate already dropped.
    public Transform dropSpawn;
    public GameObject supplyDropGO;


    // Update is called once per frame
    void Update()
    {
        // Moves the plane towards the target
        transform.position = Vector3.MoveTowards(transform.position, planeFlightEnd.transform.position, speed * Time.deltaTime);
        transform.LookAt(lookAtTarget);
    }

    // Spawns the supply crate from the back of plane
    public void supplyDrop()
    {
        if (readyToDrop)
        {
            readyToDrop = false;
            Instantiate(supplyDropGO, dropSpawn.position, Quaternion.identity);
            Debug.Log("Crate dropped!!!");
            hasDropped = true;
            compassController = FindObjectOfType<CompassController>();
            compassController.ShowCrateOnCompass();
        }
    }

    // Initiates the drop
    public void dropTheCrate()
    {
        StartCoroutine(waitToDrop());

    }

    // Waits for so many seconds until the supply crate drops.
    public IEnumerator waitToDrop()
    {
        // Chooses time to wait to drop
        int secondsToDrop = Random.Range(3, 15);
        Debug.Log("Crate will drop in: " + secondsToDrop + " seconds");
    
        yield return new WaitForSeconds(secondsToDrop); // drop after so much time
        supplyDrop();
    }
}
