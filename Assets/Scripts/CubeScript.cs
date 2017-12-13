using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class CubeScript : MonoBehaviour {
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
			gameObject.transform.localScale *= 1.2f; 
		}

		
	}
	void OnMouseExit (){
		if (!NextCube && gameObject.GetComponent<Renderer>().material.color != Color.black && gameObject.GetComponent<Renderer>().material.color != Color.white && !Active  ) {
			gameObject.transform.localScale /= 1.2f; 
		}
		 
		
	}

	// Update is called once per frame
	void Update () {
		 

	}
}

 
 