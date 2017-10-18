using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    //How fast enemies will sink through the floor
    public int scoreValue = 10;
    //public AudioClip deathclip;

    //Animator anim;
    //AudioSource enemyAudio;
    //ParticleSystem hitParticles;
    bool isDead;
    bool isSinking;



    // Use this for initialization
    void Awake ()
    {
        //anim = GetComponent<Animator>();
        //enemyAudio = GetComponent<AudioSource>();
        //hitParticles = GetComponentInChildren<ParticleSystem>();
        //GetComponentInChildren will search for all children of the game object until it finds the right type
        //it will then store it as hitParticles

        //anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        //Health starts off as startingHealth
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            //if the object is sinking it will translate the transform down. -Vector3.up. 
        }
	}

    public void TakeDamage(int amount)
        //given this argument two arguments. How much damage will be taken and where it has been hit.
    {
        if (isDead)
            return;
        //if the enemy is already dead this code does need need to execute
        //enemyAudio.Play();

        currentHealth -= amount;
        //Plays the hurt sound and take the amount of damage from the currentHealth
        
        //hitParticles.Play();
        //The particles will be transformed whereve the HitPoint is and play from that origin
        if(currentHealth <= 0)
        {
            Death();
            //if health is less than or equal to zero. Call death function.
        }
    }

    void Death()
    {

        Debug.Log("Enemy is dead");
        isDead = true;

        CapsuleCollider col = GetComponent<CapsuleCollider>();
        col.isTrigger = true;
        //The collider is now a trigger which means it will no longer be an obstacle to the player

        //anim.SetBool("isDead", true);

        //enemyAudio.clip = deathclip;
        //enemyAudio.Play();
        StartSinking();
        //CHECK HERE FOR ERRORS WITH ENEMY DEATH
        //set the audio file to the deathclip and play it.
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        //Because this is a companent i am using .enabled = false.
        //If i was turning off the entire gameObject i would use .SetActive(false);
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        //isSinking is true
        //ScoreManager.score += scoreValue;
        //ScoreManager.showText();
        //Accesses and adds the correct enemy value to the score value variable in the ScoreManager class
        Destroy(gameObject, 2f);
        //Destroys the gameObject after 2 seconds.
    }
}
