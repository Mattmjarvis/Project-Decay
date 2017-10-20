using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    GasDeterrent gasDeterrent;
    PlayerHealth playerHealth;

    public GameObject radiationImage;

    // Use this for initialization
    void Start ()
    {
        gasDeterrent = FindObjectOfType<GasDeterrent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public void radiationSymbol()
    {        
        radiationImage.SetActive(true);
       
    }
}
