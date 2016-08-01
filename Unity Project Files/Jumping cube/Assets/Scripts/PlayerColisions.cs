using UnityEngine;
using System.Collections;

public class PlayerColisions : MonoBehaviour {
	public static PlayerColisions instance;
	//PlayerColisions(){ }

	//WampaFruit	wampaFruit;
	//CheckPoint checkPoint; 
	//CrateManager crateManager; 
	LevelExit levelExit;
	GameObject collide;
	GameObject trigger;
	void Awake (){
		if (instance == null) {
			//DontDestroyOnLoad (gameObject);
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
	}

	public GameObject GetCollider () {
		return collide;
	}  

	public GameObject GetTrigger () {
		return trigger;
	}  
	
	void OnTriggerEnter (Collider col){

		trigger = col.gameObject;

		if (col.tag == "DeathCol") {
			PlayerManager.instance.PlayerRespawn ();
		} else if (col.tag == "WampaFruit") {
			//wampaFruit = col.GetComponent<WampaFruit> ();
			ColectableManager.colectableManager.WampaPickedUp ();
		}else if (col.tag == "LevelCrystal") {
			ColectableManager.colectableManager.LevelCrystal();	
		} else if (col.tag == "LevelExit") {
			levelExit = col.GetComponent <LevelExit> ();
			LevelManager.instance.LoadWarpRoom ();
		}
//		else if (col.tag == "CheckPoint") {
//			//checkPoint = col.GetComponent<CheckPoint> ();
//			CrateManager.crateManager.SetNewCheckpoint ();
//		} else if (col.tag == "Crate") {
//			//crateManager = col.GetComponent <CrateManager> ();
//			CrateManager.crateManager.WampaCrate ();
//		} else if (col.tag == "LevelExit") {
//			levelExit = col.GetComponent <LevelExit> ();
//			LevelManager.instance.LoadWarpRoom ();
//		} else if (col.tag == "TNTCrate") {
//			//crateManager = col.GetComponent <CrateManager> ();
//			CrateManager.crateManager.TNTCrate ();		
//		} else if (col.tag == "NitroCrate") {
//			PlayerManager.instance.PlayerRespawn ();
//			print ("Boom"); 
//		//	crateManager = col.GetComponent <CrateManager> ();
//			CrateManager.crateManager.NitroCrate();		
//		} 
	}

	void OnControllerColliderHit (ControllerColliderHit col){
		print ("This is a tag: " + col.gameObject.tag);

		CharacterController controller = GetComponent<CharacterController>();
//		if ((controller.collisionFlags == CollisionFlags.CollidedBelow) && col.gameObject.tag == "Crate") {
//			if ((controller.collisionFlags == CollisionFlags.Sides) != 0) {
//				print ("touched the ceiling");
//			}
//		}
		collide = col.gameObject;

		print (string.Format ("Move direction: {0}", col.moveDirection));
	
		if (col.moveDirection == Vector3.down && col.gameObject.tag == "Crate") {
		//	col.gameObject.SetActive (false);
			CrateManager.crateManager.WampaCrate ();
			print ("<color=green>Collision is in the downward direction</color>");
	
		}	else if (col.moveDirection == Vector3.down && col.gameObject.tag == "CheckPoint") {
			//col.gameObject.SetActive (false);
			PlayerControls.instance.JumpForward ();
			print ("Im doing the thing patty, look at meeeee!!!");
			CrateManager.crateManager.SetNewCheckpointCol ();
			print ("<color=green>Collision is in the downward direction</color>");

		}	else if (col.moveDirection == Vector3.down && col.gameObject.tag == "TNTCrate") {
			//col.gameObject.SetActive (false);
			CrateManager.crateManager.TNTCrate ();
			print ("<color=green>Collision is in the downward direction</color>");

		}	else if (col.moveDirection == Vector3.down && col.gameObject.tag == "NitroCrate") {
			//col.gameObject.SetActive (false);
			CrateManager.crateManager.NitroCrate ();
			print ("<color=green>Collision is in the downward direction</color>");

		}
	}

	void Update() {
//		CharacterController controller = GetComponent<CharacterController>();
//		if ((controller.collisionFlags & CollisionFlags.Below) != 0 && GetTrigger )
//			print("touched the ceiling");

	}
}
