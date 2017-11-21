using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {

    public GameObject[] spawnItems;
    public Transform spawnPoint;
    public Transform[] landPoints;
    private bool searched = false;

    // Spawns items when the player searches the lootpile
    public void SpawnItems()
    {
        // Spawn items for each spawn point
        for(int i = 0; i < landPoints.Length; i++)
        {
            int randomSpawnGen = Random.Range(0, spawnItems.Length + 1); // generates random spawn
            Instantiate(spawnItems[randomSpawnGen], landPoints[i]); // Spawns random item for each spawnpoint
        }   


    }
}
