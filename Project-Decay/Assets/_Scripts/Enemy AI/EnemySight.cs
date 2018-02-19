using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemySight : MonoBehaviour
{
    NavMeshAgent agent;
    ThirdPersonShooterController player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    EnemyAttack enemyAttack;
    Transform playerTransform;
    Animator anim;

    public enum State
    {
        PATROL,
        CHASE,
        INVESTIGATE
    }

    public State state;
    private bool alive;

    //Variables for Patrolling
    public GameObject[] waypoints;
    private int waypointInd;
    public float patrolSpeed = 0.5f;

    //Variables for Chasing
    public float chaseSpeed = 1f;
    public GameObject playerTarget;
    Vector3 target;
    float targetDistance;

    //Variables for Investigating
    private Vector3 investigateSpot;
    private float timer = 0f;
    public float investigateWait = 10f;

    //Variables for Sight
    public float heightMultiplier;
    public float sightDist = 20f;


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GetComponent<ThirdPersonShooterController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = FindObjectOfType<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAttack = GetComponent<EnemyAttack>();
        anim = GetComponent<Animator>();

        agent.updatePosition = true;
        agent.updateRotation = false;

        //waypoints = GameObject.FindGameObjectsWithTag("PatrolWaypoints");
        waypointInd = Random.Range(0, waypoints.Length);

        state = EnemySight.State.PATROL;

        alive = true;

        heightMultiplier = 0.5f;

        StartCoroutine("FSN");
    }

    void Update()
    {
        if(playerHealth.health <= 0)
        {
            state = EnemySight.State.PATROL;
            return;
        }
        targetDistance = Vector3.Distance(playerTransform.transform.position, transform.position);
        //target distance is equal to the distance between the target and this enemies transform
    }
    IEnumerator FSN()
    {
        //State machine, organises whichs state the enemy AI is in.
        while (alive)
        {
            switch (state)
            {
                case State.PATROL:
                    Patrol();
                    break;
                case State.CHASE:
                    Chase();
                    break;
                case State.INVESTIGATE:
                    Investigate();
                    break;
            }
            yield return null;
        }
    }
    void Patrol()
    {
        agent.speed = patrolSpeed;
        //Sets the NavMesh speed to be the patrol speed
        if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
        {
            if(enemyHealth.isDead == true)
            {
                return;
            }
            agent.SetDestination(waypoints[waypointInd].transform.position);
            anim.SetFloat("Speed", 1 , 0.25f, Time.deltaTime);
            Vector3 targetWayPoint = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z);
            //Steering point tells unity to make the AI face the point in which the agent is moving too
            Vector3 lookRot = targetWayPoint - transform.position;
            if(lookRot != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(lookRot, Vector3.up);
                //Debug.Log("Look Rotation: " + lookRot);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
                //Smooth rotation based on the position it is moving towards.
            }
        }
        else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
        {
            waypointInd = Random.Range(0, waypoints.Length);
        }        
    }

    void Chase()
    {
        if(enemyHealth.isDead == true)
        {
            return;
        }

        var offset = target - transform.position;
        if(offset.magnitude > .1f)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(playerTarget.transform.position);
            anim.SetFloat("Speed", chaseSpeed, 2f, Time.deltaTime);

        }


        Vector3 targetPoint = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z);
        var targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 4.0f);

        if (targetDistance > sightDist)
        {
            //Debug.Log("Enemy is out of sight");
            state = EnemySight.State.PATROL;
        }
    }

    void Investigate()
    {
        timer += Time.deltaTime;      
        //Enemy will stop moving
        agent.SetDestination(this.transform.position);
        //Ensures player movement is zero
        transform.LookAt(investigateSpot);
        if(timer >= investigateWait)
        {
            state = EnemySight.State.PATROL;
            timer = 0;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Player")
        {
            state = EnemySight.State.INVESTIGATE;
            investigateSpot = coll.gameObject.transform.position;
        }
    }

    void FixedUpdate()
    {
        
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = EnemySight.State.CHASE;
                playerTarget = hit.collider.gameObject;
                //print("Player hit");
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = EnemySight.State.CHASE;
                playerTarget = hit.collider.gameObject;
                //print("Player hit");

            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = EnemySight.State.CHASE;
                playerTarget = hit.collider.gameObject;
                //print("Player hit");
            }
        }
    }
}
