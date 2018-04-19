﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    // Components
    SimpleThirdPerson playerController;
    Animator Anim;
    public Image healthBar;

    //Damaged Variables
    public Image damageImage;
    private float damageFlashSpeed = 5f;
    public Color damageFlashColor = Color.red;

    //Death Variables
    public GameObject deathImage;    

    //Health Variables
    public float currentHealth;
    float maxHealth = 100;
    public bool damaged = false;
    public GameObject HealReminder;

    public float reminderWaitTime = 2f;
    float reminderWaitTimer;

    // Audio
    public AudioClip deathSound;
    public AudioClip hurtSound;


	// Use this for initialization
	void Start () {
        // Assign Controller component
        playerController = GetComponent<SimpleThirdPerson>();
        Anim = GetComponent<Animator>();

        // Set health values
        currentHealth = 100;
        //healthBar.fillAmount = health / 100;

        reminderWaitTimer = reminderWaitTime;
	}
	
	// Update is called once per frame
	void Update () {
        // When damage flash screen
        DamageFlash();

        // Reduce health bar fill amount
        ReduceHealthBar();

        healing();
	}

    // Player takes damage
    public void TakeDamage(float amount)
    {
        // If damaged is true then flash screen
        damaged = true;
        currentHealth -= amount; // Reduce health amount
    }

    // Changes fill amount of health bar
    public void ReduceHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
       
        // Player dies when health bar reaches 0
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    // Flashes screen if player is damaged
    public void DamageFlash()
    {
        if (damaged == true)
        {
            damageImage.color = damageFlashColor; // Sets the flash colour
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime); // Damage image fades
        }

        damaged = false;
    }    

    public void healing()
    {
        if (currentHealth < 100)
        {
            HealReminder.SetActive(true);
            if (Input.GetKey(KeyCode.H))
            {
                Anim.SetBool("Healing", true);
                currentHealth += 0.1f;
;           }
            else
            {
                Anim.SetBool("Healing", false);
            }
        }
        else if (currentHealth >= 100)
        {
            HealReminder.SetActive(false);
            Anim.SetBool("Healing", false);
        }
    }

    // Kills player and play audio
    void Death()
    {
        print("Called death");
        //StartCoroutine(Respawning());
        Debug.Log("Player is dead");
        playerController.playerAudio.clip = deathSound;
        playerController.playerAudio.Play();
        Destroy(this.gameObject);

        deathImage.SetActive(true);
        SceneManager.LoadScene(2);
    }
}
