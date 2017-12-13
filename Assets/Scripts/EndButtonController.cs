using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndButtonController : MonoBehaviour {


	 
	public void ChangeScene (string ChangeScene) {
		SceneManager.LoadScene (ChangeScene); 
	}

}