using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private GameController gameController;

	void Start ()
	{
		// find instance of game controller for each instance of the asteroid
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		// if found, set gameController to the gamecontroller in the hierarchy
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

	void OnTriggerEnter(Collider other)
	{
		// checks to see if asteroid is colloding with boundary
		// when game starts, asteroid will be destroyed if this is not checked
		// if (other.tag == "Boundary" || other.tag == "Enemy")

		// bolt is shot from enemy as well, so we don't want enemy shooting others and asteroids
		// tag enemy, asteroid and bolt as Enemy
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy"))
		{
			return;
		}
		// instantiates asteroid explosion vfx and check for explosion
		// if have explosion, instantiate explosion
		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}
		// checks to see if asteroid collides with player
		if (other.tag == "Player")
		{
			//instantiates player and asteroid explosion vfx
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			// game over when player collides with asteroid
			gameController.GameOver ();
		}

		gameController.AddScore (scoreValue);
		// destroy marks object to be destroyed at end of frame so order does not matter
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
