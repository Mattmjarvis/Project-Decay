using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitHeal : MonoBehaviour {

    AIHealth _AIHealth;
    int healAmount = 50;

    private void Start()
    {
        _AIHealth = FindObjectOfType<AIHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AI")
        {
            print("Triggered");
            //_AIHealth.currentHealth += healAmount;
            print("Healed");
        }
    }
}
