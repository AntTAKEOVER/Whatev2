using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class PlayerInput : LivingEntity {

	public float moveSpeed = 5f;
	public Text energyBar;

	bool canAddEnergy = true;
	bool CanSubtractEnergy = true;
	bool canChangeColor = true;
	bool isShooting = false;
	public Vector3 moveVelocity;


	Camera viewCamera;
	PlayerController controller;
	GunController gunController;

	AudioSource BGMusic;
	AudioSource walking;
	AudioSource energyLow;
	AudioSource shooting;
	AudioSource[] audios;

	// Use this for initialization
	protected override void Start () {
		audios = GetComponents<AudioSource>();
		BGMusic = audios[0];
		walking = audios[1];
		energyLow = audios[2];
		shooting = audios[3];
		BGMusic.Play();
		BGMusic.volume = 0.8f;

		base.Start ();
		controller = GetComponent<PlayerController>();
		viewCamera = Camera.main;
		gunController = GetComponent<GunController>();


	}
	
	// Update is called onceper frame
	void Update () {
		health = Mathf.Clamp (health, 0, 200);
		energyBar.text = "Energy Level: " + health;

		if(health <= 0){
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
		if(Input.GetMouseButton(0)){

			gunController.Shoot();
			if(isShooting == false){
				shooting.Play();
				BGMusic.volume = 0.5f;
				shooting.volume = 0.6f;
				StartCoroutine(canShoot());
			}


				health -= 5;
			
			
		}
		else{
			BGMusic.volume = 0.8f;
		}

		if(moveVelocity.x != 0 || moveVelocity.z != 0){
			walking.mute = false;
			walking.volume = 0.4f;
			if(canAddEnergy){
				StartCoroutine(addEnergy());
				canAddEnergy = false;
			}
		}
		else{
			walking.mute = true;
		}

		if(health <= 50){
			energyLow.mute = false;
			energyLow.volume = 0.1f;
			if(canChangeColor){
			StartCoroutine(cameraFlash());
				canChangeColor = false;
			}
		}
		if(health > 50){
			energyLow.mute = true;
			Camera.main.backgroundColor = Color.black;
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
