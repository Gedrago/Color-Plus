using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class CubeControllerScript : MonoBehaviour {
	GameScript scriptReference ;
	public int IndividualX, IndividualY; 
	public bool Active; 
	public bool NextCube= false ; 
	 

	// Use this for initialization

	void Start () {
		scriptReference = GameObject.Find ("Controller").GetComponent<GameScript> ();
	}

	void OnMouseDown (){
		if(!NextCube){
			scriptReference.ProcessClick (gameObject, IndividualX, IndividualY,  gameObject.GetComponent<Renderer>().material.color,Active); 
		}
	}  
	void OnMouseEnter ( ){
		if (!NextCube && gameObject.GetComponent<Renderer>().material.color != Color.black && gameObject.GetComponent<Renderer>().material.color != Color.white && !Active ) {
			gameObject.transform.localScale += new Vector3(0.1F, 0.1F, 0);
		}


	}
	void OnMouseExit (){
		if (!NextCube && gameObject.GetComponent<Renderer>().material.color != Color.black && gameObject.GetComponent<Renderer>().material.color != Color.white && !Active  ) {
			gameObject.transform.localScale -= new Vector3(0.1F, 0.1F, 0);
		}
		 
		
	}

	// Update is called once per frame
	void Update () {
		 

	}
}

 
 