using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


    private float attack_timer = 0F;
    public float attack_speed = 4F;

    //Projektil mit Ammunition script
    public GameObject bullet;

    public AudioClip weapon_sound;

    public float weapon_Sound_volume = 0.5F;

    // Update is called once per frame
    void Update ()
    {
        attack_timer += Time.deltaTime;

        if (attack_timer >= attack_speed)
        {
            attack();
            attack_timer = 0;
        }
           

        
    }

    private void attack()
    {
        Vector2 normed = getNormedDirectionVector();
        GameObject projectile = (GameObject)Instantiate(bullet, transform.position, transform.rotation);

        projectile.SendMessage("setDirection", normed, 0);
    }


    private Vector2 getNormedDirectionVector()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player == null)
            return new Vector2(0, 0);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        //Normalisiert den richtungs Vektor 
        Vector2 direction = normaVector(
            getDirectionVector(gameObject.transform.position,
           playerPos 
            ));

        return direction;
    }
    private Vector2 getDirectionVector(Vector2 current, Vector2 target)
    {
        return
             new Vector2
             (
                 calculatePoint(current.x, target.x),
                 calculatePoint(current.y, target.y)
             );
    }

    private float calculatePoint(float p1, float p2)
    {
        return p2 - p1;
    }

    private Vector2 normaVector(Vector2 richtung)
    {
        float I = Mathf.Sqrt(Mathf.Pow(richtung.x, 2) + Mathf.Pow(richtung.y, 2));
        return new Vector2(richtung.x / I, richtung.y / I);
    }
}
