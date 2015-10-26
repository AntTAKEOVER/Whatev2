using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class PlayerInput : LivingEntity {

	public bool isInUse = true;
	public float moveSpeed = 5f;
	public Text energyBar;

	bool canFade;
	bool canAddEnergy = true;
	bool CanSubtractEnergy = true;
	bool canChangeColor = true;
	bool isShooting = false;

	public Vector3 moveVelocity;
	public int modifyHealth = 0;


	Camera viewCamera;
	PlayerController controller;
	GunController gunController;


	AudioSource walking;

	AudioSource shooting;
	AudioSource[] audios;

	// Use this for initialization
	protected override void Start () {
		if(isInUse){
		audios = GetComponents<AudioSource>();

		walking = audios[1];

		shooting = audios[3];
	//	BGMusic.Play();
	//	BGMusic.volume = 0.8f;

		base.Start ();
		controller = GetComponent<PlayerController>();
		viewCamera = Camera.main;
		gunController = GetComponent<GunController>();
		}
		else{
			base.Start ();
			controller = GetComponent<PlayerController>();
			viewCamera = Camera.main;
			gunController = GetComponent<GunController>();
		}

	}
	
	// Update is called onceper frame
	void Update () {
		if(isInUse){

		if(transform.position.y < -5){
			Die();
		}

		health = Mathf.Clamp (health, 0, 200);
		energyBar.text = "Energy Level: " + health;

		if(modifyHealth > 0){
			health += modifyHealth;
			modifyHealth = 0;
		}

		if(health <= 0 || health == 200){
			Die ();
		}
		if (CanSubtractEnergy) {
			StartCoroutine (subtractEnergy ());
			CanSubtractEnergy = false;
		}

		//MoveInput
		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		moveVelocity = moveInput.normalized * moveSpeed;
		controller.move(moveVelocity);

		//LookInput
		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up,Vector3.zero);
		float rayDistance;

		if(groundPlane.Raycast(ray, out rayDistance)){
			Vector3 point = ray.GetPoint(rayDistance);
			//Debug.DrawLine(ray.origin, point, Color.red);
			controller.LookAt(point);
		}

		//WeaponInput
		if(Input.GetMouseButtonDown(0)){

			gunController.Shoot();
			if(isShooting == false){
				shooting.Play();
			
				shooting.volume = 0.6f;
				StartCoroutine(canShoot());
			}
			health -= 5;
		}
		else{

		}

		if(moveVelocity.x != 0 || moveVelocity.z != 0){
			walking.mute = false;
			walking.volume = 0.6f;
			if(canAddEnergy){
				StartCoroutine(addEnergy());
				canAddEnergy = false;
			}
		}
		else{
			walking.mute = true;
		}

		if(health <= 50 || health >= 150){

			if(canChangeColor){
			StartCoroutine(cameraFlash());
				canChangeColor = false;
			}
		}
		if(health > 50 || health < 150){

			Camera.main.backgroundColor = Color.black;
		}

		if(health < 50){
			if(canFade){
			FindObjectOfType<AudioController>().lowheath = true;
				canFade = false;
			}
		}
		if(health > 50){
			canFade = true;
		}

	}
		else{
			if(transform.position.y < -5){
				Debug.Log("Fell down NOOB!");
				transform.position = Vector3.zero;
			}
			//MoveInput
			Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
			moveVelocity = moveInput.normalized * moveSpeed;
			controller.move(moveVelocity);
			
			//LookInput
			Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
			Plane groundPlane = new Plane(Vector3.up,Vector3.zero);
			float rayDistance;
			
			if(groundPlane.Raycast(ray, out rayDistance)){
				Vector3 point = ray.GetPoint(rayDistance);
				//Debug.DrawLine(ray.origin, point, Color.red);
				controller.LookAt(point);
				//WeaponInput
				if(Input.GetMouseButtonDown(0)){
					
					gunController.Shoot();
				}
			}
		}
}

	//Flashing BG
	IEnumerator cameraFlash(){
		{
			Camera.main.backgroundColor = Color.black;
		}
		yield return new WaitForSeconds(0.5f);
		{
			Camera.main.backgroundColor = Color.red;
			canChangeColor = true;
		}

	}

	//Shooting
	IEnumerator canShoot(){
		{
			isShooting = true;
		}
		yield return new WaitForSeconds(0.2f);
		{
			isShooting = false;
		}
	}



	//Adding Energy
	IEnumerator addEnergy(){
		 {
			Debug.Log("Enery Movement Subtracted!");
			health -= 2f;

		}
		yield return new WaitForSeconds (1);
		//I know im subtracting but i cant be bothered to change it! :D
		canAddEnergy = true;
	}

	//Constantly subtracting energy
	IEnumerator subtractEnergy(){
		{
			health -= 1f;
			Debug.Log ("EnerySubtracted");
		}
		yield return new WaitForSeconds (1);
		CanSubtractEnergy = true;
	}


}
