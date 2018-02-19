using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTester : MonoBehaviour {
    //This script is used for testing, i would normally test by using methods within the PlayerHealth script for it to damage itself
    //However as ai will be damaging the player it is better to call damage from another script

    public PlayerHealthDamiano playerHealth;
	
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealthDamiano>();
    }

    public void Heal(int health)
    {
        playerHealth.Heal(health);
        //Debug.Log("Healing");
    }

    public void hurt(int dmg)
    {        
        playerHealth.TakeDamage(dmg);
        //Debug.Log("Damaging");
    }

}
