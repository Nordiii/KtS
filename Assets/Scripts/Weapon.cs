using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public string weapon_Name;

    public float reload_Time_Second;
    public float shoot_interval_Second;

    public bool infinite_Ammunition;

    public int weapon_Reload_Ammunition;
    public int weapon_Magazin_Ammunition;
    public int weapon_Magazin_size;

    public GameObject bullet;

    private float current_threshold;
    private float shoot_timer;
    private bool able_to_shoot;

    private Vector2 start;
    private Vector2 target;

	void Start ()
    {
        current_threshold = 0;
        shoot_timer = 0;
       
        able_to_shoot = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!able_to_shoot)
        {
            if (!(shoot_timer >= current_threshold))
            {
                shoot_timer += Time.deltaTime;
                //Debug.Log(shoot_timer);
            }
            else
            {
                able_to_shoot = true;
                shoot_timer = 0;
            }
               
        }
        Debug.Log(Input.GetMouseButton(0));
        if (able_to_shoot && Input.GetMouseButton(0))
            shot();

        
	}

    void shot()
    {
       // Debug.Log("TEST");
        able_to_shoot = false;
        current_threshold = shoot_interval_Second;

        GameObject projectile = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
       // Debug.Log(projectile.transform.position);
        return;
    }


}
