using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour {

    public GameObject weather;
    public GameObject dayLights;
    public GameObject nightLights;   
    
    void Start()
    {
        //weather = GameObject.FindGameObjectWithTag("Weather");
        //nightLights = GameObject.FindGameObjectWithTag("NightLights");
        //dayLights = GameObject.FindGameObjectWithTag("DayLights");
    }

    void activateStormSystem()
    {
        weather.SetActive(true);
        nightLights.SetActive(true);
        dayLights.SetActive(false);       
    }

    void disableStormSystem()
    {
        dayLights.SetActive(true);
        weather.SetActive(false);
        nightLights.SetActive(false);
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        activateStormSystem();
    //    }

    //    if (Input.GetKeyDown(KeyCode.DownArrow))
    //    {
    //        disableStormSystem();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            activateStormSystem();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            disableStormSystem();
        }
    }

}
