using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {
	public static LevelManager instance;

	MetaDataContainer levelProgression = new MetaDataContainer();
    // List <string>
	// pass in the current level. 
	public static string currentLevel;

	LevelMetaData CurrentLevelMetaData{
		get{
			return levelProgression.GetLevelProgression(currentLevel);
		}
	}

	void Awake (){
		if (instance == null) {
			//DontDestroyOnLoad (gameObject);
			instance = this;
		}
		else if (instance != this) {
			Destroy (this.gameObject);
			}
	}
		
	void OnLevelWasLoaded (int level) {

		//Sets the name of the level for loading and saving the meta data.
		string levelString = currentLevel;
		//currentLevel = SceneManager.GetActiveScene().name;

		// gets the level metadata for the current level
		LevelMetaData lmd = levelProgression.GetLevelProgression (levelString);

		levelProgression.Load ();

		levelProgression.Save ();

		/*
		// examples for how to write the changes to the script.
		CurrentLevelMetaData.allCratesDestroyed = true;
		CurrentLevelMetaData.TimeTrialCompleted = true;


		if (lmd.levelCompleted) 
		{
			// do stuff!
		}
		 */

	}
	public void HasDied (bool isDead) {
		CurrentLevelMetaData.noDeath = isDead;
	}

	public void isCompleted(){
		CurrentLevelMetaData.levelCompleted = true;
	}
	public void AllCrateDestroyed(){
		CurrentLevelMetaData.allCratesDestroyed = true;
	}

	//
	public void LoadWarpRoom (){
		levelProgression.Save ();

		SceneManager.LoadScene("OneButtonCrash_WarpRoom_test");
	}

	public void loadlevel () {
		SceneManager.LoadScene("OneButtonCrash_test_05");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.L)) 
			loadlevel ();
		if (Input.GetKeyUp (KeyCode.W))
			LoadWarpRoom ();
	}
}
