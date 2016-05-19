using UnityEngine;
using System.Collections;

public class Pingu_Boss : Enemy {
	public Animator childAnimator;
	public Transform trans;
	public GameObject child;
	SpriteRenderer spriteRend_;

	//Animator childAnimator;

	bool wasLeft = false, wasRight = true;
	void Awake(){
	//	childAnimator = GetComponentInChildren<Animator>();
	}

	public void turnKatana(){
		transform.Rotate(new Vector3(0,180,0));

	//	trans.localRotation = new Quaternion (trans.localRotation.x * -1.0f,
	//		trans.localRotation.y,trans.localRotation.z,trans.localRotation.w *-1.0f);
	}

	public override void turnAround(){
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
			animator_.SetTrigger ("right");
			if(!wasLeft){
				turnKatana ();
				wasLeft = true;
				wasRight = false;
			}
		}
		if ((position.x > 0 && position.x > betrag (position.y)) && !right) {
			up = false;
			down = false;
			left = false;
			right = true;
			animator_.SetTrigger ("right");
			if(!wasRight){
				turnKatana ();
				wasRight = true;
				wasLeft = false;
			}
		}
	}

	public override void slash(){
		if (attack_timer >= 2) {
			if(Random.Range(0f,1f) < 0.5f){
			childAnimator.SetTrigger ("slash1t");
			} else {
				childAnimator.SetTrigger ("slash2t");
			}
			attack_timer = 0;
		}
	}

	public void destroyMe(){
		Destroy (gameObject);
	}

	public override void death(){
		dead = true;
		box_.enabled = false;
		Destroy(child);
	}
}
