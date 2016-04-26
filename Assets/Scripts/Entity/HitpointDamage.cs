using UnityEngine;
using System.Collections;

public class HitpointDamage : MonoBehaviour {

    public int hitpoints = 1;
    public int damage = 1;
    public float attack_speed = 2F;

    private float attack_timer = 0F;
    private Animator animation_;

	GameManager gamemanager_;
	// Use this for initialization
	void Start ()
    {
        animation_ = GetComponent<Animator>(); 
		gamemanager_ = GameManager.gm;
	}
	
	// Update is called once per frame
	void Update ()
    {
        attack_timer += Time.deltaTime;
	}

    void hitRecived(int damage)
    {
        hitpoints -= damage;
		if (hitpoints <= 0 && !gameObject.CompareTag ("Player")) {
			gamemanager_.onedead ();
			animation_.SetTrigger ("death");
		}
		else if (hitpoints <= 0 && gameObject.CompareTag ("Player"))
			gameObject.SendMessage ("death");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (attack_timer >= attack_speed)
        {
            if ( collision.collider.gameObject.CompareTag("Player"))
            {
                collision.gameObject.SendMessage("hitRecived", damage);

                attack_timer = 0;
            }
            if (collision.collider.gameObject.CompareTag("Kakerlake") && !gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.SendMessage("hitRecived", damage);

                attack_timer = 0;
            }
            

        }
    }

	public void destroy(){
		Destroy (gameObject);
	}
}
