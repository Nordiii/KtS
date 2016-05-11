using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float moveSpeed = 7.0f;
	public Transform target;
	public float minDistance = 1f;
	public AudioClip deathclip;
	float distance;
	Animator animator_;
	Transform myTransform_;
	AudioSource audiosource_;
	[HideInInspector]
	public BoxCollider2D box_;

	public float attack_timer;
	float tempx;
	float tempy;
	float movex;
	float movey;
	[HideInInspector]
	public bool up = false, down = false, right = false, left = false, dead = false;

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
		box_ = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		attack_timer += Time.deltaTime;
		if (dead) {
			return;
		}
		if (target == null) {
			return;
		}
		distance = Vector2.Distance (transform.position, target.position);
		
		if (minDistance < distance) {
			transform.position =
				Vector2.MoveTowards (transform.position, target.position, moveSpeed * Time.deltaTime);
		} else {
			slash ();
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
			turnKatana ();

		}
		if ((position.x > 0 && position.x > betrag (position.y)) && !right) {
			up = false;
			down = false;
			left = false;
			right = true;
			animator_.SetTrigger ("right");
			turnKatana ();
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
	 
	public virtual void death(){
		dead = true;
		box_.enabled = false;
	}

	public void deathSound(){
		audiosource_.PlayOneShot (deathclip,3f);
	}

	public virtual void slash(){
	}
	public virtual void turnKatana(){
	}
}
