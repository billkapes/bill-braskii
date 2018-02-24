using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject snow, tree;
	PlayerController player;
	float spawnTime, clusterSpawnTime;
	float snowSpawnDistance = 50f;
	int clusterCount;
	public Transform lastTree;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();	
		spawnTime = 0f;
		clusterSpawnTime = 0f;
		clusterCount = 10;
	}
	
	// Update is called once per frame
	void Update () {
		spawnTime += Time.deltaTime;
		clusterSpawnTime += Time.deltaTime;
		if (spawnTime > 3f) {
			spawnTime = Random.Range (0.5f, 2.5f);
			Instantiate (tree, new Vector3 (Random.Range(-4f, 4f), 1f, player.transform.position.z - 20f), Quaternion.identity);
		}

		if (clusterSpawnTime > 3f) {
			SpawnCluster ();
			clusterSpawnTime = 0f;
		}

		if (player.transform.position.z <= -snowSpawnDistance + 30f) {
			Instantiate (snow, -Vector3.forward * snowSpawnDistance, Quaternion.identity);
			snowSpawnDistance += 50f;
		}

		if (Input.GetKeyDown(KeyCode.Z)) {
			SpawnCluster ();
		}
	}

	void SpawnCluster() {

		lastTree = Instantiate (tree, new Vector3 (Random.Range(-4f, 4f), 1f, player.transform.position.z - 30f), Quaternion.identity).GetComponent<Transform>();

		for (int i = 0; i < clusterCount; i++) {
			lastTree = Instantiate (tree, new Vector3 (lastTree.position.x + Random.Range(-4f, 4f), 1f, lastTree.position.z + Random.Range(-3f, 0f)), Quaternion.identity).GetComponent<Transform>();

		}
		clusterCount = Random.Range (10, 30);

	}
}
