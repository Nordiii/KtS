using UnityEngine;
using System.Collections;

public class SwitchToDefaultWeapon : MonoBehaviour {

    // Use this fo
    public GameObject default_weapon;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void switchOut()
    {

        Vector3 position = GameObject.Find("Player").GetComponentInChildren<Weapon>().gameObject.transform.position;
        Destroy(GameObject.Find("Player").GetComponentInChildren<Weapon>().gameObject);
        GameObject current_item = (GameObject)Instantiate(default_weapon, transform.position - new Vector3(0, 0.5F, 0), transform.rotation);
        current_item.transform.parent = GameObject.Find("Player").transform;
            current_item.transform.localScale = default_weapon.transform.lossyScale;
            current_item.transform.position = position;
    }
}
