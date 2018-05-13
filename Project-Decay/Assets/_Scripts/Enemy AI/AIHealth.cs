using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class AIHealth : MonoBehaviour
{
    // Component
    MissionCompletionInfo MCI;
    MissionManager MM;

    AIMovement _AIMovement;
    AIStates state;
    Animator anim;
    NavMeshAgent NMG;

    public float maxHealth;
    public float currentHealth;

    public Image healthBar;
    public GameObject healthBarParent;

    public bool isDead = false;
    public bool isStarterEnemy = false; // Tick this box so game knows that the start enemy is complete when the mission has been reached.
    public bool isMission7 = false;
    public bool isMission11 = false;
    
    ParticleSystem blood;

    public GameObject PlayerTarget;

    bool kcAdded = false; // When enemy dies alert that it has updated missioninfo

    BoxCollider BC;

    private void Awake()
    {
        MCI = FindObjectOfType<MissionCompletionInfo>();
        MM = FindObjectOfType<MissionManager>();

        anim = GetComponent<Animator>();
        _AIMovement = GetComponent<AIMovement>();

        blood = GetComponent<ParticleSystem>();

        BC = GetComponent<BoxCollider>();

        NMG = GetComponent<NavMeshAgent>();

    }    

    public void TakeDamage(int damage)
    {
        //print("AI HIT");   
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
        //alertOtherEnemies();
        //print("fill amount decreasing");
        
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

    //void alertOtherEnemies()
    //{
    //    Collider[] collidersHit = Physics.OverlapSphere(transform.position, 20);
    //    //Check for enemy tags

    //    for (int i = 0; i < collidersHit.Length; i++)
    //    {
    //        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            

    //    }
    //    //call isChasing from those enemies
    //}

    IEnumerator Death()
    {
        anim.SetLayerWeight(1, 0);
        anim.SetTrigger("isDead");
        state = AIStates.DEAD;
        BC.enabled = false;
        NMG.enabled = false;
        blood.Stop();

        // Notify mission completion info
        if (isStarterEnemy == true)
        {
            MCI.startEnemyisDead = true; // Alerts mission info
            MCI.MissionCompletionCheck();
        }

        // Notify mission completion info
        if (isMission7 == true)
        {
            if (kcAdded == false)
            {
                kcAdded = true; // Stops numerator from adding too many
                MCI.M7killCount += 1;
                MCI.MissionCompletionCheck();
            }
        }

        // Notify mission completion info
        if (isMission11 == true)
        {
            if (kcAdded == false)
            {
                kcAdded = true; // Stops numerator from adding too many
                MCI.M11killCount += 1;
                MCI.MissionCompletionCheck();
            }
        }

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
