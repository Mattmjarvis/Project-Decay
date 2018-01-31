using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 4f;
    public int attackDamage = 50;

    //Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyMovement enemyMovement;
    EnemyHealth enemyHealth;
    EnemySight enemySight;
    Animator anim;

    //NavMeshAgent nav;

    //AudioSource enemyAudio;
    //public AudioClip attackSound;

    bool playerInRange;
    public bool enemyAttacking;
    float timer;
    float playerDistance;
    public float attackDistance;

	void Awake ()
    {
        enemyAttacking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        //enemyMovement = GetComponent<EnemyMovement>();
        //finds the player objects
        playerHealth = FindObjectOfType<PlayerHealth>();
        //nav = GetComponent<NavMeshAgent>();
        //pulls the player health script off of the player, stores and adds a reference to it.
        //This will improve performance as apose to constantly searching for the script.
        enemyHealth = GetComponent<EnemyHealth>();
        enemySight = GetComponent<EnemySight>();
        anim = GetComponent<Animator>();
        //anim = GetComponent<Animator>();
        
        //enemyAudio = GetComponent<AudioSource>();
	}   
    void Update()
    {
        if(player == null)
        {
            return;
        }

        timer += Time.deltaTime;
        //creates a timer that will track attack duration
        checkAttack();
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            //check if the timer is greater than the timeBetweenAttacks limit and the player is in range and the enemie is not dead
            Attack();            
            //Call attack
        }
       
        //if(playerHealth.currentHealth <= 0)
        //{
        //    //enemyMovement.MoveEnemy();
        //    //anim.SetTrigger ("PlayerDead");
        //    //add code that will turn the enemy around and walk away from corpse
        //}
        //HERE
    }

    public void checkAttack()
    {
        // Checks the player distance whether to attack
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance <= attackDistance)
        {
            playerInRange = true;
            //nav.speed = 0;
        }
        else
        {
            playerInRange = false;
            anim.SetBool("EnemyIsAttacking", false);
            //anim.SetBool("isAttacking", false);
            //nav.speed = 3.5f;
        }
    }

	 void Attack()
    {
        timer = 0f;
        //reset timer to zero

        if (playerHealth.health > 0)
        {
            Debug.Log("Enemy has stopped to attack");
            //if playerHealth is greater than 0
            playerHealth.TakeDamage(25);
            anim.SetBool("EnemyIsAttacking", true);
            //enemyAudio.clip = attackSound;
            //enemyAudio.Play();
            //damage the playerHealth with the value of attackDamage
            //if the players health drops below 0 the previous code will execute.
            Debug.Log("DamagePlayer Called: " + attackDamage);

        }
    }
}
