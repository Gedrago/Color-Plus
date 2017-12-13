using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonController : MonoBehaviour {

	 
	
	// Update is called once per frame
	public void ChangeScene (string ChangeScene) {
		SceneManager.LoadScene (ChangeScene); 
	}
}
