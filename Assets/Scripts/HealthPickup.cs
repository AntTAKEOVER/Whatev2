using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	void Update(){
		transform.Rotate(0, 80*Time.deltaTime, 0);
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("Colllision!");
		Destroy(this.gameObject);
		FindObjectOfType<PlayerInput>().modifyHealth += 20;
	}
}
