
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    // Components
    SimpleThirdPerson playerController;
    public Image healthBar;
    //Damaged Variables
    public Image damageImage;
    private float damageFlashSpeed = 5f;
    public Color damageFlashColor = Color.red;

    //Damaged Variables
    public Image deathImage;    

    public float health;
    public bool damaged = false;

    // Audio
    public AudioClip deathSound;
    public AudioClip hurtSound;


	// Use this for initialization
	void Start () {
        // Assign Controller component
        playerController = FindObjectOfType<SimpleThirdPerson>();

        // Set health values
        health = 100;
        //healthBar.fillAmount = health / 100;
	}
	
	// Update is called once per frame
	void Update () {
        // When damage flash screen
        DamageFlash();

        // Reduce health bar fill amount
        ReduceHealthBar();

	}

    // Player takes damage
    public void TakeDamage(float amount)
    {
        // If damaged is true then flash screen
        damaged = true;
        health -= amount; // Reduce health amount
    }

    // Changes fill amount of health bar
    public void ReduceHealthBar()
    {
        // Make health bar reduce gradually
        if(healthBar.fillAmount > health / 100)
        {
            healthBar.fillAmount -= 0.01f;
        }

        // Player dies when health bar reaches 0
        if (healthBar.fillAmount <= 0)
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
    
    //IEnumerator Respawning()
    //{
    //    deathImage.enabled = true;
    //    yield return new WaitForSeconds(3);
    //    SceneManager.LoadScene(2);
    //    deathImage.enabled = false;

    //}

    // Kills player and play audio
    void Death()
    {
        if (healthBar.fillAmount <= 0)
        {
            
            Debug.Log("Player is dead");
            playerController.playerAudio.clip = deathSound;
            playerController.playerAudio.Play();
            Destroy(this.gameObject);
            SceneManager.LoadScene(2);
        }
    }
}
