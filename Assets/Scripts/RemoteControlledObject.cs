using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// No physics movment (autostops)
// needs look direction (object)
// move function (based on direction)
// rotate head function
// has its own camera (head)
// Link to VR controller done elsewhere
// SOUNDS--make up implementation for now
// NO ANIMATIONS---save for later for when using real model


// Simulated RC player controller, with simulated joystick movement (and buttons)
// Controlled 
[RequireComponent (typeof(CharacterController))]
public class RemoteControlledObject : MonoBehaviour
{
    [SerializeField]
    private Transform headTransform;// aka the camera attached to the head
    [SerializeField]
    private Transform armPivot;// aka the camera attached to the head
    private CharacterController _characterController;

    public float moveSpeed = 2f;
	public float headSpeed = 0.09f;
	public float armSpeed = 0.05f;

	public bool LeftController = true;

	public bool KeyboardControls = false;

	//aaaah
	private float currentHeadXRotation = 0f;
	private float currentHeadYRotation = 0f;

    private float currentArmXRotation = 0f;
    private float currentArmYRotation = 0f;
    private float currentArmZRotation = 0f;

    // Joystick sim
    public Vector2 moveStick = new Vector2(0,0);// 1 and -1 are max intensity
    public Vector2 headStick = new Vector2(0,0);// rotation
    public Vector2 MaxHeadRotationMagnitude = new Vector2(10,10);// cannot be negative
    public Vector2 armStick = new Vector2(0, 0);// TODO: this may need to be a vector 3
    public Vector3 MaxArmRotationMagnitude = new Vector3(15, 15, 15);// cannot be negative
																	 //public float moveReturnSpeed = 0.1f; // This may be a bad idea. This is done automatically through the real controller, no need to sim

	public bool isAlive = true;
	// GRABBER
	/*[SerializeField] private Transform grabPoint;
	[SerializeField] private float grabRadius = 3;
	private Rigidbody grabbedObject = null;
	private bool isGrabbing = false;*/


	//[SerializeField] public Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        // Never fails
		_characterController = GetComponent<CharacterController>();

        //headObject.GetComponent<Camera>

	}
    
    // Revert back to calling fixed update
    void Update()
    {
		HandleMovement();
    }

	private void HandleMovement()
	{
		// MOVEMENT
		// move.y = z movement
		moveStick.Normalize();
		// USE DIRECTION OF HEAD
		Vector3 moveDirection = headTransform.forward * moveStick.y + headTransform.right * moveStick.x;
		moveDirection.y = 0;// uuuh I dont like this, if you look up/down, you will move slower than looking straight.
		moveDirection.Normalize();// unless.....?

		//_characterController.SimpleMove(new Vector3(moveStick.x * moveSpeed * Time.deltaTime, 0, moveStick.y * moveSpeed * Time.deltaTime));
		_characterController.SimpleMove(moveDirection * moveSpeed * Time.deltaTime);



		headStick.Normalize();
		//rotate based off x,y
		// make sure final rotation x,y is within bounds
		// HEAD ROTATION
		// y is actually x and x is actually y.
		// to rotate in a direction we rotate on the other axis
		currentHeadYRotation += headStick.x * headSpeed * Time.deltaTime;
		currentHeadXRotation += headStick.y * headSpeed * Time.deltaTime;

		//Debug.Log("PREnewRotation: " + newRotation);
		// REMOVE CLAMP, LET THAT BOY SPIN!
		//currentHeadXRotation = Mathf.Clamp(currentHeadXRotation, -MaxHeadRotationMagnitude.y, MaxHeadRotationMagnitude.y);
		//currentHeadYRotation = Mathf.Clamp(currentHeadYRotation, -MaxHeadRotationMagnitude.x, MaxHeadRotationMagnitude.x);
		//Debug.Log("POSTnewRotation: " + newRotation);
		headTransform.localEulerAngles = new Vector3(currentHeadXRotation, currentHeadYRotation, 0f);

		// ARM ROTATION
		// TODO: Remove mechanic and use buttons for something else
		currentArmYRotation += armStick.x * armSpeed * Time.deltaTime;
		currentArmXRotation += armStick.y * armSpeed * Time.deltaTime;
		//currentArmXRotation += armStick.y * armSpeed;
		// TODO: Z/roll movement
		currentArmXRotation = Mathf.Clamp(currentArmXRotation, -MaxArmRotationMagnitude.y, MaxArmRotationMagnitude.y);
		currentArmYRotation = Mathf.Clamp(currentArmYRotation, -MaxArmRotationMagnitude.x, MaxArmRotationMagnitude.x);
		armPivot.localEulerAngles = new Vector3(currentArmXRotation, currentArmYRotation, 0f);
	}

	private void FixedUpdate()
	{
		if(KeyboardControls)
		{
			KeyboardInput();
			return;
		}
		// hold shift for camera
		// only used for testing with keyboard
		Vector2 axis;
		if(LeftController)
		{
			axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

		}
		else
		{
			axis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
			// This does notthing, it only serves as a memory of a dead idea.
		}

		axis.Normalize();
		// IMPLEMENT ROTATE AND GRAB FOR CONTROLLER

		if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
		{
			headStick.x = axis.x;
			//headStick.y = axis.y;
		}
		else
		{
			moveStick.x = axis.x;
			moveStick.y = axis.y;

			headStick = Vector2.zero;

		}
	}

	private void KeyboardInput()
	{
		if(Input.GetKey(KeyCode.RightShift))
		{
			/*if(Input.GetKey(KeyCode.UpArrow))
			{
				headStick.y -= 1;
			}
			else if(Input.GetKey(KeyCode.DownArrow))
			{
				headStick.y += 1;
			}
			else
			{
				headStick.y = 0;
			}*/

			if(Input.GetKey(KeyCode.LeftArrow))
			{
				headStick.x -= 1;
			}
			else if(Input.GetKey(KeyCode.RightArrow))
			{
				headStick.x += 1;
			}
			else
			{
				headStick.x = 0;
			}
		}
		else if(Input.GetKey(KeyCode.RightControl))
		{
			headStick = Vector2.zero;
			if(Input.GetKey(KeyCode.UpArrow))
			{
				armStick.y -= 1;
			}
			else if(Input.GetKey(KeyCode.DownArrow))
			{
				armStick.y += 1;
			}
			else
			{
				armStick.y = 0;
			}

			if(Input.GetKey(KeyCode.LeftArrow))
			{
				armStick.x -= 1;
			}
			else if(Input.GetKey(KeyCode.RightArrow))
			{
				armStick.x += 1;
			}
			else
			{
				armStick.x = 0;
			}
		}
		else
		{
			headStick = Vector2.zero;
			if(Input.GetKey(KeyCode.UpArrow))
			{
				moveStick.y += 1;
			}
			else if(Input.GetKey(KeyCode.DownArrow))
			{
				moveStick.y -= 1;
			}
			else
			{
				moveStick.y = 0;
			}

			if(Input.GetKey(KeyCode.LeftArrow))
			{
				moveStick.x -= 1;
			}
			else if(Input.GetKey(KeyCode.RightArrow))
			{
				moveStick.x += 1;
			}
			else
			{
				moveStick.x = 0;
			}
		}
	}
}
