using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lootpile : MonoBehaviour {

    // Get components
    LootManager lm;
    UIManager uiManager;
    Interact interact;
    MissionCompletionInfo MCI;

    // Item spawn components
    public GameObject mainSpawnPoint;
    public Transform[] landPoints;
    private GameObject[] pickup = new GameObject[5];

    // Distance check
    public GameObject player;
    public float distance;

    // Has the lootpile been searched
    public bool searched = false;
    public bool pistolSpawn = false;

    int randomSpawnGen;

    public void Start()
    {
        lm = FindObjectOfType<LootManager>();
        uiManager = FindObjectOfType<UIManager>();
        interact = FindObjectOfType<Interact>();
        MCI = FindObjectOfType<MissionCompletionInfo>();

    }

    // Spawns items when the player searches the lootpile
    public void SpawnItems()
    {
        // Spawn items for each spawn point
        if (searched == false)
        {
            if (pistolSpawn == false)
            {
                for (int i = 0; i < pickup.Length; i++)
                {
                    // If the player only has a shotgun only spawn shotgun ammo
                    if (MCI.hasShotgun == true && MCI.hasAR ==false )
                    {
                        randomSpawnGen = 0;
                    }
                    // If the player only has an AR only spawn AR ammo
                    else if(MCI.hasShotgun == false && MCI.hasAR == true)
                    {
                        randomSpawnGen = 1;
                    }
                    // If the player has both weapons then spawn both ammo
                    else if(MCI.hasShotgun == true && MCI.hasAR == true)
                    {
                        randomSpawnGen = Random.Range(0, 1); // generates random spawn
                    }

                    else { return; }


                    // Spawns each item relative to the main spawnpoint
                    pickup[i] = Instantiate(lm.spawnItems[randomSpawnGen], landPoints[i].transform, false);
                    pickup[i].transform.position = mainSpawnPoint.transform.position;
                }
            }
            // Check if pile is meant to spawn the pistol
            else if(pistolSpawn == true)
            {
                pickup[0] = Instantiate(lm.pistol, landPoints[0].transform, false);
                pickup[0].transform.position = mainSpawnPoint.transform.position;
            }
            searched = true;

            // Disables the particle system
            gameObject.transform.Find("Particle").GetComponent<ParticleSystem>().enableEmission = false;
            
            this.GetComponent<Lootpile>().enabled = false;
            uiManager.disableSearchTip();
        }
    }
}
