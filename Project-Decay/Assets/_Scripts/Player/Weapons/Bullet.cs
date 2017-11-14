using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{

	public void OnCollisionEnter(Collision col)
	{
		Rigidbody rb = this.GetComponent<Rigidbody>();
		rb.velocity = Vector3.zero;

		rb.isKinematic = true;
		transform.position = col.contacts[0].point;

		GameObject go = new GameObject();
		go.transform.position = this.transform.position;
		go.transform.parent = col.transform;
		transform.parent = go.transform;

//		this.transform.parent = col.transform;

//		rb.useGravity = true;
	}
}
