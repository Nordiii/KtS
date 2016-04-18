using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public float moveSpeed = 0f;
	private float movex = 0f;
	private float movey = 0f;

	Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		movex = Input.GetAxis ("Horizontal");
		movey = Input.GetAxis ("Vertical");
		rigidbody2D.velocity = new Vector2 (movex * moveSpeed, movey * moveSpeed);
	}
}

