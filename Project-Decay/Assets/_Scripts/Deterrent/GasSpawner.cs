using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpawner : MonoBehaviour {

    public GameObject gasZone;
    public GameObject[] spawnPoints;
    private GameObject currentPoint;
    public bool gasActive;
    public float spawnRate = 1f;
    //public float spawnAmount = 5;
    private int index;
    private bool spawning;

    void start()
    {
        if(gasZone.activeInHierarchy == true)
        {
            gasActive = true;
        }
    }
    void Update()
    {
        /*transform.childCount < spawnAmount && */
        if (spawning == false && gasActive == false)
        {
            StartCoroutine(SpawnGas());
            //SpawnGas();
        }
    }

    public IEnumerator SpawnGas()
    {                
        spawning = true;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");        
        while (true)
        {
            index = Random.Range(0, spawnPoints.Length);
            currentPoint = spawnPoints[index];
            if (currentPoint.transform.childCount == 0)
            {
                break;
            }
            //Checks if a gasZone is already spawned on the spawnPoint, if so it will not spawn there again until one spawn has passed.
            index -= 1;
        }

        GameObject spawnedGasZone = Instantiate(gasZone, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
        //Storing the instantiated gasZone prefab in a variable called spawnedGasZone.
        spawnedGasZone.transform.parent = currentPoint.transform;
        //spawnedGasZone.GetComponent<GasDeterrent>().currentGasMultiplier = 1;
        yield return new WaitForSeconds(spawnRate);
        //Will wait for seconds to re set the spawing bool which calls the Ienumerator.
        spawning = false;
    }
}
