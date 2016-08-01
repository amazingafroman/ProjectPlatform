using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance; 

	[SerializeField] int playerLives = 3;
	[SerializeField] bool playerDead = false;
	[SerializeField] int wampaPool = 0;
	bool firstDeath = false;

	Transform currentCheckPoint; 

	// These happen on level load. 
	void Awake () {
		print ("the is the screen res: " + Screen.currentResolution.width.ToString() + " x " + Screen.currentResolution.height.ToString());

		//carries over the player data like current lives and wampas
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	void Start (){
		// sets the checkpoint as the level start and moves the player to the postion. 
		// currentCheckPoint = GameObject.FindGameObjectWithTag ("LevelStart").GetComponent<Transform> ();
		LevelSpawn();
	}
		
	void OnLevelWasLoaded(int level){
		LevelSpawn();
		firstDeath = false;
	}

	void LevelSpawn (){
		// sets the checkpoint as the level start and moves the player to the postion. 
		currentCheckPoint = GameObject.FindGameObjectWithTag ("LevelStart").GetComponent<Transform> ();
		transform.position = new Vector3 (currentCheckPoint.position.x, currentCheckPoint.position.y, currentCheckPoint.position.z);
	}

	public void PlayerRespawn () {
		playerDead = true;
		firstDeath = true;
		if (playerDead == true && playerLives != 0) {
			if (firstDeath == true) {
				LevelManager.instance.HasDied (true);
			//	MetaDataContainer.metaDataContianer.Save ();
			}
			print ("Players Lives Left: " + playerLives);
			LifeLost ();
			transform.position = new Vector3 (currentCheckPoint.position.x, currentCheckPoint.position.y, currentCheckPoint.position.z);
			playerDead = false;
		} else if (playerDead == true && playerLives == 0) {
			LevelManager.instance.HasDied (true);
			print ("GameOver: You sux all the ballz");
		}
	}

	public void WampaColected (){
		wampaPool++;
	}

	// deals with player damage
	public void LifeLost () {
		playerLives -= 1;
		print ("Player Lives: " + playerLives); 
	}

	public Transform SetCheckPoint (Transform newCheckpoint) {
		return currentCheckPoint = newCheckpoint;	
	}

	public int AddWamaps (int wampasToAdd) {
		return wampaPool += wampasToAdd;
	}
		
	void ExtraLife(){
		playerLives++;
		print ("Life Count: " + playerLives);
		wampaPool = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.R)){
			PlayerRespawn();
		}

		if (wampaPool >= 6) {
			ExtraLife ();
		}
		// print ("Wampas Colected" + wampaPool);
	}

	//TODO: add to a diffrent script. 
	public void Save() {
		BinaryFormatter bF = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/PlayerInfo.dat");
		
		PlayerData data = new PlayerData ();
		data.playerLivesSaved = playerLives;
		
		bF.Serialize (file, data);
		file.Close ();
	} 
	
	//TODO: add to a diffrent script.
	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat")) {
			BinaryFormatter bF = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/PlayerInfo.dat",FileMode.Open);
			PlayerData data = (PlayerData) bF.Deserialize (file);	
			file.Close();
			playerLives = data.playerLivesSaved;
		}
	}
}

[Serializable]
class PlayerData
{
	public int playerLivesSaved;
}