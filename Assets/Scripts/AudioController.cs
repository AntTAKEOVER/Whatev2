using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour {

	public AudioMixerSnapshot Intense;
	public AudioMixerSnapshot Calm;
	public bool lowheath;


	AudioSource pianoAS;
	AudioSource guitarAS;
	AudioSource pianoBB;
	AudioSource guitarBS;
	AudioSource Sad;
	AudioSource warning;
	AudioSource[] audios;

	// Use this for initialization
	void Start () {
		audios = GetComponents<AudioSource>();
		pianoAS = audios[0];
		guitarAS = audios[1];
		pianoBB = audios[2];
		guitarBS = audios[3];
		Sad = audios[4];
		warning = audios[5];
		mainBGMusic();

	}
	
	// Update is called once per frame
	void Update () {

		if(lowheath){
			StartCoroutine(FadeMainToLow());
			lowheath = false;

		}
	}



	void mainBGMusic(){
	
		pianoAS.mute = false;
		//pianoBB.mute = false;
		guitarAS.mute = false;
		//pianoBB.volume = 0.8f;
		pianoAS.volume = 0.5f;
		guitarAS.volume = 0.35f;
	}

	void energyLowMusic(){
		//new WaitForSeconds(3);

		//Debug.Log("Sad Music");
		Sad.mute = false;
		guitarBS.mute = false;
		pianoBB.mute = false;
		warning.mute = false;
		warning.volume = 0.05f;
		guitarBS.volume = 0.3f;
		pianoBB.volume = 0.3f;
		Sad.volume = 2.5f;
	}

	IEnumerator FadeMainToLow(){
		{
	    //Debug.Log("Faded");
		pianoAS.volume = 0.4f;
		//Debug.Log("FADED ONE");
		//pianoBB.volume = 0.2f;
		guitarAS.volume = 0.4f;
		Sad.volume = 0.6f;
		pianoAS.volume = 0.6f;
		guitarBS.volume = 0.6f;
		}
		yield return new WaitForSeconds(2);
		{
		pianoAS.mute = true;
		//pianoBB.mute = true;
		guitarAS.mute = true;
		Sad.volume = 0.8f;
		guitarBS.volume = 0.8f;
		pianoAS.volume = 0.8f;
		}
		new WaitForSeconds(1);
		{
			energyLowMusic();
		}



	}


}
