using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	float offset;
	public Transform thePlayer;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController> ().transform;
		offset = transform.position.z - thePlayer.position.z;
	}

	void LateUpdate () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, thePlayer.transform.position.z + offset);

	}
}
