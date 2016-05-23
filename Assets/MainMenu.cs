using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Canvas exitMenu;
    public Canvas creditScreen;
    public Button start;
    public Button credits;
    public Button beenden;
    public Text highscore;
    public Text score;
    public int scoreInt = 0;

	// Use this for initialization
	void Start () {

        exitMenu.enabled = false;
        creditScreen.enabled = false;
        score.text = scoreInt.ToString();
        SetHighscore(PlayerPrefs.GetFloat("scorePref"));
        PlayerPrefs.SetFloat("scorePref", 0);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    //Wenn der "Beenden" Button benutzt wird
    public void BeendenPress()
    {
        exitMenu.enabled = true;
        start.enabled = false;
        beenden.enabled = false;
    }

    //Wenn der "Credit" Button benutzt wird
    public void CreditPress()
    {
        creditScreen.enabled = true;
        start.enabled = false;
        beenden.enabled = false;
    }

    //Wenn der "X" Button benutzt wird
    public void ExitCreditPress()
    {
        creditScreen.enabled = false;
        start.enabled = true;
        beenden.enabled = true;
    }

    //Wenn der "Nein" Button benutzt wird
    public void NeinPress()
    {
        exitMenu.enabled = false;
        start.enabled = true;
        beenden.enabled = true;
    }

    //Wenn der "Neues Spiel" Button benutzt wird
    public void Starten()
    {
        SceneManager.LoadScene("level1");
    }

    //Wenn der "Ja" Button benutzt wird
    public void Beenden()
    {
        Application.Quit();
    }

    //Setzt einen int Wert als neuen Highscore
    public void SetHighscore(float x)
    {
        if(x > PlayerPrefs.GetFloat("Highscore"))
        {
            PlayerPrefs.SetFloat("Highscore", x);
            
        }
        score.text = PlayerPrefs.GetFloat("Highscore").ToString();
    }
        
}
