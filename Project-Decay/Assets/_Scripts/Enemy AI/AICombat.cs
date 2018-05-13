using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AICombat : MonoBehaviour {

    AIStates state;
    AIMovement _AIMovement;
    AIHealth _AIHealth;
    Animator anim;
    Transform targetEnemy;
    PlayerHealth PlayerHealth;
    NavMeshAgent NMA;
    

    public float AttackWaitTime = 2f;
    float AttackWaitTimer;

    // Use this for initialization
    void Awake ()
    {
        NMA = GetComponent<NavMeshAgent>();
        _AIMovement = GetComponent<AIMovement>();
        _AIHealth = GetComponent<AIHealth>();
        anim = GetComponent<Animator>();

        state = AIStates.NOTINCOMBAT;

        StartCoroutine(CombatStateMachine());
        targetEnemy = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerHealth = FindObjectOfType<PlayerHealth>();

        AttackWaitTimer = AttackWaitTime;

    }

    IEnumerator CombatStateMachine()
    {
        //State machine, organises whichs state the enemy AI is in.
        switch (state)
        {
            case AIStates.INCOMBAT:
                Attack();
                break;
        }

        yield return null;
        StartCoroutine(CombatStateMachine());
    }

    public void Attack()
    {
        if(_AIHealth.isDead == true)
        {
            return;
        }

        float damageHit = Random.Range(20f, 30f);

        if (_AIMovement.enemyInCombatRange == true)
        {
            anim.SetLayerWeight(2, 1);
            //Changes the animation layer
            transform.LookAt(targetEnemy);
            state = AIStates.INCOMBAT;

            //print("AI is attacking target");
            //Creates a random int between 0 and 2 and stores in inside an in variable
            int randomAttackAnimation = Random.Range(0, 2);

            //Stop the navMeshAgent exerting force to move forward when attacking
            NMA.isStopped = true;
            anim.SetBool("Attacking", true);
            anim.SetInteger("AttackValue", randomAttackAnimation);
            

            AttackWaitTimer -= Time.deltaTime;
            //When the timer runs out to 0. The enemy can deal damage.

            if (AttackWaitTimer <= 0)
            {
                PlayerHealth.TakeDamage(damageHit);
                //print("Enemy hit for " + damageHit);
                AttackWaitTimer = AttackWaitTime;
            }          

        }
        else
        {
            anim.SetBool("Attacking", false);

            state = AIStates.PATROLLING;
        }
    }  
}
