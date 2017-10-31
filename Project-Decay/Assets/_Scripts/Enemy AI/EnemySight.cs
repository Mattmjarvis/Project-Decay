using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemySight : MonoBehaviour
{

    NavMeshAgent agent;
    ThirdPersonShooterController character;

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
    public GameObject target;

    //Variables for Investigating
    private Vector3 investigateSpot;
    private float timer = 0f;
    public float investigateWait = 10f;

    //Variables for Sight
    public float heightMultiplier;
    public float sightDist = 10f;


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonShooterController>();

        agent.updatePosition = true;
        agent.updateRotation = false;

        waypoints = GameObject.FindGameObjectsWithTag("PatrolWaypoints");
        waypointInd = Random.Range(0, waypoints.Length);

        state = EnemySight.State.PATROL;

        alive = true;

        heightMultiplier = 0.5f;

        StartCoroutine("FSN");
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
            agent.SetDestination(waypoints[waypointInd].transform.position);
            //character.Move(agent.desiredVelocity, false, false);            
        }
        else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
        {
            waypointInd = Random.Range(0, waypoints.Length);
        }
        //else
        //{
        //    //character.moveSpeed(Vector3.zero, false, false);
        //}
    }

    void Chase()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(target.transform.position);
        //character.move(agent.desiredVelocity, false, false);
    }

    void Investigate()
    {
        timer += Time.deltaTime;
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);

        if(Physics.Raycast (transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                state = EnemySight.State.CHASE;
                target = hit.collider.gameObject;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = EnemySight.State.CHASE;
                target = hit.collider.gameObject;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = EnemySight.State.CHASE;
                target = hit.collider.gameObject;
            }
        }

        //Enemy will stop moving
        agent.SetDestination(this.transform.position);
        //Ensures player movement is zero
        //this.transform.position = new Vector3(0, 0, 0);
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
    //Currently invesitgate mode is not detecting player. Sort this tomorrow by using debugs.
}
