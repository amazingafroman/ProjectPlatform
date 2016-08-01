using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;


public class instanceScript : MonoBehaviour {
	public static instanceScript instance;


	MetaDataContainer levelProgression = new MetaDataContainer();
    // List <string>
	// pass in the current level. 
	public static string currentLevel;

	//
	LevelMetaData CurrentLevelMetaData{
		get{
			return levelProgression.GetLevelProgression(currentLevel);
		}
	}

	void Awake (){
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		}
		/*else if (instance != this) {
			Destroy (gameObject);
			}*/
	}

	void OnLevelWasLoaded (int level) {

		levelProgression.Load ();
		string levelString = currentLevel;
		//currentLevel = Scene.name;
	
		// gets the level metadata for the current level
		LevelMetaData lmd = levelProgression.GetLevelProgression (levelString);

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

	public void isCompleted(){
		CurrentLevelMetaData.levelCompleted = true;
	}

	public void LoadWarpRoom (){
		levelProgression.Save ();

		//SceneManager.LoadScene("OneButtonCrash_WarpRoom_test");
		// Application.lo
	}

	public void loadlevel () {
		//SceneManager.LoadScene("OneButtonCrash_test_05");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.L)) {
			loadlevel ();

		}
	}
}
