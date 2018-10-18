using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	private Rigidbody rb;
	public float speed;

////////////////
	// public GameObject[] targets;
	public float rotateSpeed = 200f;

	/////// other tutorial
	private GameObject[] targets;
	// public GameObject target;



	// moves gameObject forward (shot, asteroid)
	void Start ()
	{
		rb = GetComponent<Rigidbody>();

		// if (rb.tag == "Player Bolt")
		// {
		// 	return;
		// } else
		// {
			rb.velocity = transform.forward * speed;
		// }
	}


	// add homing

	// void Update ()
	// {
	// 	if (rb.tag == "Player Bolt")
	// 	{
	// 		if (targets == null)
	// 		{
	// 			targets = GameObject.FindGameObjectsWithTag("Enemy");
	//
	// 			foreach (GameObject target in targets)
	// 			{
	// 				Vector3 direction = (Vector3)target.transform.position - rb.transform.position;
	// 				direction.Normalize();
	// 				// rb.transform.Translate(target.transform.position * Time.deltaTime * speed);
	// 				// rb.transform.Translate(direction * Time.deltaTime * speed);
	// 				var targetRotation = Quaternion.LookRotation(direction);
	// 				rb.MoveRotation(Quaternion.RotateTowards(rb.transform.rotation, target.transform.rotation, rotateSpeed));
	// 				rb.transform.position = Vector3.MoveTowards(rb.transform.position, target.transform.position, speed * Time.deltaTime);
	// 			}
	// 		}
	// 		// foreach(GameObject target in targets)
	// 		// {
	// 		// 	Vector3 direction = (Vector3)target.transform.position - rb.position;
	// 		// 	direction.Normalize();
	// 		//
	// 		// 	float rotateAmount = Vector3.Cross(direction, transform.forward).y;
	// 		//
	// 		// 	rb.angularVelocity = -rotateAmount * rotateSpeed;
	// 		// 	rb.velocity = transform.forward * speed;
	// 		// }
	// 		// var targetRotation = Quaternion.LookRotation
	// 	}
	// }


}
