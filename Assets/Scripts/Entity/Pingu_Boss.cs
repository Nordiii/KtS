using UnityEngine;
using System.Collections;

public class Pingu_Boss : Enemy {
	public Animator childAnimator;
	public Transform trans;
	public GameObject child;
	//Animator childAnimator;

	void Awake(){
	//	childAnimator = GetComponentInChildren<Animator>();
	}

	public override void turnKatana(){
		trans.localRotation = new Quaternion (trans.localRotation.x * -1.0f,
			trans.localRotation.y,trans.localRotation.z,trans.localRotation.w *-1.0f);
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
