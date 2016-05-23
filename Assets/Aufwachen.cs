using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Aufwachen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pressEndbild()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void pressAufwachen()
    {
        GetComponent<Image>().enabled = false;
        GameObject endBild = GameObject.Find("Endbild");
        endBild.GetComponent<Image>().enabled = true;
    }


}
