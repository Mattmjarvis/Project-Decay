
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    // Components
    SimpleThirdPerson playerController;
    Animator Anim;
    InputManager IM;
    public Image healthBar;

    //Damaged Variables
    public Image damageImage;
    private float damageFlashSpeed = 5f;
    public Color damageFlashColor = Color.red;

    //Death Variables
    public GameObject deathScreen;

    //Health Variables
    public float currentHealth;
    float maxHealth = 100;
    public bool damaged = false;
    public GameObject HealReminder;
    public bool nowHealing = false;
    public bool dead = false;

    // Audio
    public AudioClip deathSound;
    public AudioClip hurtSound;

    // Use this for initialization
    void Start()
    {
        // Assign Controller component
        playerController = GetComponent<SimpleThirdPerson>();
        IM = FindObjectOfType<InputManager>();
        Anim = GetComponent<Animator>();

        // Set health values
        currentHealth = 100;
        //healthBar.fillAmount = health / 100;

        StartCoroutine(healPrompt());
    }

    // Update is called once per frame
    void Update()
    {
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
            // Stops update from playing lots of audio
            if(dead == false)
            {
                Death();
                dead = true;
            }

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

    IEnumerator healPrompt()
    {
        while (currentHealth >= 61)
        {
            yield return null;
        }

        HealReminder.SetActive(true);
        yield return new WaitForSeconds(1f);
        HealReminder.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(healPrompt());

        //While health is 100 or greater, yield return null
        //turn game objects off
        //wait seconds
        //turn back on
        //restart coroutine

    }

    public void healing()
    {
        if (currentHealth < 100)
        {
            //HealReminder.SetActive(true);
            if (Input.GetKey(KeyCode.H))
            {
                nowHealing = true;
                playerController.canFire = false;
                playerController.gunActive = false;
                Anim.SetBool("Healing", true);
                currentHealth += 0.1f;
                
            }
            else
            {
                nowHealing = false;
                playerController.canFire = true;
                //playerController.gunActive = true;
                Anim.SetBool("Healing", false);
            }
        }

        else if (currentHealth >= 100)
        {
            nowHealing = false;
            playerController.canFire = true;
            //playerController.gunActive = true;
            //HealReminder.SetActive(false);
            Anim.SetBool("Healing", false);
        }
    }

    IEnumerator StartDeathSequence()
    {
        //Stops player from firing
        playerController.canFire = false;
        playerController.gunActive = false;
        playerController.blockControl = true;

        //Switches animation layer
        Anim.SetLayerWeight(0, 1);
        Anim.SetBool("isDead", true);

        //Waits for 3 seconds before activating audio and UI
        yield return new WaitForSeconds(3);

        IM.PauseGameplay(); // Pauses game
        // Disable all audio sources
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudioSources)
        {
            audio.Stop();
        }
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(deathSound, 1f);
        deathScreen.SetActive(true);
    }

    // Kills player and play audio
    public void Death()
    {
        
        StartCoroutine(StartDeathSequence());
    }
}
