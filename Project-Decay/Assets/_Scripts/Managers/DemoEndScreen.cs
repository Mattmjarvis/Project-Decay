using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEndScreen : MonoBehaviour {

    public GameObject endScreen;
	// Use this for initialization
	
    void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player")
        {
            endScreen.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
