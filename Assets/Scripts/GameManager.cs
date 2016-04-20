using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager gm;
	public float score = 0f;

	void Awake(){
		if (gm == null) {
			gm = this.GetComponent<GameManager> ();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
