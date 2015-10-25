using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

	public Text instruction;
	public int levelToLoad;

	// Use this for initialization
	void Start () {
		instruction.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(){
		instruction.text = "Press 'E' to play Level " + levelToLoad;
		if(Input.GetKeyDown(KeyCode.E)){
			//Debug.Log("Hit Key!");
			Application.LoadLevel("Level" +levelToLoad);
		}
	}

	void OnTriggerExit(){
		instruction.text = "";
	}
}
