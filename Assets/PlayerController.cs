using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	bool goRight, isLerping, keyUp;
	ParticleSystem myParticles;

	public float speed = 1f, lerpTime, maxLerpTime = 1f, rotationAngle = 45f;
	public Vector3 leftPath, rightPath;


	float tempAngle;

	// Use this for initialization
	void Start () {
		goRight = true;
		isLerping = true;
		lerpTime = 1f;
		myParticles = GetComponentInChildren<ParticleSystem> ();
		var em = myParticles.emission;
		em.enabled = false;
		tempAngle = rotationAngle;
	}
	
	// Update is called once per frame
	void Update () {
		lerpTime += Time.deltaTime * 1.25f;

		if (Input.GetKeyDown(KeyCode.Space)) {
			tempAngle = rotationAngle;
			keyUp = false;
			goRight = !goRight;
			isLerping = true;
			if (lerpTime >= 1f) {
				lerpTime = 0f;
			} else {
				lerpTime = 1f - lerpTime;
			}
		}

		//Debug.Log("" + transform.rotation.y);
		//Debug.Log("" + Quaternion.Angle(Quaternion.identity, transform.rotation));
		Debug.Log("" + transform.rotation.ToAngleAxis());


		if (Input.GetKeyUp(KeyCode.Space)) {
			keyUp = true;
			//tempAngle = Mathf.Min( tempAngle * lerpTime, rotationAngle);
		}

		if (keyUp) {
			if (goRight && (transform.rotation.y < 0f)) {
				keyUp = false;
				isLerping = false;
			} else if (!goRight && (transform.rotation.y > 0f)) {
				keyUp = false;
				isLerping = false;				
			}
		}
		if (!isLerping) {
			
			transform.SetPositionAndRotation(transform.position - transform.forward * speed * Time.deltaTime, transform.rotation);
			return;
		}

		if (goRight) {
			transform.SetPositionAndRotation(transform.position - transform.forward * speed * Time.deltaTime, Quaternion.AngleAxis( Mathf.Lerp(tempAngle, -tempAngle, lerpTime), Vector3.up));
			//transform.Rotate( new Vector3(0f, Mathf.Lerp(-rotationAngle, rotationAngle, lerpTime), 0f));
			//transform.rotation.eulerAngles = Vector3.Lerp(Vector3.up * rotationAngle, Vector3.up * -rotationAngle, lerpTime);	
		} else {
			transform.SetPositionAndRotation(transform.position - transform.forward * speed * Time.deltaTime, Quaternion.AngleAxis(Mathf.Lerp(-tempAngle, tempAngle, lerpTime), Vector3.up));
			//transform.Rotate( new Vector3(0f, Mathf.Lerp(rotationAngle, -rotationAngle, lerpTime), 0f));
			//transform.rotation.eulerAngles = Vector3.Lerp(Vector3.up * -rotationAngle, Vector3.up * rotationAngle, lerpTime);
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
