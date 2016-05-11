using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {

    public float ammunition_Speed = 100;
    public float ammunition_Damage = 10;
    public float time_Till_Destroy = 5;
    public bool aoe = false;
    public float aoe_radius;


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
        //Das Projektil wird in die richtige Richtung gedreht
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rota = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.rotation = rota;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!aoe)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Kakerlake"))
            {


                //Fügt die geschwindigkeit hinzu bevor das gameObject zerstört wird (knockback)
                collision.rigidbody.AddForce(collision.relativeVelocity);
                collision.gameObject.SendMessage("hitRecived", ammunition_Damage);

               
            }
        }
        else if(aoe)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(aoe_radius, aoe_radius);
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Kakerlake"))
                collision.gameObject.SendMessage("hitRecived", ammunition_Damage);

            return;
        }

        Destroy(gameObject);
    }

    /*
     * Aoe Waffen schaden
     * 
     */
    public void OnTriggerEnter2D(Collider2D coll)
    { 
        
        if (coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("Kakerlake"))
        {
            coll.gameObject.SendMessage("hitRecived", ammunition_Damage);
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        //Destroy(gameObject);
    }


    }
