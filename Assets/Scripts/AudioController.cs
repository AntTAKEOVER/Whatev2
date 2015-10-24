using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour {

	public AudioMixerSnapshot Intense;
	public AudioMixerSnapshot Calm;
	private bool lowheath;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if
	}

	void ChangeMusic () {
		Intense.TransitionTo(.01f);
	}
}
