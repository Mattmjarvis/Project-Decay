using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropTriggerStart : MonoBehaviour
{

    // The plane will drop the crate when it enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Plane>().supplyDrop();
    }
}
