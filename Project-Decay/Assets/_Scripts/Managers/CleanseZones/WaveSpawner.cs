using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
       

    [System.Serializable]
    public class Wave
    {
        //Creates a customisable menu for the waves in the unity inspector
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    //public string enemyTag;

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    public Text WaveInfoText;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        WaveInfoText.enabled = false;

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points found.");
        }

        //waveCountdown starts as the timeBetweenWaves
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        WaveInfoText.text = "Next wave spawns in " + Mathf.Round(waveCountdown);

        if (state == SpawnState.WAITING)
        {
            WaveInfoText.enabled = false;

            //Checking if enemies are still alive
            if (!EnemyisAlive())
            {
                //Display a message to say well done
                //Begin a new round
                WaveCompleted();
                return;
            }
            else
            {
                //if enemies are still alive this will return, allowing the player to continue their wave
                return;
            }
        }

        //if waveCountdown is less than or equal to zero and it is not already spawning
        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                //Spawn Wave, calls the coroutine and sets next wave as the value of _wave
                StartCoroutine(SpawnWave(waves[nextWave]));
                WaveInfoText.enabled = false;

            }

        }
        else
        {
            //else, timer counts down
            waveCountdown -= Time.deltaTime;
            WaveInfoText.enabled = true;

        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        WaveInfoText.enabled = true;
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        //if the next wave we are going to spawn is bigger than the number of waves we have
        if(nextWave + 1 > waves.Length - 1)
        {
            //loop the waves
            nextWave = 0;
            Debug.Log("All waves completed! Looping");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyisAlive()
    {
        print("Objects remaining: " + GameObject.FindGameObjectsWithTag("EnemyCleanseMode").Any(e => e.activeInHierarchy));

        //Checks if the enemy is alive when the timer runs out, This uses less processing power.
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            //Creating a boolean method that checks for enemies in the cleanse mode using their tag
            if (GameObject.FindGameObjectsWithTag("EnemyCleanseMode").Any(e => e.activeInHierarchy))
            {
                //if none are found, meaning they are all dead, this bool returns false
                return true;
            }
            else
            {
                return false;
            }
        }        
        //else it will return true
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        //This IEnumerator takes an argument of value Wave
        state = SpawnState.SPAWNING;

        //Count is the number of enemies to spawn, will loop through until no more enemies are left to spawn
        for (int i = 0; i < _wave.count; i++)
        {
            //Spawns enemy and will way for a certain amount of time before it loops.
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        //Spawn Enemies

        //Set the state as waiting, while the player is killing enemies
        state = SpawnState.WAITING;
        yield break;

    }

    void SpawnEnemy(Transform _enemy)
    {
        //Spawn Enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);        
        //chooses from the list of spawnPoints stores the value of it in _sp
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //Instantiates the enemy prefab at the new random spawn point position and rotation.
        Instantiate(_enemy, _sp.position, _sp.rotation);        
    }
}
