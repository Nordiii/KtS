using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject[] enemy;

	public int counter = 20;
	public float spawnMin = 1f; //wann spawnt nächster Gegner
	public float spawnMax = 3f;

	float time;

	// Use this for initialization
	void Start () {
		StartCoroutine(Spawn ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Spawn(){
		while (counter-- != 0) {
			Instantiate (enemy [Random.Range (0, enemy.Length)], transform.position, Quaternion.identity);
			yield return new WaitForSeconds(1);
		}
	}
}
