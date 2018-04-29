using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolLootPile : MonoBehaviour {

    // Get components
    LootManager lm;
    UIManager uiManager;
    Interact interact;

    // Item spawn components
    public GameObject mainSpawnPoint;
    public Transform landPoint;
    private GameObject pickup = new GameObject();
    public GameObject pistol;

    // Distance check
    public GameObject player;
    public float distance;

    // Has the lootpile been searched
    public bool searched = false;

    public void Start()
    {
        lm = FindObjectOfType<LootManager>();
        uiManager = FindObjectOfType<UIManager>();
        interact = FindObjectOfType<Interact>();

    }

    // Spawns items when the player searches the lootpile
    public void SpawnItems()
    {
        // Spawn items for each spawn point
        if (searched == false)
        {

            // Spawns each item relative to the main spawnpoint
            pickup = Instantiate(pistol, landPoint.transform, false);
            pickup.transform.position = mainSpawnPoint.transform.position;

            searched = true;

            // Disables the particle system
            gameObject.transform.Find("Particle").GetComponent<ParticleSystem>().enableEmission = false; // Not sure why it's green but it does the job.

            this.GetComponent<PistolLootPile>().enabled = false;
            uiManager.disableSearchTip();
        }
    }
}
