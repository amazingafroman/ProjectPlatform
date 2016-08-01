using UnityEngine;
using System.Collections;

public class LevelExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void LevelCompleated () {
		//LevelManager.instance.LoadWarpRoom ();


		print ("Level Compleated"); 
		//in a coroutine 
		// Would award the player with the crystals they have compleated
		// would animate the character warping out of the level
		// would load the warp room

	}


	// Update is called once per frame
	void Update () {
	
	}
}
