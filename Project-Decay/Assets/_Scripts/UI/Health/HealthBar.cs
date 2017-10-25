using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Image[] hearts;
    //An array of images used to store the heart images. Found on the Canvas.
    private int healthPerHeart = 10;

    void OnEnable()
    {
        Rules.MAX_PLAYER_HEALTH = hearts.Length * healthPerHeart;
        //length can be used on hearts as it is an array[]
        //determine how much health we have
        PlayerHealth.OnHealthChanged += OnHealthChanged;
    }
    void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= OnHealthChanged;
    }

    void OnHealthChanged(int health)
    {
        int heart = health / healthPerHeart; //will default to lower bound
        int heartfill = health % healthPerHeart; //return the remainder of the division

       
        if(health % healthPerHeart == 0)
        {
            if(heart == hearts.Length)//indicates the player has full health
            {
                hearts[heart - 1].fillAmount = 1;
                return;
            }
            if(heart > 0)//indicates anything but 0 health, where there are only whole hearts or empty hearts
            {
                hearts[heart].fillAmount = 0;
                hearts[heart-1].fillAmount = 1;
            }
            else
            {
                hearts[heart].fillAmount = 0;
            }
            return;

        }

        hearts[heart].fillAmount = heartfill / (float)healthPerHeart;
        //getting the heart arrays heart fill amount and setting it to the value of heart fill divided by healthPerHeart which is being cast into a float 
   }


}
