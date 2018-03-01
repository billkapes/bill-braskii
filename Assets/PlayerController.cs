 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	bool goRight, isLerping, keyUp;
	ParticleSystem myParticles;

	public float speed = 1f, lerpTime, maxLerpTime = 1f, rotationAngle = 45f, rotateFactor = 1f, minAngle = 25f;
	public Vector3 leftPath, rightPath;


	float tempAngle, startAngle;

	// Use this for initialization
	void Start () {
		goRight = true;
		isLerping = true;
		lerpTime = 1f;
		myParticles = GetComponentInChildren<ParticleSystem> ();
		var em = myParticles.emission;
		em.enabled = false;
		startAngle = 45f;
	}
	
	// Update is called once per frame
	void Update () {
		lerpTime += Time.deltaTime * rotateFactor;

		if (Input.GetKeyDown(KeyCode.Space)) {
			//if (keyUp && lerpTime < 0.5f) return;

			startAngle = transform.eulerAngles.y;
			startAngle = (startAngle > 180) ? startAngle - 360 : startAngle;
			goRight = !goRight;
			isLerping = true;

			if (lerpTime >= 1f) {
				lerpTime = 0f;
			} else {
				lerpTime = 1f - lerpTime;
			}
		}

//		Debug.Log("transform.rotation.y " + transform.rotation.y);
//		Debug.Log("quaternion.angle " + Quaternion.Angle(Quaternion.Euler(Vector3.zero), transform.rotation));
//		float angle = transform.eulerAngles.y;
//		angle = (angle > 180) ? angle - 360 : angle;
//		Debug.Log("transform.eulerangles.y " + angle);

		//Debug.Log("transform.rotation.toangleaxis" + transform.rotation.ToAngleAxis());


		if (Input.GetKeyUp(KeyCode.Space)) {
			keyUp = true;
		}

		if (keyUp) {
			float angle = transform.eulerAngles.y;
			angle = (angle > 180) ? angle - 360 : angle;

			if (goRight && (angle < -minAngle)) {
				keyUp = false;
				isLerping = false;
			} else if (!goRight && (angle > minAngle)) {
				keyUp = false;
				isLerping = false;				
			}
		}
		if (!isLerping) {
			
			transform.SetPositionAndRotation(transform.position - transform.forward * speed * Time.deltaTime, transform.rotation);
			return;
		}

		if (goRight) {
			transform.SetPositionAndRotation(transform.position - transform.forward * speed * Time.deltaTime,
												Quaternion.AngleAxis( Mathf.Lerp(startAngle, -rotationAngle, lerpTime), Vector3.up)
											);
		} else {
			transform.SetPositionAndRotation(transform.position - transform.forward * speed * Time.deltaTime,
												Quaternion.AngleAxis(Mathf.Lerp(startAngle, rotationAngle, lerpTime), Vector3.up)
											);
		}

		//transform.position += -transform.forward * speed * Time.deltaTime;

	



	}


	void OnTriggerEnter(Collider other) {
		//Debug.Log ("hit trigger");
	}

	void OnCollisionEnter(Collision other) {
		Debug.Log ("hit collider");

	}
		
}
