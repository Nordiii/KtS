﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float moveSpeed = 7.0f;
	public Transform target;
	public float minDistance = 1f;
	float distance;
	Animator animator_;


	float tempx;
	float tempy;
	float movex;
	float movey;
	bool up = false, down = false, right = false, left = false;

	// Use this for initialization
	void Start () {
		if (target == null) {
			if (GameObject.FindWithTag ("Player") != null) {
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
		animator_ = GetComponent<Animator> ();

		tempx = transform.position.x;
		tempy = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			return;
		}
		distance = Vector2.Distance (transform.position, target.position);
		
		if (minDistance < distance) {
			transform.position =
				Vector2.MoveTowards (transform.position, target.position, moveSpeed * Time.deltaTime);
		}
		turnAround ();
	}

	void turnAround(){
		tempx = movex;
		tempy = movey;
		movex = transform.position.x;
		movey = transform.position.y;

		if ((tempy < movey && (movey - tempy > movex - tempx )) && !up) {
			up = true;
			down = false;
			left = false;
			right = false;
			animator_.SetTrigger ("up");
		}
		if ((tempy > movey && (movey - tempy < movex - tempx )) && !down) {
			up = false;
			down = true;
			left = false;
			right = false;
			animator_.SetTrigger ("down");
		}
		if ((tempx > movex && (movex - tempx < movey - tempy )) && !left) {
			up = false;
			down = false;
			left = true;
			right = false;
			animator_.SetTrigger ("left");
		}
		if ((tempx < movex && (movex - tempx > movey - tempy )) && !right) {
			up = false;
			down = false;
			left = false;
			right = true;
			animator_.SetTrigger ("right");
		}

	}
}
