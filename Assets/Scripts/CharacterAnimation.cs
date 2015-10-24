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
		moveVelocity = FindObjectOfType<PlayerInput>().moveVelocity;
		if(FindObjectOfType<PlayerInput>() != null){
			character.SetFloat("Velocity", moveVelocity.magnitude);
		}
	}
}

