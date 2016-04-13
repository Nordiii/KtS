using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {

    public float ammunition_Speed = 100;
    public float ammunition_Damage = 10;
    public float time_Till_Destroy = 5;

    private Vector2 mousePos;
    private Vector2 bulletPos;
    private Vector2 movement;

	// Use this for initialization
	void Start ()
    {
     
        //Speichert die Mausposition mit den koordinaten auf der Spielkarte
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //speichert die koordinate wo die kugel gespawnt ist
        bulletPos = gameObject.transform.position;

        //Sucht nach dem rigidbody des Objektes um später die Kraft hinzuzufügen
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        
        
        if (rb == null)
            Debug.Log("Kein Rigidbody vorhanden");
        //Normalisiert den richtungs Vektor damit egal wie die Entfernung zur Maus ist die Kugel die gleiche geschwindigkeit hat.
        Vector2 norma = normaVector(new Vector2(calculateX(), calculateY()));

        //Rechnet die gewünschte Geschwindigkeit ein
        norma = new Vector2(norma.x * ammunition_Speed, norma.y * ammunition_Speed);
        
        
        rb.AddForce(norma);
        
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

    private Vector2 normaVector(Vector2 richtung)
    {
        float I = Mathf.Sqrt(Mathf.Pow(richtung.x,2)+Mathf.Pow(richtung.y,2));
        return new Vector2(richtung.x / I, richtung.y / I);
    }

    private float calculateX()
    {
        return mousePos.x - bulletPos.x;
    }

    private float calculateY()
    {
        return mousePos.y - bulletPos.y;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Fügt die geschwindigkeit hinzu bevor das gameObject zerstört wird (knockback)
        collision.rigidbody.AddForce(collision.relativeVelocity);
        Destroy(gameObject);
    }

}
