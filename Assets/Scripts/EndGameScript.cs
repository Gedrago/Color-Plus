using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour {
	//GameScript scriptReference ;
	 
	public Text WinText; 
	public Text ScoreText; 
	void Start () {
		int score = PlayerPrefs.GetInt ("SCORE");
		if (score > 0) {
			//print you won with text
			WinText.text = "Victory!";
			ScoreText.text = "Score: " + score ; 
		} 
		else {
			WinText.text = "Defeat!";
			ScoreText.text = "Score: " + score ; 

		}
	 
	}

}