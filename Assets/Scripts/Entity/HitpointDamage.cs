using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitpointDamage : MonoBehaviour {

    public int hitpoints = 1;
    public int damage = 1;
    public float attack_speed = 2F;

	bool death = false;

    private float attack_timer = 0F;
    private Animator animation_;
	private SpriteRenderer renderer_;
    private float def = 1;
    private float def_up_time;
    private float def_timer;
    private bool def_increased = false;
	GameManager gamemanager_;

	public GameObject healthUI;
    

    public void setDef(float def_up_time,float def,bool def_increased)
    {
        this.def_up_time = def_up_time;
        this.def = def;
        this.def_increased = def_increased;
        def_timer = 0;

        if (def_increased)
            renderer_.color = new Color(100f, 100f, 0f);
        else
            StartCoroutine(changeColorDefault());
    }


    public void addHitpoints(int hp)
    {
        if (hitpoints == 10)
            return;

        hitpoints += hp;
        renderer_.color = new Color(0f, 100f, 0f);
        StartCoroutine(changeColorDefault());
        healthUI.SendMessage("setHealthbar", hitpoints);
    }

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
        def_timer += Time.deltaTime;
        if(def_increased && def_timer >= def_up_time)
            setDef(0, 1,false);
        else if(def_increased)
            renderer_.color = new Color(100f, 100f, 0f);

	}

    void hitRecived(int damage)
    {
		if (!death) {
			hitpoints -= (int)(damage/def);
			if (hitpoints <= 0 && !gameObject.CompareTag ("Player"))
            {
                death = true;
				gamemanager_.onedead ();
                if(animation_ != null)
				    animation_.SetTrigger ("death");
				gameObject.SendMessage ("death");
			} else if (hitpoints <= 0 && gameObject.CompareTag ("Player"))
            {
                death = true;
                gameObject.SendMessage ("death");
				healthUI.SendMessage ("setHealthbar",hitpoints);
			} else if (gameObject.CompareTag ("Player"))
            {
				renderer_.color = new Color (100f,0f,0f);
				StartCoroutine (changeColorDefault ());
				healthUI.SendMessage ("setHealthbar",hitpoints);
			}
		}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (attack_timer >= attack_speed &&!death)
        {
            if (collision.collider.gameObject.CompareTag("Player"))
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


    void OnTriggerStay2D(Collider2D collision)
    {
        if (attack_timer >= attack_speed)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.SendMessage("hitRecived", damage);
          
                attack_timer = 0;
            }
            if (collision.gameObject.CompareTag("Kakerlake") && !gameObject.CompareTag("Enemy") && !gameObject.CompareTag("Kakerlake"))
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
