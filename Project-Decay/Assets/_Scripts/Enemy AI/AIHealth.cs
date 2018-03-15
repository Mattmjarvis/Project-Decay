using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class AIHealth : MonoBehaviour
{
    AIMovement _AIMovement;
    AIStates state;
    Animator anim;
    NavMeshAgent NMG;

    public float maxHealth;
    public float currentHealth;

    public Image healthBar;
    public GameObject healthBarParent;

    public bool isDead = false;   
    
    ParticleSystem blood;

    public GameObject PlayerTarget;

    BoxCollider BC;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _AIMovement = GetComponent<AIMovement>();

        blood = GetComponent<ParticleSystem>();

        BC = GetComponent<BoxCollider>();

        NMG = GetComponent<NavMeshAgent>();

    }    

    public void TakeDamage(int damage)
    {
        print("AI HIT");   
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
        
        if(_AIMovement.isChasing != true)
        {
            _AIMovement.StartChasing(PlayerTarget);
        }       
    }

    IEnumerator ShowHealthUI()
    {
        //Change to color lerping eventually
        healthBarParent.SetActive(true);
        yield return new WaitForSeconds(5);
        healthBarParent.SetActive(false);
    }

    IEnumerator Death()
    {
        anim.SetTrigger("isDead");
        state = AIStates.DEAD;
        BC.enabled = false;
        NMG.enabled = false;
        blood.Stop();
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }        

    private void Update()
    {       
        if (currentHealth <= 0)
        {
            isDead = true;
            StartCoroutine(Death());
        }
    }


}
