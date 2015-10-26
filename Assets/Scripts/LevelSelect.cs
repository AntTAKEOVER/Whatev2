using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

	public Text instruction;
	public int levelToLoad;
	bool hasEnteredTrigger;

	// Use this for initialization
	void Start () {
		instruction.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(hasEnteredTrigger){
			if(Input.GetKeyDown(KeyCode.E)){
				//Debug.Log("Hit Key!");
				Application.LoadLevel("Level" +levelToLoad);
			}
		}
	}

	void OnTriggerStay(){
		instruction.text = "Press 'E' to play Level " + levelToLoad;
		hasEnteredTrigger = true;
	}

	void OnTriggerExit(){
		instruction.text = "";
		hasEnteredTrigger = false;
	}
}
