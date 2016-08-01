using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

	GameObject player;
	Rigidbody playerRig;
	[SerializeField] float movementSpeed = 40f;
	//Vector3 jumping = Vector3.zero;
	float jumpSpeedY = 7.0f;
	float jumpSpeedZ = 8.0f;
	bool isGrounded = true;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player Character Controler");
		playerRig = GetComponent <Rigidbody> ();
	}


	void MoveForward () {
		player.transform.Translate (Vector3.forward * Time.deltaTime * movementSpeed, Space.World);
		//playerRig.AddForce (Vector3.forward * movementSpeed, ForceMode.Impulse);
	}

	void JumpForward () {
		playerRig.AddForce (Vector3.up * jumpSpeedY, ForceMode.Impulse);
		playerRig.AddForce (Vector3.forward * jumpSpeedZ, ForceMode.Impulse);
	}


	// Update is called once per frame
	void Update () {

		playerRig.velocity = Vector3.ClampMagnitude (playerRig.velocity, 7f);

		if (isGrounded == true) {
			if (Input.GetMouseButton (0) ) {
				MoveForward ();
			} 
		} if (Input.GetMouseButtonUp (0)) {
				JumpForward ();
		}
	}
}
