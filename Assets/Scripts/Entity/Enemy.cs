using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float moveSpeed = 7.0f;
	public Transform target;
	public float minDistance = 1f;
	float distance;
	Animator animator_;
	Transform myTransform_;
	AudioSource audiosource_;

	float tempx;
	float tempy;
	float movex;
	float movey;
	bool up = false, down = false, right = false, left = false;

	Vector3 zombiePosition;
	Vector3 playerPosition;
	Vector3 position;

	// Use this for initialization
	void Start () {
		if (target == null) {
			if (GameObject.FindWithTag ("Player") != null) {
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
		myTransform_ = GetComponent<Transform> ();
		audiosource_ = GetComponent<AudioSource> ();
		animator_ = GetComponent<Animator> ();
		audiosource_.Play();

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
		playerPosition = Camera.main.WorldToScreenPoint(target.position);
		zombiePosition = Camera.main.WorldToScreenPoint(myTransform_.position);

		position = playerPosition-  zombiePosition;
	//	Debug.Log (playerPosition);


		if ((position.y > 0 && position.y > betrag (position.x)) && !up) {
			up = true;
			down = false;
			left = false;
			right = false;
			animator_.SetTrigger ("up");
		}
		if ((position.y < 0 && position.y < nbetrag (position.x)) && !down) {
			up = false;
			down = true;
			left = false;
			right = false;
			animator_.SetTrigger ("down");
		}
		if ((position.x < 0 && position.x < nbetrag (position.y)) && !left) {
			up = false;
			down = false;
			left = true;
			right = false;
			animator_.SetTrigger ("left");
		}
		if ((position.x > 0 && position.x > betrag (position.y)) && !right) {
			up = false;
			down = false;
			left = false;
			right = true;
			animator_.SetTrigger ("right");
		}
	}

	float betrag(float zahl){
		if (zahl < 0) {
			return -1 * zahl;
		} 
		return zahl;
	}
	float nbetrag(float zahl){
		if (zahl > 0) {
			return -1 * zahl;
		} 
		return zahl;
	}
}
