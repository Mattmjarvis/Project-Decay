using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropTriggerStart : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        // Allows the plane to drop
        if (other.CompareTag("Plane"))
        {
            // If the crate has already dropped then return
            if (other.GetComponent<Plane>().hasDropped == true || other.GetComponent<Plane>().readyToDrop == true)
            {
                return;
            }

            // Continue if crate hasn't dropped
            else
            {
                other.GetComponent<Plane>().readyToDrop = true;
                //Debug.Log("Ready to drop? : " + other.GetComponent<Plane>().readyToDrop);
                other.GetComponent<Plane>().dropTheCrate();
            }
        }
    }

    // Forces crate to drop if it reaches the end without dropping
    private void OnTriggerExit(Collider other)
    {
        // If the plane reaches this point without dropping supply crate then force the drop.
        if (other.CompareTag("Plane"))
        {
            // Returns if the plane has already dropped / not ready to drop
            if (other.GetComponent<Plane>().hasDropped == true || other.GetComponent<Plane>().readyToDrop == false)
            {
                return;
            }

            // Force supply to drop
            else
            {
                //Debug.Log("Force Drop");
                other.GetComponent<Plane>().supplyDrop();

            }
        }
    }
}
