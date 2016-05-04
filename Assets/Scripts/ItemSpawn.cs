using UnityEngine;
using System.Collections;

public class ItemSpawn : MonoBehaviour
{

    public GameObject[] weapon_Prefab;
    public float spawn_time;

    private bool empty = true;
    private float timer = 0;


	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (timer >= spawn_time && empty)
        {
            Random rnd = new Random();
            GameObject item = (GameObject)Instantiate(weapon_Prefab[Random.Range(0,weapon_Prefab.Length)], transform.position - new Vector3(0, 0.5F, 0), transform.rotation);
            item.GetComponent<Weapon>().enabled = false;
            item.GetComponent<WeaponRotation>().enabled = false;

            item.GetComponent<Transform>().localScale = new Vector3(2, 2, 1);
            empty = false;
        }
        timer += Time.deltaTime;
	}
}
