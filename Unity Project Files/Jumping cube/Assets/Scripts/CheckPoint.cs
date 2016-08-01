using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {
	//PlayerManager playerManager;
	Transform checkPointPos;

	// Use this for initialization
	void Start () {
		//playerManager = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerManager> ();
	}

	public void SetNewCheckpoint () {
		checkPointPos = GetComponent<Transform> ();
		//print ("Check point Location: " + checkPointPos.position);
		PlayerManager.instance.SetCheckPoint (checkPointPos);
		gameObject.SetActive (false);
	}



	// Update is called once per frame
	void Update () {
	
	}
}
