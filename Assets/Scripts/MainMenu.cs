using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
//	public Text creditText;
	public GameObject instructions;


	// Use this for initialization
	void Start () {
//		creditText.text = " ";
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
		//Stuff for credits here!
	
	}

	public void onInstruction(){
		//instructions.enabled = true;
		instructions.SetActive(true);
		//stuff for instructions here!
	}

	public void exitInstrucitons(){
		instructions.SetActive(false);
	}


}
