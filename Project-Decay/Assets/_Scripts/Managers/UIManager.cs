using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject radiationImage;

    // Use this for initialization
    void Start ()
    {
	}
	
    public void turnOnRadiationSymbol()
    {        
        radiationImage.SetActive(true);       
    }

    public void turnOffRadiationSymbol()
    {
        radiationImage.SetActive(false);

    }
}
