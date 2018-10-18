using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//needs to be serialized b/c new class is not known to unity
[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

	private Rigidbody rb;
	private AudioSource audioSource;
	private GameController gameController;
	public Boundary boundary;
	public float speed;
	public float tilt;
	public GameObject shot;
	//multi shot
	public Transform[] shotSpawns;
	public Transform singleShotSpawn;
	public float fireRate;

	private float nextFire;


	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		// if not found, log error msg
		if (gameControllerObject == null)
		{
			Debug.Log("Cannot find 'GameController' script");
		}
	}

	void Update ()
	{
		//this will fire a shot every frame
		// Instantiate (shot, shotSpawn.position, shotSpawn.rotation);

		// limit fire rate
		// if (Input.GetButton("Fire1") && Time.time > nextFire)
		// {
		// 	nextFire = Time.time + fireRate;
		//
		// 	// gives reference to instantiate object as clone
		// 	// GameObject clone = Instantiate(project, transform.position, transform.rotation) as GameObject;
		// 	Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		// 	// talks to audio source and will play the audio clip
		// 	audioSource.Play ();
		// }

		// change weapon display
		if (Input.GetButton("Weapon1"))
		{
			gameController.ChangeWeapon ("Normal");
		}
		if (Input.GetButton("Weapon2"))
		{
			gameController.ChangeWeapon ("Rapid");
		}
		if (Input.GetButton("Weapon3"))
		{
			gameController.ChangeWeapon ("Multi");
		}
		if (Input.GetButton("Weapon4"))
		{
			gameController.ChangeWeapon ("Multi-Rapid");
		}
		if (Input.GetButton("Weapon5"))
		{
			gameController.ChangeWeapon ("Homing");
		}

		// TODO implement switch case ?
		//
		// // if weapon selected is normal, fire normal shot
		// if (gameController.weapon == "Normal")
		// {
		// 	if (Input.GetButton("Fire1") && Time.time > nextFire)
		// 	{
		// 		nextFire = Time.time + fireRate;
		//
		// 		// gives reference to instantiate object as clone
		// 		// GameObject clone = Instantiate(project, transform.position, transform.rotation) as GameObject;
		// 		Instantiate(shot, singleShotSpawn.position, singleShotSpawn.rotation);
		// 		// talks to audio source and will play the audio clip
		// 		audioSource.Play ();
		// 	}
		// }
		//
		// // shoot shot every frame
		// if (gameController.weapon == "Rapid")
		// {
		// 	if (Input.GetButton("Fire1"))
		// 	{
		// 		Instantiate (shot, singleShotSpawn.position, singleShotSpawn.rotation);
		// 	}
		// }
		//
		//
		// // multishot
		// // DEBUG bolts do not collide with each other, instead they are off the x-z plane and fly off and get destroyed by boundary
		// // because of ships tilt, turned off tilt for quickfix
		// if (gameController.weapon == "Multi")
		// {
		// 	if (Input.GetButton("Fire1") && Time.time > nextFire)
		// 	{
		// 		nextFire = Time.time + fireRate;
		// 		foreach (var shotSpawn in shotSpawns)
		// 		{
		//
		// 			// gives reference to instantiate object as clone
		// 			// GameObject clone = Instantiate(project, transform.position, transform.rotation) as GameObject;
		// 			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		// 			// talks to audio source and will play the audio clip
		// 			audioSource.Play ();
		// 		}
		// 	}
		// }

		switch (gameController.weapon)
      {
      case "Normal":
				if (Input.GetButton("Fire1") && Time.time > nextFire)
				{
					nextFire = Time.time + fireRate;

					// gives reference to instantiate object as clone
					// GameObject clone = Instantiate(project, transform.position, transform.rotation) as GameObject;
					Instantiate(shot, singleShotSpawn.position, singleShotSpawn.rotation);
					// talks to audio source and will play the audio clip
					audioSource.Play ();
				}
        break;
      case "Rapid":
				if (Input.GetButton("Fire1"))
				{
					Instantiate (shot, singleShotSpawn.position, singleShotSpawn.rotation);
				}
	      break;
      case "Multi":
				if (Input.GetButton("Fire1") && Time.time > nextFire)
				{
					nextFire = Time.time + fireRate;
					foreach (var shotSpawn in shotSpawns)
					{

						// gives reference to instantiate object as clone
						// GameObject clone = Instantiate(project, transform.position, transform.rotation) as GameObject;
						Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
						// talks to audio source and will play the audio clip
						audioSource.Play ();
					}
				}
        break;
      case "Multi-Rapid":
				if (Input.GetButton("Fire1"))
				{
					foreach (var shotSpawn in shotSpawns)
					{

						// gives reference to instantiate object as clone
						// GameObject clone = Instantiate(project, transform.position, transform.rotation) as GameObject;
						Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
						// talks to audio source and will play the audio clip
					}
				}
        break;
      case "Homing":
				// TODO MAKE HOME LASERS
				break;
      default:
        break;
      }


	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
		// Mathf lets you calculate and clamp restricts min and max values
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

}
