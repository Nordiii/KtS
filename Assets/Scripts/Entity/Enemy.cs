﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float moveSpeed = 7.0f;
	public Transform target;
	public float minDistance = 1f;
	float distance;

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
		distance = Vector2.Distance(transform.position, target.position);
		
		if(minDistance<distance){
			transform.position =
				Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); }
		}
		
}