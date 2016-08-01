using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour {
	//All will mod how the player moves. 
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public float horizontalSpeed = 2.0F;
	float modifier = 100f;
	float verticalMove;
	public float deadZone = 1.5f;
	private Vector3 moveDirection = Vector3.zero;	
	public Vector2 startPos;
	public Vector2 direction;
	public bool directionChosen;
	public GameObject atackCol;

	float lastX;
	float currentX;

	//for chaniging controlls
	Dictionary <int, LevelType> levels;
	public LevelType levelType;
	public enum LevelType {
		Platformer,
		WarpRoom,
		Runner
	};

	//used for Lvl Select
	public int gateNum;
	int jumpCount = 1;
	GameObject camPivot; 
	Transform gateTarget;
	List <GameObject> warpGates;

	//debug text
	Text text;

	void Awake(){
		//text = GameObject.Find ("fuText").GetComponent<Text> ();
		levels = new Dictionary<int, LevelType> {
			{0, LevelType.Platformer},
			{1, LevelType.Platformer},
			//{2, LevelType.Platformer},
			//{3, LevelType.Runner},
			//{4, LevelType.Platformer},
			//{5, LevelType.Runner},
		};

		//remember to change in final form
		levelType = levels [Application.loadedLevel];
	}

	void WarpRoom (){
		camPivot = GameObject.Find ("Camera Pivot");
		warpGates = new List<GameObject> ();
		foreach (Transform child in GameObject.Find("WarpRoom").transform) {
			if (child.tag == "WarpGate")
				warpGates.Add (child.gameObject);
		}
	}

	//sets control type on level load
	void OnLevelWasLoaded (int level) {
		//Awake ();
		levelType = levels[level];
		if (level == 0) {
			WarpRoom ();
		}
	}
		
	void SetMousePos () {
		if (Application.isMobilePlatform)
			verticalMove = Input.GetTouch (0).position.y;
		else 
			verticalMove = Input.mousePosition.y;
	} 

	void JumpForward () {
		moveDirection.y = jumpSpeed;
		jumpCount++;
		print ("Jump Count is: " + jumpCount);
	}

	IEnumerator ResetMousePos(){
		yield return new WaitForSeconds (0.5f);
		verticalMove = 0f;
	}

	IEnumerator Attack() {
		atackCol.SetActive (true);
		print ("SPINNING!!!! WOOOOOOOOOOO");
		yield return new WaitForSeconds (0.3f);
		atackCol.SetActive (false);
		print ("not spinning anymore :(");
		//yield return null;
	}

	//Forward Movement
	public void MoveForward () {
		float mY = Input.mousePosition.y;
		Vector2 tY;
		if (Application.isMobilePlatform) {
			tY = Input.GetTouch (0).position;
		} else {
			tY = new Vector2 (0f,0f);
		}
		// for joystick like movement. 
		if (mY > verticalMove + Screen.height / deadZone || tY.y > verticalMove + Screen.height / deadZone) {
			moveDirection = Vector3.forward;
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= speed;
		} else if (mY < verticalMove + Screen.height / deadZone || tY.y < verticalMove + Screen.height / deadZone) {
			StopMoving ();
		}
		jumpCount = 1;
	}

	//For left and right movement. 
	void MoveHorz () {
//		float h;

		//print ("Mousepos " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
		//float h = (transform.position.x, val, horizontalSpeed*Time.deltaTime);
		//h = Mathf.Clamp (h, 5f, -5f);


		float val = Input.mousePosition.x / Screen.width;
		val -= .5f;
		val *= 10f;

		float horSpeed = 20f;
		horSpeed /= 100f;

		currentX = Mathf.Clamp (val, lastX - horSpeed, lastX + horSpeed);
		print ("Val is " + val + "CurrextX is " + currentX);

		currentX = Mathf.Clamp (currentX, -5f, 5f);
		transform.position = new Vector3 (currentX, transform.position.y, transform.position.z);

		lastX = currentX;
//		if (Application.isMobilePlatform) {
//			h = horizontalSpeed * Input.GetTouch(0).position.y;
//		} else {
//			h = horizontalSpeed * Input.GetAxis ("Mouse X");
//		}
//
		
//		Vector3 tempPos = transform.position;
//		tempPos.x += h/modifier;
//		transform.position = tempPos;
	}

	//rests the player move direction to zero, to stop forwarmovement. 	
	void StopMoving () {
		moveDirection = Vector3.zero;
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		verticalMove = 0f;
		jumpCount = 1;
	}
	void FixedUpdate (){
		//deals with the horizontal movment of the chracter
		if (Input.GetMouseButton(0)) 
		MoveHorz ();
	}
	void Update() { 
		//print ("Mouse Y Axis: " + Input.mousePosition.y);
		//print ("Mouse posY:  " + verticalMove); 
		print ("Player posx first" + transform.position.x); 

		//if (Application.isMobilePlatform) {
			//if (levelType == LevelType.Platformer) {

//				CharacterController controller = GetComponent<CharacterController> ();
//				controller.Move (moveDirection * Time.deltaTime);
//				moveDirection.y -= gravity * Time.deltaTime;
//
//				//deals with the horizontal movment of the chracter
//				MoveHorz ();

//			if (controller.isGrounded && Input.GetTouch (0).phase == TouchPhase.Began) {
//				SetMousePos ();
//			}
//
//			if (controller.isGrounded && Input.GetTouch (0).phase == TouchPhase.Moved) {
//				//SetMousePos();
//				MoveForward ();
		//			if (Input.GetTouch (1).phase == TouchPhase.Began ) {
//					StartCoroutine(Attack());
//				}
//			}
//			//Jumps when the player lets go of the screen/mousebutton
//			else if (controller.isGrounded && Input.GetTouch (0).phase == TouchPhase.Ended) {
//				if (jumpCount <= 2){
//						JumpForward ();
//				}
//			}
		//}
			
		#region PC  Mouse Controls 
		//else {

			if (levelType == LevelType.Platformer) {
				
				CharacterController controller = GetComponent<CharacterController> ();
				controller.Move (moveDirection * Time.deltaTime);
			print ("Player posx second" + transform.position.x);
				moveDirection.y -= gravity * Time.deltaTime;
				
			print ("Player posx third" + transform.position.x);
				if (controller.isGrounded && Input.GetMouseButtonDown(0) ) 
					SetMousePos();

				//Moves when the player touches the screen/mousebutton
				if (controller.isGrounded && Input.GetMouseButton (0) ) {
					//SetMousePos();
					MoveForward ();
					if (Input.GetMouseButton (1) ) {
						StartCoroutine(Attack());
					}
				}
				
				//Jumps when the player lets go of the screen/mousebutton
				else if (Input.GetMouseButtonUp (0)) {
					if( jumpCount <= 2 )
						JumpForward ();
				}
			
				else if (controller.isGrounded && Input.GetMouseButton (1) ){
					StartCoroutine(Attack());
				}
				//Stops the players momentum if they are not clicking. 
				else if (controller.isGrounded && !Input.GetMouseButtonUp (0) ) {
					StopMoving ();
				}

		} 
			
			else if (levelType == LevelType.WarpRoom) {
			//TODO: add warp room controls.
			WarpRoom ();
			print ("You are in the warp room");

			gateTarget = warpGates [gateNum].GetComponent<Transform>();
			transform.LookAt (gateTarget);
			camPivot.transform.LookAt (gateTarget);

			print ("Im looking at WarpGate: " + gateTarget + "_"+ gateTarget.transform.position);
		} 
			else if (levelType == LevelType.Runner){
		//Create runner controls
		print ("You are in a runner level");
	}

		#endregion
		print ("Player posx last" + transform.position.x);
		}
}