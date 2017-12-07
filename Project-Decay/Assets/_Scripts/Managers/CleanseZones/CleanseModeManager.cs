using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanseModeManager : MonoBehaviour {

    public GameObject CleanseMeter;
    Image cleanseSlider;

    Vector3 rewardSpawnPoint = new Vector3(0,0,0);
    public GameObject lootPileReward;

    bool isCleansing = false;

    //EnemyHealth enemyHealth;

    // Use this for initialization
    void Awake()
    {
        //Getas the image component of the cleanseMeter game object
        cleanseSlider = CleanseMeter.GetComponent<Image>();
    }
    // Update is called once per frame
	void Update ()
    {
        //if cleansing is not true, turn off the CleanseMeter UI
        if(isCleansing != true)
        {
            CleanseMeter.SetActive(false);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            //If player is in the zone cleansing is true and CleanseMeter UI is true
            //print("Player is Cleansing");
            CleanseMeter.SetActive(true);
            isCleansing = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            //If player leaves the cleansing zone, cleansing is false and CleanseMeter UI is false
            //print("Player is not Cleansing");
            CleanseMeter.SetActive(false);
            isCleansing = false;
        }
    }

    public void updateCleanseSlider()
    {
        //if cleansing is true add the amount to the fill of the slider, called from the enemyHealth script
        if(isCleansing == true)
        {
            cleanseSlider.fillAmount += 0.2f;
        }
        //When fill amount is full, DO SOMETHING.
        if (cleanseSlider.fillAmount == 1f)
        {
            Instantiate(lootPileReward, rewardSpawnPoint, Quaternion.identity);

        }
    }
}
