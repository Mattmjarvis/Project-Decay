using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICombat : MonoBehaviour {

    AIStates state;
    AIMovement _AIMovement;
    AIHealth _AIHealth;
    Animator anim;
    Transform targetEnemy;
    PlayerHealth PlayerHealth;

    public float AttackWaitTime = 2f;
    float AttackWaitTimer;

    // Use this for initialization
    void Awake ()
    {
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
            transform.LookAt(targetEnemy);
            state = AIStates.INCOMBAT;
            //print("AI is attacking target");
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", true);

            AttackWaitTimer -= Time.deltaTime;
            //When the timer runs out to 0. The enemy can deal damage.

            if (AttackWaitTimer <= 0)
            {
                PlayerHealth.TakeDamage(damageHit);
                print("Enemy hit for " + damageHit);
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
