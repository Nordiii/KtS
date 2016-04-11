using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public float moveSpeed = 3.0f; //speed player moves
	public float gravtiy = 9.81f;

	private CharacterController myController;

	Vector3 movementZ;
	Vector3 movementx;
	Vector3 movement;

	// Use this for initialization
	void Start () {
		myController = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}



	void Move()
	{
		movementZ = Input.GetAxis ("Vertical") * Vector3.forward * moveSpeed * Time.deltaTime;
		movementx = Input.GetAxis ("Horizontal") * Vector3.right * moveSpeed * Time.deltaTime;
		movement = transform.TransformDirection (movementZ + movementx);
		movement.y -= gravtiy * Time.deltaTime;

		myController.Move (movement);
	}
}
