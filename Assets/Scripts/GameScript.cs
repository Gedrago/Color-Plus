using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
 

public class GameScript : MonoBehaviour {
	//define the variables
	public int score = 0; 
	float GameLength = 60; 
	float TurnLength= 2;
	int Turn= 0;
	int gridLenY = 5;
	int gridLenX =8 ; 
	Vector3 NextCubePosition = new Vector3 (8, 10, 0);
	public GameObject[,] CubeArray; 
	public GameObject CubePre; 
	GameObject NextCube; 
	new Vector3 CubePosition;
	Color[] ColorArray = { Color.blue, Color.green, Color.red, Color.yellow, Color.magenta };
	int row; 
	bool CubeActive; 
	bool NextCubeDisplaced;
	GameObject ActiveCube = null;  
	int RainbowPoints =5;
	int SameColorPoints= 10; 
	public Text ScoreText; 
	public Text NextCubeText; 
	public Text TimeLeft; 
	bool GameOver = false ;

	// Use this for initialization
	void Start () {
		//start values of variables 
		CreateCubeArray ();	 
	}

	//create the array of the cubes 
	void CreateCubeArray (){
		CubeArray = new GameObject[gridLenX, gridLenY];
		//make grid
		for (int y = 0; y < gridLenY ; y ++){
			for (int x = 0; x < gridLenX  ; x++) {
				CubePosition = new Vector3 (x *2, y * 2, 0);
				CubeArray[x,y]= Instantiate (CubePre, CubePosition, Quaternion.identity);
				CubeArray [x, y].GetComponent<CubeScript> ().IndividualX = x;
				CubeArray [x, y].GetComponent<CubeScript> ().IndividualY = y;
			}
		}
	}


	//create the nextcube that appears ontop 
	void GetNextCube(){
		NextCube = Instantiate ( CubePre , NextCubePosition, Quaternion.identity);
		int RandomColor = Random.Range (0, ColorArray.Length);
		NextCube.GetComponent<Renderer> ().material.color = ColorArray [RandomColor];
		NextCube.GetComponent<CubeScript> ().NextCube = true; 
	}


	//find the an available white space to place the 'Next Cube'
	GameObject FindAvailableCube(int y){
		List<GameObject> whiteCubes = new List<GameObject> () ;
		 
			//checking which one is white on the row 
		//int NumWhiteCubes =0;
		for (int x= 0; x< gridLenX; x++){
			if(CubeArray[x,y].GetComponent<Renderer>().material.color == Color.white){
				whiteCubes.Add(CubeArray[x, y]);	
				
			}
			
		}
		return PickWhiteCubes (whiteCubes);
	}

	// picks random white cube from a list of gameobject white cubes 
	GameObject PickWhiteCubes (List<GameObject> whiteCubes){
		if (whiteCubes.Count == 0){
			return null; 
		}
		//pick a random cube from the list of cubes 
		return whiteCubes[Random.Range(0,whiteCubes.Count)]; 
	}

	//find available white spaces to black out later 
	GameObject FindAvailableCube(){
		List<GameObject> whiteCubes = new List<GameObject> () ;

		//checking which one is white on the row 
		//int NumWhiteCubes =0;
		for (int y = 0; y < gridLenY; y++) {
			for (int x = 0; x < gridLenX; x++) {
				if (CubeArray [x, y].GetComponent<Renderer> ().material.color == Color.white) {
					whiteCubes.Add (CubeArray [x, y]);	

				}

			}
		}
		return PickWhiteCubes (whiteCubes);
	}

	//color a cube by giving this method which cube to color and what color to color it with 
	void SetCubeColor (GameObject mycube, Color color ){
		if ( mycube == null) {
			EndGame (false);
			SceneManager.LoadScene (2);
		} 
		else {
			mycube.GetComponent<Renderer>().material.color =  color;
			Destroy (NextCube);
			NextCube = null; 

		}

	}
	// transport the color of the next cube to the available cube generated from the method 'FindAvailableCube(int y)'
	void TransportCube(int y){
		//goes over each of the columns of a specific and checks which one is white and which one is black
		GameObject WhiteCube = FindAvailableCube(y);
		SetCubeColor (WhiteCube, NextCube.GetComponent<Renderer>().material.color ); 
	}
	// turn an available cube generated from the method 'FindAvailableCube()' and turn the color to black(blackout the position)
	void AddBlackCube (){
		GameObject WhiteCube = FindAvailableCube();
		//use a color value greater than 15 
		SetCubeColor (WhiteCube, Color.black ); 
	}  


