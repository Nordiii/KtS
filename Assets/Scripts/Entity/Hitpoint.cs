using UnityEngine;
using System.Collections;

public class Hitpoint : MonoBehaviour {

    public int hitpoints = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (hitpoints <= 0)
            Destroy(gameObject);
	}

    void hitRecived(int damage)
    {
        hitpoints -= damage;
    }
}
