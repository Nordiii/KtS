using UnityEngine;
using System.Collections;

public class HitpointDamage : MonoBehaviour {

    public int hitpoints = 1;
    public int damage = 1;
    public float attack_speed = 2F;

	bool death = false;

    private float attack_timer = 0F;
    private Animator animation_;
	private SpriteRenderer renderer_;

	GameManager gamemanager_;
	// Use this for initialization
	void Start ()
    {
        animation_ = GetComponent<Animator>(); 
		gamemanager_ = GameManager.gm;
		renderer_ = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        attack_timer += Time.deltaTime;
	}

    void hitRecived(int damage)
    {
		if (!death) {
			hitpoints -= damage;
			if (hitpoints <= 0 && !gameObject.CompareTag ("Player")) {
				death = true;
				gamemanager_.onedead ();
				animation_.SetTrigger ("death");
				gameObject.SendMessage ("death");
			} else if (hitpoints <= 0 && gameObject.CompareTag ("Player")) {
				gameObject.SendMessage ("death");
			} else if (gameObject.CompareTag ("Player")) {
				renderer_.color = new Color (100f,0f,0f);
				StartCoroutine (changeColorDefault ());
			}
		}
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
            if (collision.collider.gameObject.CompareTag("Kakerlake") && !gameObject.CompareTag("Enemy") && !gameObject.CompareTag("Kakerlake"))
            {
                collision.gameObject.SendMessage("hitRecived", damage);

                attack_timer = 0;
            }
            

        }
    }

	public void destroy(){
		Destroy (gameObject);
	}

	IEnumerator changeColorDefault(){
		yield return new WaitForSeconds (0.5f);
		renderer_.color = new Color(1,1,1);
	}
}
