using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public Text creditText;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startNewGame(){
		Application.LoadLevel(1);
	}

	public void onQuit(){
		Application.Quit();
	}

	public void onCredit(){

		Debug.Log("Credits!");
	}


}
