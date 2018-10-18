using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{

    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;

    // make enemy move towards player
    private Transform playerTransform;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent <Rigidbody> ();
        currentSpeed = rb.velocity.z;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
				// DEBUG for some issue the current speed is not properly set
				// currentSpeed = -5;
				// ISSUE FIXED - order of scripts on inspector matters, mover needs to be above EvasiveManeuver
				// so that mover sets velocity forward then EvasiveManeuver calls on the z component of velocity
        StartCoroutine (Evade ());
    }

    IEnumerator Evade()
		// set a target value along x axis and move it towards it over a period of time
		// make it a coroutine
    {
				// set range with startwait and get random from that range
        yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

				// loop through dodging maneuver
        while (true)
        {
						// // point on x axis - choose from 1 to max number set by dodge
						// // -Mathf.Sign will reverse the sign (positive to negative or negative to positive)
						// // so the ship will move to opposite side (from left to right or right to left
						// // because our game has x axis from -6 to 6
            // targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);

            // make enemy move towards player
          if (playerTransform == null)
          {
            targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
          }
          targetManeuver = playerTransform.position.x;
          // wait while maneuvering
					yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
					// set so there is no target
					targetManeuver = 0;
					// wait until maneuver begins again
          yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
        }
    }

    void FixedUpdate ()
    {
				// set newManeuver(?)
        float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);

				// set rb velocity, z is set to currentSpeed or original speed coming down screen
				rb.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
				// clamp position of enemy ship to within game boundaries
				rb.position = new Vector3
        (
            Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );
				// add tild to enemy ship when moving
        rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
