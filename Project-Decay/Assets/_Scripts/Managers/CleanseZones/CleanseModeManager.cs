using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanseModeManager : MonoBehaviour {

    public GameObject CleanseMeter;
    Image cleanseSlider;

    bool isCleansing = false;

    //EnemyHealth enemyHealth;

    // Use this for initialization
    void Awake ()
    {
        //CleanseMeter = GameObject.FindGameObjectWithTag("CleanseMeter");
       
        cleanseSlider = CleanseMeter.GetComponent<Image>();
        //enemyHealth = FindObjectOfType<EnemyHealth>();
    }        	
	
	// Update is called once per frame
	void Update ()
    {
        if(isCleansing != true)
        {
            CleanseMeter.SetActive(false);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            print("Player is Cleansing");
            CleanseMeter.SetActive(true);
            isCleansing = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            print("Player is not Cleansing");
            CleanseMeter.SetActive(false);
            isCleansing = false;
        }
    }

    public void updateCleanseSlider()
    {
        if(isCleansing == true)
        {
            cleanseSlider.fillAmount += 0.05f;
        }

        if (cleanseSlider.fillAmount == 1f)
        {
            print("Zone Cleansed!");
        }
    }
}
