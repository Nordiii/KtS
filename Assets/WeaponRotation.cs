using UnityEngine;
using System.Collections;

public class WeaponRotation : MonoBehaviour {

    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
        //Waffe wird immer zum Mauszeiger gerdreht
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rota = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.rotation = rota;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        
        //Wenn die Waffe nach Links zeigt wird das Sprite geflipt damit die Waffe nicht auf dem Kopf steht
        if(transform.eulerAngles.z <= 180)
        {
            spriteRenderer.flipX = true;

        }
        else
        {
            spriteRenderer.flipX = false;
        }
	}
}
