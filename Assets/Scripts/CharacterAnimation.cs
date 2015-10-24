using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {
	private Animator character;

	// Use this for initialization
	void Start () {
		character = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveVelocity = FindObjectOfType<PlayerInput>().moveVelocity;
		character.SetFloat("Speed", moveVelocity.magnitude);
	}
}
