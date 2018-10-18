using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	//make an array of hazards
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text weaponText;
	public string weapon;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		weaponText.text = "";// + weapon;
		score = 0;
		weapon = "Normal";
		//sets initial text and score to zero
		UpdateScore ();
		UpdateWeapon ();
		// must explicity use startcoroutine and not normal SpawnWaves function
		StartCoroutine (SpawnWaves ());
	}

	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	// for coroutine, cannot return void
	IEnumerator SpawnWaves ()
	{
		// short pause for player to get ready for the game
		yield return new WaitForSeconds (startWait);

		// wrap for loop so that the spawning continues to run while true
		// until loop is broken
		while (true)
		{
			//spawns number of asteroids specified by hazardCount
			for (int i = 0; i < hazardCount; i++)
			{
				// select random hazard from array of hazards
				GameObject hazard = hazards[Random.Range (0, hazards.Length)];
				//spawns asteroid in xyz position (random x within given min/max range)
				// x is random so that asteroid spawns randomly left to right
				// y is set to 0 because asteroid has to be on same plane as player
				// z is set to above game view so that it looks like asteroid is coming down (or player moving up)
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);

				// need coroutine using yield instead
				// WaitForSeconds (spawnWait); -interval between each asteroid being instantiated
				// i.e. if spawnwait 1 second, an asteroid is generated every second
				// if spawnwait is 0.5 sec, two asteroids are generated every second
				yield return new WaitForSeconds (spawnWait);
			}

			// time between each wave or between each for loop (generating asteroids)
			yield return new WaitForSeconds (waveWait);

			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void ChangeWeapon (string weaponName)
	{
		weapon = weaponName;
		UpdateWeapon ();

	}

	void UpdateWeapon ()
	{
		weaponText.text = "Current Weapon: " + weapon;
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}
