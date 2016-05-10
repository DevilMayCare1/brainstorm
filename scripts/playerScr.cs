using UnityEngine;
using System.Collections;

public class playerScr : MonoBehaviour {

	public float movementSpeed = 6.0f;
	public float upDownRange = 60.0f;
	public float mouseSens = 6.0f;
	public float jumpHeight = 6.0f;

	private float rotLeftRight;
	private float rotUpDown;

	public static bool CAUGHT;
	public static bool RESET;

	public GameObject shotSpawn;

	private float fbMove;
	private float lrMove;
	private float udMove;
	private Vector3 travel;
	private CharacterController cc;
	void Start () {
		cc = GetComponent<CharacterController> ();
		Cursor.visible = false;
	}


	void Update () {

		if (RESET == true) {
			transform.rotation = Quaternion.identity;

			RESET = false;
		}
		//rotation
		if (CAUGHT == false) {
			rotLeftRight = Input.GetAxis ("Mouse X") * mouseSens;
			transform.Rotate (0, rotLeftRight, 0);
			rotUpDown -= Input.GetAxis ("Mouse Y") * mouseSens;
			rotUpDown = Mathf.Clamp (rotUpDown, -upDownRange, upDownRange);
			Camera.main.transform.localRotation = Quaternion.Euler (rotUpDown, 0, 0);
			//movement
			fbMove = Input.GetAxis ("Vertical") * movementSpeed;
			lrMove = Input.GetAxis ("Horizontal") * movementSpeed;
			udMove += Physics.gravity.y * Time.deltaTime;
			travel = new Vector3 (lrMove, udMove, fbMove);
			travel = transform.rotation * travel;
			cc.Move (travel * Time.deltaTime);

			if (Input.GetKey ("left shift")) {
				movementSpeed = 15.0f;
			} else {
				movementSpeed = 9.0f;
			}
		}

		//jump
		if (cc.isGrounded && Input.GetButtonDown ("Jump")) {
			udMove = jumpHeight;
		}
	}
}
