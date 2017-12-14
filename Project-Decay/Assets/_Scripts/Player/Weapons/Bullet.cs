using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    EnemyHealth enemyHealth;
    private int lifeSpan = 2;
    WeaponReloader reloader;

    public GameObject bulletHole_Metal;

    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    public void OnCollisionEnter(Collision col)
    {
        //Rigidbody rb = this.GetComponent<Rigidbody>();
        //rb.velocity = Vector3.zero;

        //rb.isKinematic = true;
        //transform.position = col.contacts[0].point;

        //GameObject go = new GameObject();
        //go.transform.position = this.transform.position;
        //go.transform.parent = col.transform;
        //transform.parent = go.transform;

        // If the bullets hits an enemy then deal damage depending on weapon
        if (col.gameObject.tag == "Enemy")
        {
            enemyHealth = col.gameObject.GetComponent<EnemyHealth>();
            reloader = FindObjectOfType<WeaponReloader>(); // Gets the weapon player is using
            enemyHealth.TakeDamage(reloader.currentWeapon.bulletDamage); // Deal damage to enemy based on weapon
            //Debug.Log("Enemy took: " + reloader.currentWeapon.bulletDamage + "damage."); // Debug
        }        
        //		this.transform.parent = col.transform;

        //		rb.useGravity = true;
    }
}

   
