using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {

    public float ammunition_Speed = 100;
    public float ammunition_Damage = 10;
    public float time_Till_Destroy = 5;

    public bool aoe = false;
    public float aoe_radius;
    public bool enemyWeapon = false;

    public AudioClip hit_sound;
    public float hit_sound_volume = 0.5f;
    public float time_to_wait = 1;
    // Use this for initialization
    void Start ()
    {
     
        
    }
    
    private float timeToDestroyCounter = 0;
    // Update is called once per frame


    void Update ()
    {

        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity);
        timeToDestroyCounter += Time.deltaTime;

        if(timeToDestroyCounter >= time_Till_Destroy)
        {
            timeToDestroyCounter = 0;
            Destroy(gameObject);
        }
    }

    void setDirection(Vector2 direction)
    {
        Debug.Log(direction);
        direction = new Vector2(direction.x * ammunition_Speed, direction.y * ammunition_Speed);
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction);
   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (!enemyWeapon)
            if (!aoe)
            {
                if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Kakerlake"))
                {


                    //Fügt die geschwindigkeit hinzu bevor das gameObject zerstört wird (knockback)
                    collision.rigidbody.AddForce(collision.relativeVelocity);
                    collision.gameObject.SendMessage("hitRecived", ammunition_Damage);


                }
            }
            else if (aoe)
            {
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(aoe_radius, aoe_radius);
                GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Kakerlake"))
                    collision.gameObject.SendMessage("hitRecived", ammunition_Damage);
               
                GetComponent<Animator>().SetTrigger("explode");

                StartCoroutine(playSound());

                return;
            }

        if(enemyWeapon)
        {
         
            if(collision.gameObject.CompareTag("Player"))
            {
              
                collision.rigidbody.AddForce(collision.relativeVelocity);
                collision.gameObject.SendMessage("hitRecived", ammunition_Damage);
                collision.gameObject.SendMessage("hitBySpider");
            }
           
        }
    
        Destroy(gameObject);
    }

    /*
     * Aoe Waffen schaden
     * 
     */
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("weaponspawner"))
            return;


        if (coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("Kakerlake"))
        {
            coll.gameObject.SendMessage("hitRecived", ammunition_Damage);
        }
        
    }


    public void aoeFinished()
    {
        Destroy(gameObject);
    }


    IEnumerator playSound()
    {
         yield return new WaitForSeconds(time_to_wait);
        GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(hit_sound, hit_sound_volume);
    }


    }
