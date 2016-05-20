using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager gm;
	public float score = 0f;
	public Spawner[] spawners; 
	public bool paused = false;
	public int waves = 3;
	int countwaves = 0;
	public int countEnemy;
	public string levelAfterGameOver;

	public GameObject UIGamePaused;	
	public GameObject UIStartWaves;
    public GameObject UIAmmunitonCounter;

	int livingEnemies;
    bool start_ = false;
    bool ongoing = false;
    public bool getOngoing()
    {
        return ongoing;
    }

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
		if (start_)
        {
            ongoing = true;
			start_ = false;
			for(int i = 0; i < spawners.Length; i++){
				spawners [i].release(countEnemy);
				livingEnemies = countEnemy * spawners.Length;
			}
			if (countwaves == waves) {
				livingEnemies++;
				spawners [0].releaseBoss ();
			}
			countEnemy += 4;
		}
	}
		
	public void start(){
		if(waves == countwaves){
			nextLevel();
		}
		UIStartWaves.SetActive (false);
		start_ = true;
        
        countwaves++;
	}

	void nextLevel(){
		SceneManager.LoadScene (levelAfterGameOver);
	}

	public void onedead(){
		livingEnemies--;
		if(livingEnemies <= 0)
        {
            UIStartWaves.SetActive (true);
            ongoing = false;
		}
	}
}
