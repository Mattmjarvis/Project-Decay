using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    GasDeterrent gasDeterrent;
     
    public delegate void HealthBarDelegate(int health);
    public static event HealthBarDelegate OnHealthChanged;
    public int health = 0;
    bool healingEnabled = true;

    public bool damaged;
    public Image damageImage;
    private float damageFlashSpeed = 5f;
    public Color damageFlashColor;

    public GameObject radiationSymbol;  
    
    void Start()
    {
        health = Rules.MAX_PLAYER_HEALTH;
        //Health will be set in a function within the Rules script  
        damageFlashColor = new Color(255f, 0f, 0f, 180f);

        gasDeterrent = FindObjectOfType<GasDeterrent>();
    }

    void Update()
    {
        
        DamageFlash();
        Death();
    }
    
    public void TakeDamage(int dmg)
    {
        damaged = true;

        for (int i = 0; i < dmg; i++)
        {
            //This function must be give a dmg variable/amount when called.
            health -= 1;
            ClampHealth();
            //This will be called to clamp the health and ensure it does not go over the max amount
        }
    }

    public void Heal(int heal)
    {
        //Also takes an argument
        if (healingEnabled)
        {
            health += heal;
        }
        ClampHealth();
        //This will be called to clamp the health and ensure it does not go under the min amount
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, Rules.MAX_PLAYER_HEALTH);
        //MathF.clamp is being used to clamp the player health at the minimun amount which is 0 and the Max amount
        OnHealthChanged(health);        
    }

    public void DamageFlash()
    {

       if(damaged == true)
        {
            damageImage.color = damageFlashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime);
        }

         damaged = false;
    }

    public void inRadiation()
    {
        radiationSymbol.SetActive(true);       
    }

    void Death()
    {
        if(health == 0)
        {
            Debug.Log("Player is dead");
            Destroy(this.gameObject);
            //SM.resetScoreIncrease();
            //This must be reset when player respawns
            healingEnabled = false;
            //if player is dead it can not heal anymore

        }
    }

}
