using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {      

    public AIStates state;
    AISight AISight;
    AICombat _AICombat;
    AIHealth _AIHealth;
    
    //Wandering Variables
    public float speed = 4;
    public float directionChangeInterval = 1;
    public float maxHeadingChange = 30;

    //Patrolling Variables
    public GameObject[] waypoints;
    private int waypointInd;
    Vector3 _Target;
    GameObject _targetEnemy;

    public float patrolWaitTime = 3;
    float patrolWaitTimer;
    //public GameObject targetPos;

    NavMeshAgent navMeshAgent;
    float heading;
    Vector3 targetRotation;
    Animator anim;

    public float AttackWaitTime = 3f;
    float AttackWaitTimer;


    public bool enemyInCombatRange = false;

    public int chaseSpeed = 3;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        AISight = GetComponent<AISight>();
        _AICombat = GetComponent<AICombat>();
        _AIHealth = GetComponent<AIHealth>();

        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        //chooses a random waypoint in the array and sets it as the current waypoint.
        waypointInd = Random.Range(0, waypoints.Length);
        if (waypointInd == 0)
        {
            print("No Waypoint index");
        }

        anim = GetComponent<Animator>();

        StartCoroutine(NewHeading());
        StartCoroutine(MovementStateMachine());

        state = AIStates.PATROLLING;

        _Target = waypoints[waypointInd].transform.position;

        patrolWaitTimer = patrolWaitTime;

        AttackWaitTimer = AttackWaitTime;
    }

    IEnumerator MovementStateMachine()
    {        
        //State machine, organises whichs state the enemy AI is in.

        switch (state)
        {
            case AIStates.IDLE:
                Idle();
                break;
            case AIStates.WANDERING:
                Wandering();
                break;
            case AIStates.PATROLLING:
                Patrolling();                
                break;
            case AIStates.CHASING:
                Chase(_targetEnemy);
                break;
            case AIStates.SEARCHINGFORHEALTH:
                FindHealth();
                break;
        }
        yield return null;
        StartCoroutine(MovementStateMachine());
    }

    void Idle()
    {
        //Sets Idle Animations
        anim.ResetTrigger("Walking");
        anim.SetTrigger("Idle");
    }

    void Wandering()
    {
        //print("STATE IS NOW WANDERING!");
        //Sets AI state to wandering
        //Checks if the AI state is set to Wandering, if so, it executes the code below
        //The wandering code must be in an Update method as it is using Time.deltaTime

        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, directionChangeInterval);
        anim.SetTrigger("Walking");
        var forward = transform.TransformDirection(Vector3.forward);
        navMeshAgent.Move(forward);
    }   

    void Patrolling()
    {
        if (_AIHealth.isDead == true)
        {
            return;
        }
        var target = waypoints[waypointInd].transform.position;
        //var NMA = GetComponent<NavMeshAgent>();
        var offset = target - transform.position;
        //Get the difference.
        if (offset.magnitude > 3f)
        {
            //If we're further away than .1 unit, move towards the target.
            //The minimum allowable tolerance varies with the speed of the object and the framerate. 
            // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
            offset = offset.normalized * speed;
            //normalize it and account for movement speed.
            navMeshAgent.Move(offset * Time.deltaTime);
            //actually move the character.
            anim.SetTrigger("Walking");
            //Vector3 targetWayPoint = new Vector3(transform.rotation.x, transform.position.y, transform.rotation.z);
            //Steering point tells unity to make the AI face the point in which the agent is moving too
            Vector3 lookRot = target - transform.position;
            if (lookRot != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(lookRot, Vector3.up);
                //Debug.Log("Look Rotation: " + lookRot);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 44.0f);
                //Smooth rotation based on the position it is moving towards.
            }
        }
        else if (offset.magnitude < 3f)
        {
            patrolWaitTimer -= Time.deltaTime;
            anim.SetTrigger("Idle");
            if(patrolWaitTimer <= 0)
            {
                waypointInd = Random.Range(0, waypoints.Length);
                patrolWaitTimer = patrolWaitTime;
            }            
        }
    }

    public void Chase(GameObject targetEnemy)
    {
        if(_AIHealth.isDead == true)
        {
            return;
        }
        //print("Chase called");
        //var navMeshAgent = GetComponent<NavMeshAgent>();
        var _target = targetEnemy.transform.position;
        var offset = _target - transform.position;
        //Get the difference.
        if (offset.magnitude > 3f)
        {
            enemyInCombatRange = false;

            //If we're further away than .1 unit, move towards the target.
            //The minimum allowable tolerance varies with the speed of the object and the framerate. 
            // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
            offset = offset.normalized * chaseSpeed;
            //normalize it and account for movement speed.
            navMeshAgent.Move(offset * Time.deltaTime);
            //actually move the character.
            anim.SetBool("Running", true);
            Vector3 lookRot = _target - transform.position;
            if (lookRot != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(lookRot, Vector3.up);
                //Debug.Log("Look Rotation: " + lookRot);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 44.0f);
                //Smooth rotation based on the position it is moving towards.
            }
        }        
        else if(offset.magnitude <= 3f)
        {
            //print("In combat range");
            enemyInCombatRange = true;
            state = AIStates.INCOMBAT;

            _AICombat.Attack();            

            //print("States switched");            
        }
        //anim.SetTrigger("Chasing");              
    }

    void FindHealth()
    {
        //AI SEARCHING AND MOVING TOWARD MULTIPLE HEALTH POINTS, FIX IT
        //Search for any medkit
        int index;
        GameObject currentMedkitPoint;

        GameObject[] medkits = GameObject.FindGameObjectsWithTag("MedKits");
        while (true)
        {
            index = Random.Range(0, medkits.Length);
            currentMedkitPoint = medkits[index];
            if (currentMedkitPoint.transform.childCount == 0)
            {
                break;
            }

            index -= 1;
        }

        //Search distance to medkit
        var MedKitTarget = currentMedkitPoint.transform.position;
        var offset = MedKitTarget - transform.position;

        if(offset.magnitude > 1)
        {
            //move towards medkit
            navMeshAgent.Move(offset * Time.deltaTime);
            anim.SetTrigger("Walking");
            //print("Moving");
            Vector3 lookRot = MedKitTarget - transform.position;
            if (lookRot != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(lookRot, Vector3.up);
                //Debug.Log("Look Rotation: " + lookRot);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 44.0f);
                //Smooth rotation based on the position it is moving towards.
            }
        }
        else if( offset.magnitude < 1)
        {
            anim.SetTrigger("Idle");
            //if(_AIHealth.currentHealth > 30)
            //{
            //    state = AIStates.PATROLLING;
            //}
        }
        //check if health is above certain value
        //return to patrol

    }

    public void StartSearchingForHealth()
    {
        state = AIStates.SEARCHINGFORHEALTH;
    }

    public void StartChasing(GameObject target)
    {
        //print("Chase called");
        state = AIStates.CHASING;
        _targetEnemy = target;
    }   
    
    //Repeatedly calculates a new direction to move towards.    
    IEnumerator NewHeading()
    {
        while (true)
        {            
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
    
    // Calculates a new direction to move towards.   
    void NewHeadingRoutine()
    {
        var floor = transform.eulerAngles.y - maxHeadingChange;
        var ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }
}
