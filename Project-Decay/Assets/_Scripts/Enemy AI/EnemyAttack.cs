using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 2f;
    public int attackDamage = 5;

    //Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyMovement enemyMovement;
    EnemyHealth enemyHealth;
    //NavMeshAgent nav;

    //AudioSource enemyAudio;
    //public AudioClip attackSound;

    bool playerInRange;
    float timer;
    float playerDistance;
    public float attackDistance;

	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //enemyMovement = GetComponent<EnemyMovement>();
        //finds the player objects
        playerHealth = FindObjectOfType<PlayerHealth>();
        //nav = GetComponent<NavMeshAgent>();
        //pulls the player health script off of the player, stores and adds a reference to it.
        //This will improve performance as apose to constantly searching for the script.
        enemyHealth = GetComponent<EnemyHealth>();
        //anim = GetComponent<Animator>();
        
        //enemyAudio = GetComponent<AudioSource>();
	}

    #region Old Attack code
    //void OnTriggerEnter(Collider other)
    //{
    //    //If something that is not the enemy gameObject enters the collider this will be called
    //    if(other.gameObject == player)
    //    {
    //        //check is whatever collided is the player
    //        anim.SetBool("isAttacking", true);
    //        playerInRange = true;
    //        nav.speed = 0;
    //        //if so, playerInRange is equal to true
    //    }
    //}	

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject == player)
    //    {
    //        //check is whatever left the collider is the player
    //        playerInRange = false;
    //        anim.SetBool("isAttacking", false);
    //        nav.speed = 3.5f;
    //        //if so, set playerInRange to false
    //    }
    //}
    #endregion

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
            //anim.SetBool("isAttacking", true);
            
            //nav.speed = 0;
        }

        else
        {
            playerInRange = false;
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
            //if playerHealth is greater than 0
            //anim.SetBool("isAttacking", true);
            playerHealth.TakeDamage(attackDamage);

            //enemyAudio.clip = attackSound;
            //enemyAudio.Play();
            //damage the playerHealth with the value of attackDamage
            //if the players health drops below 0 the previous code will execute.
            Debug.Log("DamagePlayer Called: " + attackDamage);
        }
    }
}