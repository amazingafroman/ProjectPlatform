using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {
	public static PlayerControls instance;

	//For Tweeking Player Movement
	[SerializeField] float speed = 6.0F;
	[SerializeField] float jumpSpeed = 3.0F;
	[SerializeField] float gravity = 20.0F;

	int jumpCount = 1;

	//for Input
	public float deadZone = 7f;
	float startYPos;
	float verticalMove;

	//TODO: have the attackcol and player col be appart of the same script and make have the player manager be in charge of finding attack collider 
	public GameObject atackCol;

	//For Character Controler
	public Vector2 startPos;
	public Vector2 direction;
	public bool directionChosen;
	private Vector3 moveDirection = Vector3.zero;

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
		levels = new Dictionary <int, LevelType> {
			{0, LevelType.WarpRoom},
			{1, LevelType.Platformer},
		};
		//remember to change in final form
		levelType = levels [1];
	}

	//sets control type on level load
	void OnLevelWasLoaded (int level) {
		//Awake ();
		levelType = levels[level];
		//if (level == 0) {
		//	WarpRoom ();
		//}
	}
		
	void SetMousePos () {
		if (Application.isMobilePlatform)
			startYPos = Input.GetTouch (0).position.y;
		else 
			startYPos = Input.mousePosition.y;
		print ("Mouse pos is: " + startYPos);
	} 

	void ForwardMovememnt (){
//		float mY = Input.mousePosition.y;
//		Vector2 tY;
//		if (Application.isMobilePlatform) {
//			tY = Input.GetTouch (0).position;
//			} else {
//				tY = new Vector2 (0f,0f);
//			}
//		// for joystick like movement. 
//		if (mY > verticalMove + Screen.height / deadZone || tY.y > verticalMove + Screen.height / deadZone) {
//			moveDirection = Vector3.forward;
//			moveDirection = transform.TransformDirection (moveDirection);
//			moveDirection *= speed;
//		} else if (mY < verticalMove + Screen.height / deadZone || tY.y < verticalMove + Screen.height / deadZone) {
//			StopMoving ();
//			}
//			jumpCount = 1;

		moveDirection = Vector3.forward;
		moveDirection = transform.TransformDirection (moveDirection);
		moveDirection *= speed;
	
	}


	//PLayer Ablitys
	public void JumpForward () {
		moveDirection.y = jumpSpeed;
		moveDirection.y = moveDirection.y;
		jumpCount++;
		print ("Jump Count is: " + jumpCount);
	}
	
	public void CrateJump () {
		//moveDirection;
	}

	IEnumerator Attack() {
		atackCol.SetActive (true);
		print ("SPINNING!!!! WOOOOOOOOOOO");
		yield return new WaitForSeconds (0.3f);
		atackCol.SetActive (false);
		print ("not spinning anymore :(");
		//yield return null;
	}

	//rests the player move direction to zero, to stop forward movement. 	
	void StopMoving () {
		moveDirection = Vector3.zero;
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		startYPos = 0f;
		jumpCount = 1;
	}

	void Update() {
	//CharacterController controller = GetComponent<CharacterController>();
	//if (Application.isMobilePlatform) {
		//print ("1");
	if (levelType == LevelType.Platformer) {
		//print("2");
		CharacterController controller = GetComponent<CharacterController> ();
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);
		
			SetMousePos();
		if (controller.isGrounded && Input.GetMouseButton (0)) {
				//SetMousePos();
				ForwardMovememnt ();
			} if (Input.GetMouseButtonDown (1) ) {
				StartCoroutine(Attack());
				}//Jumps when the player lets go of the screen/mousebutton
		else if (controller.isGrounded && Input.GetMouseButtonUp (0)) {
			if (jumpCount <= 2){
				JumpForward ();
				}
		} else {
				StopMoving();

			}		
		}
	}
}