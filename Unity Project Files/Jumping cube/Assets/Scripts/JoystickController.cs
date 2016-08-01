using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JoystickController : MonoBehaviour {
	public static JoystickController instance;

	//All will mod how the player moves. 
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public float horizontalSpeed = 2.0F;
	private Vector3 moveDirection = Vector3.zero;
	float modifier = 100f;
	public int jumpCount = 0;

	public GameObject atackCol;


	//setting the type of level
	public LevelType levelType;
	Dictionary <int, LevelType> levels;
	public enum LevelType {
		Platformer,
		WarpRoom,
		Runner
	};

	void Awake(){
		if (instance == null) {
			//DontDestroyOnLoad (gameObject);
			instance = this;
		}

		//controller = GetComponent<CharacterController> ();
		levels = new Dictionary<int, LevelType> {
			{0, LevelType.WarpRoom},
			{1, LevelType.Platformer},
		};
		//remember to change in final form
		levelType = levels [0];
	}

	public void JumpForward () {
		moveDirection.y = jumpSpeed;
		jumpCount += 1;
		print ("Jump Count is: " + jumpCount);
	}

	IEnumerator Attack() {
		atackCol.SetActive (true);
		print ("SPINNING!!!! WOOOOOOOOOOO");
		yield return new WaitForSeconds (0.3f);
		atackCol.SetActive (false);
		print ("not spinning anymore :(");
		//yield return null;
	}

	// Update is called once per frame

	void Update () {
		CharacterController controller = GetComponent<CharacterController> ();
		if (controller.isGrounded) {
			jumpCount = 0;
			//moveDirection = new Vector3 (Input.mousePosition.x, 0, Input.mousePosition.y);
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= speed;

		} if (Input.GetButtonDown ("Attack")) {
			StartCoroutine (Attack ());
		}


		if (Input.GetButtonDown ("Jump") && jumpCount != 2) {
			JumpForward (); 
				if (jumpCount == 1) {
					jumpSpeed = 8f;		
				}
			} 
		else if (controller.isGrounded)  {
				jumpCount = 0;
				jumpSpeed = 8f;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);
	}
}