	//makes the game respond to keyboard inputs 
	void ProcessKeyboard() {
		//Detects what keyboard input was given and returns the number 
		int Numpressed= 0; 
		if (Input.GetKeyDown (KeyCode.Alpha1) ||  Input.GetKeyDown (KeyCode.Keypad1)){
			Numpressed = 1; 
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) ||  Input.GetKeyDown (KeyCode.Keypad2)){
			Numpressed = 2; 
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)  ||  Input.GetKeyDown (KeyCode.Keypad3)){
			Numpressed = 3; 
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)  ||  Input.GetKeyDown (KeyCode.Keypad4)){ 
			Numpressed = 4; 
		}
		if (Input.GetKeyDown(KeyCode.Alpha5)  ||  Input.GetKeyDown (KeyCode.Keypad5)){
			Numpressed = 5; 
		}
		//if we still have another cube and the player pressed a key
		if (NextCube!= null && Numpressed!=0 ){
			TransportCube (Numpressed - 1);
			
		}

	}


	//makes the game respond to keyboard inputs 
	public void ProcessClick (GameObject clickedCube, int IndividualX , int IndividualY , Color CubeColor, bool Active  ){

		if (CubeColor!= Color.white && CubeColor!= Color.black) {
			if (Active  == false) { 
				if(ActiveCube != null){
					ActiveCube.transform.localScale /= 1.5f;
					ActiveCube.GetComponent<CubeScript> ().Active = false;

				}
				clickedCube.transform.localScale *= 1.5f;
				clickedCube.GetComponent<CubeScript> ().Active = true; 
				ActiveCube = clickedCube; 
				 
			} else   {
				
				clickedCube.transform.localScale /= 1.5f;
				clickedCube.GetComponent<CubeScript> ().Active = false;
				ActiveCube = null; 
			}
		} 
		//If a player clicks a white cube that is adjacent to the active cube (including diagonals),
		//the active cube moves to that location instantly (and remains active). 
		//The location that the active cube just vacated should become a white cube.
		else if (CubeColor == Color.white && ActiveCube!= null) {
			if (Mathf.Abs(clickedCube.GetComponent<CubeScript>().IndividualX - ActiveCube.GetComponent<CubeScript>().IndividualX) <= 1 && Mathf.Abs(clickedCube.GetComponent<CubeScript>().IndividualY - ActiveCube.GetComponent<CubeScript>().IndividualY) <= 1){
				clickedCube.GetComponent<Renderer> ().material.color = ActiveCube.GetComponent<Renderer>().material.color;
				 
				ActiveCube.GetComponent<Renderer> ().material.color = Color.white; 
				clickedCube.transform.localScale *= 1.5f;
				ActiveCube.transform.localScale /= 1.5f;
				ActiveCube.GetComponent<CubeScript>().Active = false; 
				 
				// change the active cube to the new position 
				clickedCube.GetComponent<CubeScript>().Active = true; 
			 

				//keep track of the new active cube 
				ActiveCube = clickedCube;
				//print (clickedCube.GetComponent<CubeScript> ().ColorValue ); 
			}	 
		} 
	}  
		

	//detects whether or not the player made a rainbow plus 
	bool IsRainbowPlus (int x, int y){
		Color a = CubeArray [x, y].GetComponent<Renderer> ().material.color;
		Color b = CubeArray [x+1, y].GetComponent<Renderer> ().material.color;
		Color c = CubeArray [x-1, y].GetComponent<Renderer> ().material.color;
		Color d = CubeArray [x, y+1].GetComponent<Renderer> ().material.color;
		Color e = CubeArray [x, y-1].GetComponent<Renderer> ().material.color;

		if (a == Color.white || a == Color.black ||
			b == Color.white || b == Color.black ||
			c == Color.white || c == Color.black ||
			d == Color.white || d == Color.black ||
			e == Color.white || e == Color.black ){
			return false; 

		}
		if (a != b && a != c && a != d && a != e &&
		    b != c && b != d && b != e &&
		    c != d && c != e &&
		    d != e) {
			return true; 
		} else {
			return false; 
		}
		 
	}

	//detects whether or not the player made a same color plus 
	bool IsSameColorPlus(int x, int y){
		if (CubeArray [x, y].GetComponent<Renderer> ().material.color != Color.black &&
			CubeArray [x, y].GetComponent<Renderer> ().material.color != Color.white &&
			CubeArray [x, y].GetComponent<Renderer> ().material.color == CubeArray [x + 1, y].GetComponent<Renderer> ().material.color &&
		    CubeArray [x, y].GetComponent<Renderer> ().material.color == CubeArray [x - 1, y].GetComponent<Renderer> ().material.color &&
		    CubeArray [x, y].GetComponent<Renderer> ().material.color == CubeArray [x, y + 1].GetComponent<Renderer> ().material.color &&
		    CubeArray [x, y].GetComponent<Renderer> ().material.color == CubeArray [x, y - 1].GetComponent<Renderer> ().material.color) {
			return true; 
		} else {
			return false; 
		}
		
	}

	// blackout the plus when we make a rainbowplus or a black plus 
	void MakeBlackPlus(int x , int y ){
		if(x==0 || y == 0 || x == gridLenX-1 || y == gridLenY-1 ){
			return ; 

		}
		CubeArray [x, y].GetComponent<Renderer> ().material.color = Color.black; 
		CubeArray [x+1, y].GetComponent<Renderer> ().material.color = Color.black; 
		CubeArray [x-1, y].GetComponent<Renderer> ().material.color = Color.black; 
		CubeArray [x, y+1].GetComponent<Renderer> ().material.color = Color.black; 
		CubeArray [x, y-1].GetComponent<Renderer> ().material.color = Color.black; 
		if(ActiveCube!= null && ActiveCube.GetComponent<Renderer>().material.color== Color.black ){
			ActiveCube.transform.localScale /= 1.5f;
			ActiveCube.GetComponent<CubeScript>().Active = false; 
			ActiveCube = null; 

		}
	}

	//score the player according to the kind of plus they achieved 
	void Score (){
		// check for the edge, from 1 to gridlenY-1 because we are not including the edges 
		for (int x= 1; x < gridLenX -1 ; x++){
			for (int y= 1; y < gridLenY -1 ; y++){
				if(IsRainbowPlus(x,y)){
					score += RainbowPoints; 
					MakeBlackPlus (x,y ); 	
				}
				if(IsSameColorPlus (x,y)){
					score += SameColorPoints; 
					MakeBlackPlus (x,y); 
					
				}
			}
		}
	}
	 
	// Ends the game based on if the player won or didnt win and also make all the cubes unresponsive to clicks by treating all of the cubes as nextcube 
	public string EndGame(bool win){
		if (win) {
			NextCubeText.text = "YOU WIN";
			return "You win";

		} else {
			NextCubeText.text = "you lost! Try again :) ";
			return "You lost!"; 
		}
		Destroy (NextCube); 
		NextCube = null; 
		GameOver = true;

		for (int y = 0; y < gridLenY ; y ++){
			for (int x = 0; x < gridLenX  ; x++) {
				CubeArray [x, y].GetComponent<CubeScript> ().NextCube = true; 


			}
		}


	}
		
	// Update is called once per frame
	void Update () { 
		if (Time.timeSinceLevelLoad < GameLength) {
			ProcessKeyboard (); 
			Score ();
			float timeleft = GameLength - Time.timeSinceLevelLoad ; 
			timeleft = Mathf.Round(timeleft * 1f) / 1f;
			if (timeleft <= 10){
				TimeLeft.GetComponent<Text> ().color = Color.red; 
				TimeLeft.text = "Time left: " + timeleft;
			}
			TimeLeft.text = "Time left: " + timeleft;
			if (Time.timeSinceLevelLoad > TurnLength * Turn) {
				Turn++;
				if (NextCube != null) {
					score -= 1;
					if (score < 0) {
						score = 0; 
					}
					AddBlackCube (); 
				}

				GetNextCube ();
			}
			ScoreText.text = "Score: " + score; 
			PlayerPrefs.SetInt ("SCORE", score); 

		} 
		else if (!GameOver){
			if (score > 0) {
				EndGame (true);
			} 
			else {
				EndGame (false);
			}
			SceneManager.LoadScene (2);
		}
		 
	}
}
