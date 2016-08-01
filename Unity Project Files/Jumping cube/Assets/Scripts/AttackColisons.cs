using UnityEngine;
using System.Collections;

public class AttackColisons : MonoBehaviour {
	public static AttackColisons instance;
	GameObject collide;

	void Awake (){
		if (instance == null) {
			//DontDestroyOnLoad (gameObject);
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
	
	}

	public GameObject GetTrigger () {
		return collide;
	}  

	void OnTriggerEnter (Collider col) {
		//print ("ojfods");
		collide = col.gameObject;
		if (col.tag == "CheckPoint") {
			//checkPoint = col.GetComponent<CheckPoint> ();
			CrateManager.crateManager.SetNewCheckpoint ();
		//	print ("hguidfhigudrnoitgjewoifjeowsig");
		} else if (col.tag == "Crate") {
			//crateManager = col.GetComponent <CrateManager> ();
			CrateManager.crateManager.WampaCrate ();
		} else if (col.tag == "TNTCrate") {
			//crateManager = col.GetComponent <CrateManager> ();
			CrateManager.crateManager.TNTCrate ();		
		} else if (col.tag == "NitroCrate") {
			PlayerManager.instance.PlayerRespawn ();
			print ("Boom"); 
			//	crateManager = col.GetComponent <CrateManager> ();
			CrateManager.crateManager.NitroCrate();		
		}
	}
				
	// Update is called once per frame
	void Update () {
	
	}
}
