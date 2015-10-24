using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		Debug.Log ("Colllision!");
		Destroy(this.gameObject);
		FindObjectOfType<PlayerInput>().modifyHealth += 20;
	}
}
