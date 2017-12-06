using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class ObjectPooler : MonoBehaviour {

    public GameObject pooledObject;
    //Object i am pooling

    public int pooledAmount;
    //amount of objects being pooled

    List<GameObject> pooledObjects;
	// Use this for initialization
        
	void Start ()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
            //if i is less than pooledAmount, increment i
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.GetComponentInChildren<NavMeshAgent>().enabled = false;
            //(GameObject) will ensure that the pooledObject i am instantiating is a GameObject.
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
	}

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                //Debug.Log("Found");
                OnGetPooledObject(pooledObjects[i]);
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];                           
            }
        }
        GameObject obj = (GameObject)Instantiate(pooledObject);
        //(GameObject) will ensure that the pooledObject i am instantiating is a GameObject.
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    public virtual void OnGetPooledObject(GameObject pooledObject)
    {

    }
}
