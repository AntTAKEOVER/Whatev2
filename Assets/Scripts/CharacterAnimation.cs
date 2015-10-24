using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {
	private Animator character;
	Vector3 moveVelocity;

	// Use this for initialization
	void Start () {
		character = gameObject.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

		if(FindObjectOfType<PlayerInput>() != null){
			moveVelocity = FindObjectOfType<PlayerInput>().moveVelocity;
			character.SetFloat("Velocity", moveVelocity.magnitude);
		}
	}
}

