using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIHealth : MonoBehaviour
{
    AIMovement AIMovement;

    float maxHealth = 100;
    public float currentHealth = 100;

    public Image healthBar;
    public GameObject healthBarParent;

    public bool isDead = false;
    
    AIStates state;
    Animator anim;

    ParticleSystem blood;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        AIMovement = GetComponent<AIMovement>();

        blood = GetComponent<ParticleSystem>(); ;

        //StartCoroutine(HealthStateMachine());
    }

    //IEnumerator HealthStateMachine()
    //{
    //    //State machine, organises whichs state the enemy AI is in.
    //    switch (state)
    //    {
    //        case AIStates.SEARCHINGFORHEALTH:
    //            SearchForHealth();
    //            break;
    //    }

    //    yield return null;
    //    StartCoroutine(HealthStateMachine());
    //}

    public void TakeDamage(int damage)
    {        
        //sets a random choice of animation and plays it
        //Uses a sub state mashine
        int randomAnimation = Random.Range(0, 2);
        anim.SetTrigger("HitDetected");
        anim.SetInteger("HitReaction", randomAnimation);
        blood.Play();
        //displays health bar
        StartCoroutine(ShowHealthUI());

        //decreases health and takes away from the fill amount of the health bar
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth/maxHealth;
        print("fill amount decreasing");

        //Checks if health is below 30 and switches stae
        //if(health <= 30 && state != AIStates.SEARCHINGFORHEALTH)
        //{
        //    AIMovement.StartSearchingForHealth();
        //}
    }

    IEnumerator ShowHealthUI()
    {
        //Change to color lerping eventually
        healthBarParent.SetActive(true);
        yield return new WaitForSeconds(5);
        healthBarParent.SetActive(false);
    }   

    public void Death()
    {
        state = AIStates.DEAD;
        Destroy(this.gameObject);
    }

    //public void SearchForHealth()
    //{
    //    if (health <= 30)
    //    {
    //        AIMovement.StartSearchingForHealth();
    //    }
    //}

    private void Update()
    {       
        if (currentHealth <= 0)
        {
            isDead = true;
            Death();
        }
    }


}
