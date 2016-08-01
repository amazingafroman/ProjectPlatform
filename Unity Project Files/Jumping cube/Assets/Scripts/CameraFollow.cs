using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {

	Transform target;

	float height = 3.2f;
	[SerializeField] float distance = 5f ;
	[SerializeField] float posDamping = 10f;
	//[SerializeField] float camHight = 3.2f;
	public enum LevelType {
		Platformer,
		WarpRoom,
		Runner
	};

//	public int gateNum;
//	List <GameObject> warpGates;
//	Transform gateTarget;

	public LevelType levelType;

	Dictionary <int, LevelType> levels;


	// Use this for initialization
	void Start () {

//		warpGates = new List<GameObject> ();
//		warpGates.AddRange (GameObject.FindGameObjectsWithTag ("WarpGate"));

		levels = new Dictionary<int, LevelType> {
			{0, LevelType.WarpRoom},
			{1, LevelType.Platformer},
			//{2, LevelType.Platformer},
			//{3, LevelType.Runner},
			//{4, LevelType.Platformer},
			//{5, LevelType.Runner},
		};

		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void OnLevelWasLoaded (int level) {
		//levelType = levels[level];
		if (level == 0)
		transform.SetParent(GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>());
	}

	// Update is called once per frame
	void LateUpdate () {
		if (levelType == LevelType.WarpRoom) {
			//transform.SetParent(GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>());
			//	print ("HI IM WORKING");
//			gateTarget = warpGates [gateNum].GetComponent<Transform>();
			//transform.LookAt (target);
			//transform.position = new Vector3 (Mathf.Clamp (transform.position.y, -0f, 0f), transform.position.x, transform.position.z); 
//			print ("Im looking at WarpGate: " + gateTarget + "_"+ gateTarget.transform.position);
		}

		// escape statement if target == null
		if (levelType == LevelType.Platformer) {
			transform.parent = null;
			if (!target)
				return;
			// gets the targets position, moves it up and backwards
			Vector3 targetPosition = target.position + target.up * height - target.forward * distance;
			// moves from current pos to the one set above
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, posDamping * Time.deltaTime);
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -0f, 0f), transform.position.y, transform.position.z); 
		}
	}

}
