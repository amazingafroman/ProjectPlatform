using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class ColectableManager: MonoBehaviour {
	public static ColectableManager colectableManager;
	//	PlayerManager playerManager;

	void Awake (){
		if (colectableManager == null) {
			DontDestroyOnLoad (gameObject);
			colectableManager = this;
		}
	}
	// Use this for initialization
	public void LevelCrystal () {
	//	print ("you have compleated this level: " + SceneManager.GetActiveScene ().name);
		LevelManager.instance.isCompleted ();
		//would have more in here like the animations and crystal colection noise

	}

	public void WampaPickedUp (){

		PlayerColisions.instance.GetTrigger().gameObject.SetActive (false);
		PlayerManager.instance.WampaColected();
	}


	// Update is called once per frame
	void Update () {
	
	}
}
