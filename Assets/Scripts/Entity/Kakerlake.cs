﻿using UnityEngine;
using System.Collections;

public class Kakerlake : MonoBehaviour {
	public float moveSpeed = 7.0f;
	public Transform target;
	public float minDistance = 1f;
	float distance;
	Transform myTransform_;
	SpriteRenderer spriteRend_;
	public GameObject fleck;
	[HideInInspector]
	public Animator animator_;


	bool dead = false;

	// Use this for initialization
	void Start () {
		if (target == null) {
			if (GameObject.FindWithTag ("Player") != null) {
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
		myTransform_ = GetComponent<Transform> ();
		spriteRend_ = GetComponent<SpriteRenderer>();
		animator_ = GetComponent<Animator> ();

	}

	// Update is called once per frame
	void Update () {
		if (!dead) {
			if (target == null) {
				return;
			}
			distance = Vector2.Distance (myTransform_.position, target.position);

			if (minDistance < distance) {
				Vector3 toTargetVector = target.position - myTransform_.position;
				float zRotation = (Mathf.Atan2 (toTargetVector.y, toTargetVector.x) * Mathf.Rad2Deg) - 90.0f;
				myTransform_.rotation = Quaternion.Euler (new Vector3 (0, 0, zRotation));
				myTransform_.position =
				Vector2.MoveTowards (transform.position, target.position, moveSpeed * Time.deltaTime);
			}
		}
	}

	public void death(){
		dead = true;
		spriteRend_.sortingOrder = -5;
		Instantiate (fleck, transform.position, Quaternion.identity);
	}
}
