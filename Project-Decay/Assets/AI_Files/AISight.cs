using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISight : MonoBehaviour {
    /// <summary>
    /// /WORK OUT HOW TO SWITCH STATES, AI IS CURRENTLY IN TWO STATES WHICH SHOULDNT HAPPEN. FIX IT
    /// </summary>
    [Header("State")]
    public AIStates state;

    [Header("Sight Variables")]
    public float heightMultiplier;
    public float sightDist = 20f;
    public GameObject target;

    [Header("Chase Variables")]
    Vector3 _enemyTarget;
    Animator anim;
    AIMovement AIMovement;

    private void Awake()
    {
        AIMovement = GetComponent<AIMovement>();
        anim = GetComponent<Animator>();
        _enemyTarget = target.transform.position;
        state = AIStates.SEARCHING;
    }       

    void FixedUpdate()
    {
        //var _targetOffset = target.transform.position - transform.position;

        if (state == AIStates.SEARCHING)
        {
            SightRaycast();
        }       
    }

    IEnumerator switchToSearching()
    {
        state = AIStates.NOTSEARCHING;
        yield return new WaitForSeconds(1);
        state = AIStates.SEARCHING;
    }

    void SightRaycast()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);

        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.right * 4, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, -transform.right * 4, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, -transform.forward * 2, Color.green);
                        
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //print("Hit Player");
                //state = AIStates.CHASING;
                target = hit.collider.gameObject;
                AIMovement.StartChasing(target);
                if(AIMovement.enemyInCombatRange == true)
                {
                    StartCoroutine(switchToSearching());
                }
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //print("Hit Player");
                //state = AIStates.CHASING;
                target = hit.collider.gameObject;
                AIMovement.StartChasing(target);
                if (AIMovement.enemyInCombatRange == true)
                {
                    StartCoroutine(switchToSearching());
                }
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //print("Hit Player");
                //state = AIStates.CHASING;
                target = hit.collider.gameObject;
                AIMovement.StartChasing(target);
                if (AIMovement.enemyInCombatRange == true)
                {
                    StartCoroutine(switchToSearching());
                }
            }
        }

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.right).normalized, out hit, 4))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //print("Hit Player");
                //state = AIStates.CHASING;
                target = hit.collider.gameObject;
                AIMovement.StartChasing(target);
                if (AIMovement.enemyInCombatRange == true)
                {
                    StartCoroutine(switchToSearching());
                }
            }
        }

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (-transform.right).normalized, out hit, 4))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //print("Hit Player");
                //state = AIStates.CHASING;
                target = hit.collider.gameObject;
                AIMovement.StartChasing(target);
                if (AIMovement.enemyInCombatRange == true)
                {
                    StartCoroutine(switchToSearching());
                }
            }
        }

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (-transform.forward).normalized, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //print("Hit Player");
                //state = AIStates.CHASING;
                target = hit.collider.gameObject;
                AIMovement.StartChasing(target);
                if (AIMovement.enemyInCombatRange == true)
                {
                    StartCoroutine(switchToSearching());
                }
            }
        }
    }
}
