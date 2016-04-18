using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {

    public float ammunition_Speed = 100;
    public float ammunition_Damage = 10;
    public float time_Till_Destroy = 5;

	// Use this for initialization
	void Start ()
    {
     
        
    }
    
    private float timeToDestroyCounter = 0;
    // Update is called once per frame


    void Update ()
    {
        timeToDestroyCounter += Time.deltaTime;

        if(timeToDestroyCounter >= time_Till_Destroy)
        {
            timeToDestroyCounter = 0;
            Destroy(gameObject);
        }
    }

    void setDirection(Vector2 direction)
    {
        direction = new Vector2(direction.x * ammunition_Speed, direction.y * ammunition_Speed);
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("hitRecived", ammunition_Damage);
            //Fügt die geschwindigkeit hinzu bevor das gameObject zerstört wird (knockback)
            collision.rigidbody.AddForce(collision.relativeVelocity);
        }
        Destroy(gameObject);
    }

}
