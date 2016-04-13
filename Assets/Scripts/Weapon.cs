using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public string weapon_Name;

    public float reload_Time_Second;
    public float shoot_Interval_Second;

    public bool infinite_Ammunition;

    public int weapon_Reload_Ammunition = 1;
    public int weapon_Magazin_Ammunition = 0;
    public int weapon_Magazin_Size = 1;

    public GameObject bullet;

    private float current_threshold;
    private float shoot_timer;
    private bool able_to_shoot;
    private bool reload;

    private Vector2 start;
    private Vector2 target;

	void Start ()
    {
        current_threshold = 0;
        shoot_timer = 0;
        reload = false;
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
            }
            else
            {
                able_to_shoot = true;
                shoot_timer = 0;
                if (reload && infinite_Ammunition)
                    weapon_Magazin_Ammunition = weapon_Magazin_Size;
                else if (reload)
                   if(weapon_Magazin_Size<weapon_Reload_Ammunition)
                    {
                        weapon_Magazin_Ammunition = weapon_Magazin_Size;
                        weapon_Reload_Ammunition -= weapon_Magazin_Size;
                    }
                    else
                    {
                        weapon_Magazin_Ammunition = weapon_Reload_Ammunition;
                        weapon_Reload_Ammunition = 0;
                    }
                reload = false;
            }
               
        }
       //Debug.Log(Input.GetMouseButton(0));
        if (able_to_shoot && Input.GetMouseButton(0))
            shot();

        
	}

   private void shot()
    {
        able_to_shoot = false;

        if (weapon_Magazin_Ammunition != 0)
        {
            current_threshold = shoot_Interval_Second;
            GameObject projectile = (GameObject)Instantiate(bullet, transform.position, transform.rotation);

            weapon_Magazin_Ammunition--;
            
        }


        if(weapon_Magazin_Ammunition == 0)
        {
            reload = true;
            current_threshold = reload_Time_Second;
        }
        return;

    }


}
