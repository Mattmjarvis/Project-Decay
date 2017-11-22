using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lootpile : MonoBehaviour {

    // Get lootmanager component
    LootManager lm;

    // Item spawn components
    public GameObject mainSpawnPoint;
    public Transform[] landPoints;
    private GameObject[] pickup = new GameObject[5];

    // Has the lootpile been searched
    public bool searched = false;



    public void Start()
    {
        lm = FindObjectOfType<LootManager>();

    }
    public void Update()
    {
        

    }

    // Spawns items when the player searches the lootpile
    public void SpawnItems()
    {
        // Spawn items for each spawn point
        if (searched == false)
        {
            for (int i = 0; i < pickup.Length; i++)
            {
                int randomSpawnGen = Random.Range(0, lm.spawnItems.Length); // generates random spawn

                // Spawns each item relative to the main spawnpoint
                pickup[i] = Instantiate(lm.spawnItems[randomSpawnGen], landPoints[i].transform, false);
                pickup[i].transform.position = mainSpawnPoint.transform.position;
                searched = true;
            }
        }
    }
}
