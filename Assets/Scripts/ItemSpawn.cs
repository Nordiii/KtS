using UnityEngine;
using System.Collections;

public class ItemSpawn : MonoBehaviour
{

    public GameObject[] weapon_Prefab;
    public float spawn_time;

    private GameObject current_item;

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
            
            current_item = (GameObject)Instantiate(weapon_Prefab[Random.Range(0,weapon_Prefab.Length)], transform.position - new Vector3(0, 0.5F, 0), transform.rotation);

            if(current_item.tag.Equals("Weapon"))
            {

                current_item.GetComponent<Weapon>().enabled = false;
                current_item.GetComponent<WeaponRotation>().enabled = false;
            }

            current_item.transform.localScale = new Vector3(2, 2, 1);
            empty = false;
        }
        timer += Time.deltaTime;
	}

    
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (current_item == null || !coll.tag.Equals("Player"))
            return;
        if(coll.tag.Equals("Player")&& current_item.tag.Equals("Weapon") && !empty)
        {
            Vector3 position = coll.transform.GetChild(1).gameObject.transform.position;
            Destroy(coll.transform.GetChild(1).gameObject);
            current_item.transform.parent = coll.transform;
            current_item.transform.position = position;
            current_item.GetComponent<Weapon>().enabled = true;
            current_item.GetComponent<WeaponRotation>().enabled = true;
            
            foreach(GameObject item in weapon_Prefab)
            {
                if (current_item.name.Equals(item.name+"(Clone)"))
                {
                    current_item.transform.localScale = item.transform.lossyScale;
                    break;
                }
            }
        }
        else if(coll.tag.Equals("Player") && !empty)
        {
            current_item.SendMessage("pickedUp");
        }




        empty = true;
        timer = 0;
        current_item = null;
    }
}
