using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject objective;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(objectivePopUp());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator objectivePopUp()
    {
        objective.SetActive(true);
        yield return new WaitForSeconds(5);
        objective.SetActive(false);
    }


}
