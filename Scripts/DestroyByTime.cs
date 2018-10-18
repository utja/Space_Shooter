using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	public float lifetime;

	void Start ()
	{
		// destroy takes in a second param for time
		// add this script to the vfx so explosions are destroyed after
		// lifeTime seconds
		Destroy (gameObject, lifetime);
	}
}
