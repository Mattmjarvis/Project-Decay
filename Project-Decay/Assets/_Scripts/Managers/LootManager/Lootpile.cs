using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lootpile : MonoBehaviour {

    // Get components
    LootManager lm;
    UIManager uiManager;
    Interact interact;

    // Item spawn components
    public GameObject mainSpawnPoint;
    public Transform[] landPoints;
    private GameObject[] pickup = new GameObject[5];

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
    //public void Update()
    //{
    //    DistanceCheck();

    //}


    //// Checks distance between player and lootpile
    //public void DistanceCheck()
    //{
    //    // Get distance
    //    distance = Vector3.Distance(player.transform.position, this.transform.position);

    //    // If player is close enough to lootpile then show search option
    //    if(distance <= 10 && !searched)
    //    {
    //        player.GetComponent<SimpleThirdPerson>().isSearching = true; // Set player to searching
    //        uiManager.enableSearchTip(); // Show search tooltip
    //        // Get input and spawn items
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            player.GetComponent<SimpleThirdPerson>().isSearching = fa;
    //            this.SpawnItems();
    //        }
    //    }

    //    // Disable the search tip if player is not in range
    //    else if(player.GetComponent<SimpleThirdPerson>().isSearching == false && distance > 10)
    //    {
    //        uiManager.disableSearchTip();
    //    }
    //}

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



            }
            searched = true;
            this.GetComponent<Lootpile>().enabled = false;
        }
    }
}
