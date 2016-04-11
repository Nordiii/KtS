using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {

    public float ammunition_speed;
    public float ammunition_damage;

	// Use this for initialization
	void Start () {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        if (rb == null)
            Debug.Log("Kein Rigidbody vorhanden");

        rb.AddForce(new Vector2(0, 700));

    }

    // Update is called once per frame
    void Update ()
    {
       
        

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("LOL");
    }

}
