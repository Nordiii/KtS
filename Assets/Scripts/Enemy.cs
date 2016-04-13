using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float moveSpeed = 7.0f;
	public Transform target;
	public float minDistance = 1f;

	// Use this for initialization
	void Start () {
		if (target == null) {
			if (GameObject.FindWithTag ("Player") != null) {
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}	
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			return;
		}

		transform.LookAt (target);
		if (Vector3.Distance (transform.position, target.position) >= minDistance) {
			transform.position += transform.forward * moveSpeed * Time.deltaTime;
		}

	}
}