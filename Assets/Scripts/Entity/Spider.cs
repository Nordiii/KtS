using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {
	public float moveSpeed = 20.0f;
	public Transform target;
	public float minDistance = 1f;
	float distance;
	Transform myTransform_;
	public GameObject fleck;
	[HideInInspector] 
	public float jump_timer = 2;
	SpriteRenderer spriteRend_;
	BoxCollider2D box_;
	[HideInInspector]
	public Animator animator_;


	bool dead = false, jumping = false;

	// Use this for initialization
	void Start () {
		if (target == null) {
			if (GameObject.FindWithTag ("Player") != null) {
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
		myTransform_ = GetComponent<Transform> ();
		spriteRend_ = GetComponent<SpriteRenderer>();
		box_ = GetComponent<BoxCollider2D> ();
		animator_ = GetComponent<Animator> ();

	}

	// Update is called once per frame
	void Update () {
		if (!dead) {
			if (target == null) {
				return;
			}
			animator_.SetBool ("jumping", jumping);
			distance = Vector2.Distance (myTransform_.position, target.position);

			if (minDistance < distance) {
				Vector3 toTargetVector = target.position - myTransform_.position;
				float zRotation = (Mathf.Atan2 (toTargetVector.y, toTargetVector.x) * Mathf.Rad2Deg) - 90.0f;
				myTransform_.rotation = Quaternion.Euler (new Vector3 (0, 0, zRotation));
				if (jump_timer >= 2) {
					jumping = true;
					myTransform_.position =
					Vector2.MoveTowards (transform.position, target.position, moveSpeed * Time.deltaTime);
					StartCoroutine (resetJump());
				}
			}

			jump_timer += Time.deltaTime;
		}
	}

	public void death(){
		dead = true;
		box_.enabled = false;
		spriteRend_.sortingOrder = -5;
		Instantiate (fleck, transform.position, Quaternion.identity);
        gameObject.GetComponent<EnemyWeapon>().enabled = false;

    }

	IEnumerator resetJump(){
		yield return new WaitForSeconds (0.8f);
		jump_timer = 0f;
		jumping = false;
	}
}
