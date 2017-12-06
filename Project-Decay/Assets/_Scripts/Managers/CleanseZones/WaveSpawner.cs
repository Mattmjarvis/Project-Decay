using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    //Different WaveSpawning states

    [System.Serializable]
    //Allows to change values of this class inside the unity Inspector
	public class Wave
    {
        public string name;
        //name of the incoming wave.
        public Transform waveObject;
        //waveObject prefab.
        public int count;
        //countdown to next wave.
        public float rate;
        //spawn rate.
    }

    public string objectTag;
    //tag of object spawned
    public Wave[] waves;
    //array of waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    public ObjectPooler theObjectPool;
      
    public Text WaveInfoText;

    public bool canStartNextWave;

    void Start()
    {
        WaveInfoText.enabled = false;

        if (spawnPoints.Length == 0)
        {
            Debug.Log("Error! No spawn points");
        }
        waveCountdown = timeBetweenWaves;

        theObjectPool = FindObjectOfType<ObjectPooler>();
    }

    void Update()
    {
        WaveInfoText.text = "Next wave spawns in " + Mathf.Round(waveCountdown);
        if (state == SpawnState.WAITING)
        {
            WaveInfoText.enabled = false;
            if (!ObjectsStillPresent() && canStartNextWave)
            {
                WaveCompleted();
                //Begin a new round
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING && canStartNextWave)
                //if the current state is not spawning, call spawn/
            {
                //SpawnWave
                StartCoroutine(SpawnWave( waves[nextWave]));
                WaveInfoText.enabled = false;
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            //decrements waveCountdown. Using Time.DeltaTime makes it relevant to the time rather than the frames.
            WaveInfoText.enabled = true;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");
        WaveInfoText.enabled = true;
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVE COMPLETE! LOOPING...");
        }
        else
        {
            nextWave++;
        }

    }

    bool ObjectsStillPresent()
    {
        print("Objects remaining: " + GameObject.FindGameObjectsWithTag(objectTag).Any(e => e.activeInHierarchy));
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag(objectTag).Any(e => e.activeInHierarchy))
            {
                return true;
            }
            else
            {
                return false;
            }
        }        

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnObject(_wave.waveObject);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnObject(Transform _enemy)
    {
        Debug.Log("Spawning Object: " + _enemy.name);
                
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newObject = theObjectPool.GetPooledObject();
        newObject.transform.position = _sp.position;
        newObject.GetComponentInChildren<NavMeshAgent>().enabled = true;

        //Instantiate(_enemy, _sp.position, _sp.rotation);
    }

}
