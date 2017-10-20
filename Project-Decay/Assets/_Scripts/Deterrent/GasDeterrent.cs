using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasDeterrent : MonoBehaviour {

    PlayerHealth playerHealth;
    UIManager uiManager;

    private int gasDamage = 5;
    //public int currentGasMultiplier = 1;
    //This can be changed in late game to damage the player more.
    private int gasLifeTime = 60;
    private int GasDamageTimer = 5;
   
    public bool radiated;

    // Use this for initialization
    void Start ()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        uiManager = FindObjectOfType<UIManager>();
        Destroy(gameObject, gasLifeTime);        
    }

    void OnTriggerStay(Collider other)
    {
        //sets the damaged boolean to true which activates the damage screen effect while player is in gas      

        if(other.gameObject.tag == "Player")
        {
            playerHealth.damaged = true;
            uiManager.turnOnRadiationSymbol();
            //radiated = true;
            //calls the player inRadation method which activates the radiactive sign.

            if (Time.time > GasDamageTimer)
            {
                //print("Player detected");
                //calls damage method and resets timer back to timers original value.
                playerHealth.TakeDamage(gasDamage);
                
                GasDamageTimer += 2;
            }                         
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            uiManager.turnOffRadiationSymbol();
        }
    }

   
}
