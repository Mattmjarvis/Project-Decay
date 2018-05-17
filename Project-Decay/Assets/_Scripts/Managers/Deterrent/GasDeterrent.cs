using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasDeterrent : MonoBehaviour {

    PlayerHealth playerHealth;
    UIManager uiManager;
    MissionCompletionInfo MCI;

    private int gasDamage = 15;
    //public int currentGasMultiplier = 1;
    //This can be changed in late game to damage the player more.
    private float GasDamageTimer = 5;
   

    // Use this for initialization
    void Start ()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        MCI = FindObjectOfType<MissionCompletionInfo>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Reset the gasdamagetimer relative to the current time
    private void OnTriggerEnter(Collider other)
    {
        // Player doesn't take damage if they have radiation protection
        if(MCI.hasRadSuit == true)
        {
            return;
        }
        if(other.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(gasDamage);
            GasDamageTimer = Time.time + 2;
            //Debug.Log(GasDamageTimer);
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Player doesn't take damage if they have radiation protection
        if (MCI.hasRadSuit == true)
        {
            return;
        }
        //sets the damaged boolean to true which activates the damage screen effect while player is in gas      
        if (other.gameObject.tag == "Player")
        {
            playerHealth.damaged = true;
            uiManager.turnOnRadiationSymbol();
            //radiated = true;
            //calls the player inRadation method which activates the radiactive sign.
            if (Time.time >= GasDamageTimer)
            {
                //print("Player detected");
                //calls damage method and resets timer back to timers original value.
                playerHealth.TakeDamage(gasDamage);
                GasDamageTimer = Time.time+2;
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
