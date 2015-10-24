using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
	public Text winText;
		
	public Wave[] waves;
	public Enemy enemy;
	public GameObject health;
	
	LivingEntity playerEntity;
	Transform playerT;
	
	Wave currentWave;
	int currentWaveNumber;
	int enemiesSpawned;
	
	int enemiesRemainingToSpawn;
	int enemiesRemainingAlive;
	float nextSpawnTime;
	
	MapGenerator map;
	
	float timeBetweenCampingChecks = 4;
	float campThresholdDistance = 1.5f;
	float nextCampCheckTime;
	Vector3 campPositionOld;
	bool isCamping;
	
	bool isDisabled;

	public event System.Action<int> OnNewWave;
	
	void Start() {

		playerEntity = FindObjectOfType<PlayerInput> ();
		playerT = playerEntity.transform;
		
		nextCampCheckTime = timeBetweenCampingChecks + Time.time;
		campPositionOld = playerT.position;
		playerEntity.OnDeath += OnPlayerDeath;
		
		map = FindObjectOfType<MapGenerator> ();
		NextWave ();
	}
	
	void Update() {
		if(currentWaveNumber >= waves.Length && enemiesRemainingAlive == 0){
		//	Debug.Log("WIN!");
			winText.text = "You win!";
			//FindObjectOfType<PlayerInput>().enabled = false;
			
		}
		if(enemiesSpawned >= 5){
			enemiesSpawned = 0;
			spawnHealth();
		}

		if (!isDisabled) {
			if (Time.time > nextCampCheckTime) {
				nextCampCheckTime = Time.time + timeBetweenCampingChecks;
				
				isCamping = (Vector3.Distance (playerT.position, campPositionOld) < campThresholdDistance);
				campPositionOld = playerT.position;
			}
			
			if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime) {
				enemiesRemainingToSpawn--;
				nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
				
				StartCoroutine (SpawnEnemy ());
			}
		}
	}
	
	IEnumerator SpawnEnemy() {
		float spawnDelay = 1;
		float tileFlashSpeed = 4;
		
		Transform spawnTile = map.GetRandomOpenCoord ();
		if (isCamping) {
			spawnTile = map.GetTileFromPosition(playerT.position);
		}
		Material tileMat = spawnTile.GetComponent<Renderer> ().material;
		Color initialColour = tileMat.color;
		Color flashColour = Color.red;
		float spawnTimer = 0;
		
		while (spawnTimer < spawnDelay) {

			tileMat.color = Color.Lerp(initialColour,flashColour, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));
			
			spawnTimer += Time.deltaTime;
			yield return null;
		}
		
		Enemy spawnedEnemy = Instantiate(enemy, spawnTile.position + Vector3.up, Quaternion.identity) as Enemy;
		enemiesSpawned++;
		spawnedEnemy.OnDeath += OnEnemyDeath;
	}

	void spawnHealth(){
		Transform SpawnTile = map.GetRandomOpenCoord();
		Instantiate(health, SpawnTile.position + Vector3.up * 2f, Quaternion.identity);
	}
	
	void OnPlayerDeath() {
		isDisabled = true;
	}
	
	void OnEnemyDeath() {
		enemiesRemainingAlive --;
		playerEntity.TakeDamage(-20);
		Debug.Log("Death energyss modified");
		
		if (enemiesRemainingAlive == 0) {
			//Add things to do before advancing here!
			NextWave();
		}
	}

	void resetPlayerPosition(){
		playerT.position = map.GetTileFromPosition(Vector3.zero).position + Vector3.up * 3;
	}
	
	void NextWave() {
		currentWaveNumber ++;

		if (currentWaveNumber - 1 < waves.Length) {
			currentWave = waves [currentWaveNumber - 1];
			
			enemiesRemainingToSpawn = currentWave.enemyCount;
			enemiesRemainingAlive = enemiesRemainingToSpawn;

			if(OnNewWave != null){
				OnNewWave(currentWaveNumber);
			}
			resetPlayerPosition();
		}
	}
	
	[System.Serializable]
	public class Wave {
		public int enemyCount;
		public float timeBetweenSpawns;
	}
	
}