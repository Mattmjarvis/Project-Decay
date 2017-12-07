using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanseZoneTrigger : MonoBehaviour {

    public GameObject cleanseZonePrefab;

    void Start()
    {
        if (cleanseZonePrefab == null)
        {
            Debug.LogError("No cleanse Zone detected");
        }
    }
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            cleanseZonePrefab.SetActive(true);
        }
    }
}
