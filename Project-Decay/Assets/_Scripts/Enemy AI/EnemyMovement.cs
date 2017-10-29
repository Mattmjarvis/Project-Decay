using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    Transform player;
    //The enemies will be spawned in which means they must find the player when instantiatied
    //PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
    //Animator anim;
    //Using the NavMeshAgent will easily allow for enemy movement and object avoidance.


    public float fpsTargetDistance;
    public float enemyLookDistance;
    public float stopDistance;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Finding the player by its tag, it will then find the players position
        //anim = GetComponent<Animator>();
        //playerHealth = GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(player == null)
        {
            return;
        }
        fpsTargetDistance = Vector3.Distance(player.position, transform.position);
        if (enemyHealth.currentHealth > 0 && (fpsTargetDistance < enemyLookDistance) || enemyHealth.enemyTriggered == true)
        {
            if (fpsTargetDistance <= stopDistance)
            {
                nav.SetDestination(transform.position);
            }

            else
            {
                //anim.SetBool("isChasing", true);
                if(enemyHealth.isDead == true)
                {
                    return;
                }
                nav.SetDestination(player.position);
            }
        }
        
    }    
}
