using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class healthbar : MonoBehaviour {
	public Sprite health0;
	public Sprite health1;
	public Sprite health2;
	public Sprite health3;
	public Sprite health4;
	public Sprite health5;
	public Sprite health6;
	public Sprite health7;
	public Sprite health8;
	public Sprite health9;
	public Sprite health10;

	private Image healthBar;

	// Use this for initialization
	void Start () {
		healthBar = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}	

	public void setHealthbar(int life){
		switch (life) {
		case 1: healthBar.sprite = health1; 
			break;
		case 2: healthBar.sprite = health2; 
			break;
		case 3: healthBar.sprite = health3; 
			break;
		case 4:	healthBar.sprite = health4; 
			break;
		case 5: healthBar.sprite = health5; 
			break;
		case 6: healthBar.sprite = health6; 
			break;
		case 7: healthBar.sprite = health7; 
			break;
		case 8: healthBar.sprite = health8; 
			break;
		case 9: healthBar.sprite = health9; 
			break;
		case 10: healthBar.sprite = health10; 
			break;
		default:
			healthBar.sprite = health0; 
			break;
		}
	}

}
