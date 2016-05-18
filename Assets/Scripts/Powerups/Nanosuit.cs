using UnityEngine;
using System.Collections;

public class Nanosuit : MonoBehaviour {

    public float set_def;
    public float def_up_time;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pickedUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<HitpointDamage>().setDef(def_up_time, set_def, true);
        Destroy(gameObject);
    }
}
