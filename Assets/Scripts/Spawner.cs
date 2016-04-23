using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject[] enemy;

	public float spawnMin = 1f; //wann spawnt nächster Gegner
	public float spawnMax = 3f;

	float time;
	Animator animator_;

	void Awake(){
		animator_ = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void release(int count){
		StartCoroutine(Spawn (count));
	}

	IEnumerator Spawn(int counter){
		Debug.Log ("Active");
		animator_.SetTrigger ("Open");
		while (counter-- != 0) {
			yield return new WaitForSeconds(2);
			Instantiate (enemy [Random.Range (0, enemy.Length)], transform.position, Quaternion.identity);
		}
		animator_.SetTrigger ("Close");
	}
}
