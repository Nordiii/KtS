using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager gm;
	public float score = 0f;
	public Spawner[] spawners; 
	public bool paused = false;
	public int countEnemy;

	public GameObject UIGamePaused;	
	public GameObject UIStartWaves;


	bool start_ = false;

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
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale > 0f) {
				UIGamePaused.SetActive (true);
				Time.timeScale = 0f;
				paused = true;
			} else {
				Time.timeScale = 1f;
				UIGamePaused.SetActive (false);
				paused = false;
			}
		}
		if (start_) {
			start_ = false;
			for(int i = 0; i < spawners.Length; i++){
				spawners [i].release(countEnemy);
			}
		}
	}
		
	public void start(){
		UIStartWaves.SetActive (false);
		start_ = true;
	}
}
