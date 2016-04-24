using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour {
    
    //Name der Waffe 
    public string weapon_Name;

    //Wie lange er zum nachladen braucht
    public float reload_Time_Second;

    //Wie groß die Zeitabstände zwischen den einzelnen Schüssen ist
    public float shoot_Interval_Second;

    //Wenn true läd er immer komplett nach ohne munition zu verbrauchen
    public bool infinite_Ammunition;

    //Wieviel er noch reloaden kann
    public int weapon_Reload_Ammunition = 0;

    //Wieviel er gerade geladen hat
    public int weapon_Magazin_Ammunition = 0;

    //Anzahl der Schüsse die man nachladen kann
    public int weapon_Magazin_Size = 10;

    //Wie viele Kugeln gespawnt werden mit einem Schuss 
    public int bullets_Per_Shoot = 1;

    //Wenn eine Waffe nicht nur gerade aus schießen soll
    public float weapon_Spread = 0;

   

    //Projektil mit Ammunition script
    public GameObject bullet;

    public AudioClip weapon_sound;

    public float weapon_Sound_volume = 0.5F;

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

        if (infinite_Ammunition)
            GameObject.Find("UIAmmunition").GetComponent<Text>().text = weapon_Magazin_Ammunition + " / \u221E";
        else
            GameObject.Find("UIAmmunition").GetComponent<Text>().text = weapon_Magazin_Ammunition + " / " + weapon_Reload_Ammunition;
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
                if (infinite_Ammunition)
                    GameObject.Find("UIAmmunition").GetComponent<Text>().text = weapon_Magazin_Ammunition + " / \u221E";
                else
                    GameObject.Find("UIAmmunition").GetComponent<Text>().text = weapon_Magazin_Ammunition + " / " + weapon_Reload_Ammunition;
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

            for(int shoot = 0;shoot < bullets_Per_Shoot;shoot++)
            {
                Vector2 normed = getNormedDirectionVector();
                GameObject projectile = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
            //Quaternion.Euler(0,0, Vector2.Angle(transform.position, normed)-30
            // Debug.Log();
            // projectile.
            projectile.SendMessage("setDirection", normed, 0);
                   
            }


            weapon_Magazin_Ammunition--;
            GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(weapon_sound, weapon_Sound_volume);
            if (infinite_Ammunition)
                GameObject.Find("UIAmmunition").GetComponent<Text>().text = weapon_Magazin_Ammunition+ " / \u221E";
            else
                GameObject.Find("UIAmmunition").GetComponent<Text>().text = weapon_Magazin_Ammunition + " / " + weapon_Reload_Ammunition;
        }


        if(weapon_Magazin_Ammunition == 0)
        {
            reload = true;
            current_threshold = reload_Time_Second;
            GameObject.Find("UIAmmunition").GetComponent<Text>().text = "Reloading...";
        }
        return;
    }


    private Vector2 mousePos;
    private Vector2 bulletPos;
    private Vector2 movement;
    private Vector2 getNormedDirectionVector()
    {
        //Normalisiert den richtungs Vektor damit egal wie die Entfernung zur Maus ist die Kugel die gleiche geschwindigkeit hat.
        Vector2 direction = normaVector(
            getDirectionVector(gameObject.transform.position , 
            Camera.main.ScreenToWorldPoint(Input.mousePosition)
            ));

        return normSpread(direction);
    }

    private Vector2 getDirectionVector(Vector2 current,Vector2 target)
    {
       return 
            new Vector2
            (
                calculatePoint(current.x , target.x ) ,
                calculatePoint(current.y, target.y) 
            );
    }

    private float generateSpread()
    {

            return Random.Range(-weapon_Spread, weapon_Spread);

    }

    private Vector2 normSpread(Vector2 without_Spread)
    {
        return normaVector(new Vector2(without_Spread.x + generateSpread(), without_Spread.y + generateSpread()));
    }

    private float calculatePoint(float p1,float p2)
    {
        return p2 - p1;
    }

    private Vector2 normaVector(Vector2 richtung)
    {
        float I = Mathf.Sqrt(Mathf.Pow(richtung.x, 2) + Mathf.Pow(richtung.y, 2));
        return new Vector2(richtung.x / I, richtung.y / I);
    }



}
