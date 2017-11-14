using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    #region variables
    [SerializeField]float speed;
    [SerializeField]float lifeSpan;
    public WeaponStats weapon;
 


    //EnemyHealth enemyHealth;
    #endregion
    void Start()
    {
        //enemyHealth = FindObjectOfType<EnemyHealth>();
        //GameObject is destroyed after specified lifetime, change to the an ObjectPool later in game.
        Destroy(gameObject, lifeSpan);
        // Finds the active weapon to apply bullet damage
        weapon = FindObjectOfType<WeaponStats>();
    }

    void Update()
    {
        //Translates the x position of the object from the point where it is instantiated.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(weapon.bulletDamage);
            //print("take damage called: Damage: " + weapon.bulletDamage);
            Destroy(gameObject);
        }
        //Add damage here etc.
        //print("Hit: " + other.name);
    }
}
