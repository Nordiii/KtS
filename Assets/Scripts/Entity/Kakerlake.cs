using UnityEngine;
using System.Collections;

public class Kakerlake : MonoBehaviour {
	public float moveSpeed = 7.0f;
	public Transform target;
	public float minDistance = 1f;
	float distance;
	Transform myTransform_;

	// Use this for initialization
	void Start () {
		if (target == null) {
			if (GameObject.FindWithTag ("Player") != null) {
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
		myTransform_ = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		if (target == null) {
			return;
		}
		distance = Vector2.Distance (myTransform_.position, target.position);

		if (minDistance < distance) {
			Vector3 toTargetVector = target.position - myTransform_.position;
			float zRotation = ( Mathf.Atan2( toTargetVector.y, toTargetVector.x )*Mathf.Rad2Deg ) -90.0f;
			myTransform_.rotation  = Quaternion.Euler(new Vector3(0,0,zRotation));
			myTransform_.position =
				Vector2.MoveTowards (transform.position, target.position, moveSpeed * Time.deltaTime);
		}
	}
}
