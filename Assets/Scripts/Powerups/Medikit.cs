using UnityEngine;
using System.Collections;

public class Medikit : MonoBehaviour {
    public int addHealth = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pickedUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<HitpointDamage>().addHitpoints(addHealth) ;
        
        Destroy(gameObject);
    }
}
