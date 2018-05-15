using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteChute : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DeleteParachute());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Deletes the parachute and strings on the supply crate
    IEnumerator DeleteParachute()
    {
        yield return new WaitForSeconds(65f);
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.SetActive(false);
        }
    }
}
