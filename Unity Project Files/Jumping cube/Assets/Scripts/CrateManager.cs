using UnityEngine;
using System.Collections;

public class CrateManager : MonoBehaviour {
	public static CrateManager crateManager;
	GameObject tempCrate;
	Transform checkPointPos;

	[SerializeField] string crateType; 

	int	wampasContained;

	void Awake (){
		if (crateManager == null) {
			DontDestroyOnLoad (gameObject);
			crateManager = this;
		}
	}

	public void WampaCrate () {
		wampasContained = Random.Range (5, 11); 
		print ("Wampa Fruit in the box: " + wampasContained);
		PlayerManager.instance.AddWamaps (wampasContained);
	}

	public void TNTCrate() {
		print ("Boom"); 
		PlayerManager.instance.LifeLost ();
	}

	public void NitroCrate() {
		print ("Boom"); 
		PlayerManager.instance.LifeLost ();
		PlayerManager.instance.PlayerRespawn ();
	}

	//TODO: Keep for the time being, might be usefull later. 
	public string GetCrateType () { 
		return crateType;
	}

	public void SetNewCheckpointCol () {
		tempCrate = PlayerColisions.instance.GetCollider ().gameObject;
		//GameObject tempCheckPointGM = PlayerColisions.instance.GetCollider (); 
		//checkPointPos = tempCheckPointGM.GetComponent<Transform> ();
		checkPointPos = tempCrate.GetComponent<Transform> ();
		//print ("Check point Location: " + checkPointPos.position);
		PlayerManager.instance.SetCheckPoint (checkPointPos);
		StartCoroutine (CrateRemoval());
		//tempCrate.gameObject.SetActive (false);
	}
	public void SetNewCheckpoint () {
		tempCrate = AttackColisons.instance.GetTrigger (); 
		checkPointPos = tempCrate.GetComponent<Transform> ();
		//print ("Check point Location: " + checkPointPos.position);
		PlayerManager.instance.SetCheckPoint (checkPointPos);
		//tempCrate.gameObject.SetActive (false);
	}


	IEnumerator CrateRemoval () {
		yield return new WaitForSeconds (0.3f);		
		tempCrate.gameObject.SetActive (false);
		//tempCrate = null;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.W)) {
			WampaCrate();
		}
	}
}
