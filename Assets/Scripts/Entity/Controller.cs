using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public float moveSpeed = 0f;
	private float movex = 0f;
	private float movey = 0f;

	Rigidbody2D rigidbody2D;
	Animator animator_;
	Transform transform_;

	Vector3 playerPosition;
	Vector3 mouse;
	Vector3 position;

	bool up = false, down = false, left = false, right = false;
	bool idle = false;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		animator_ = GetComponent<Animator> ();
		transform_ = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		movex = Input.GetAxis ("Horizontal");
		movey = Input.GetAxis ("Vertical");
		if (movey == 0 && movex == 0) {
			idle = true;
			up = down = left = right = false;
		} else {
			idle = false;
		}
		rigidbody2D.velocity = new Vector2 (movex * moveSpeed, movey * moveSpeed);

		turn ();
	}

	void turn(){
		mouse = Input.mousePosition;
		playerPosition = Camera.main.WorldToScreenPoint(transform_.position);

		position = mouse - playerPosition;

		animator_.SetBool ("idle", idle);
		if (!idle) {
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

