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
    private CharacterController _characterController;

    public float moveSpeed = 0.01f;
	public float headSpeed = 0.09f;

	//aaaah
	private float currentXRotation = 0f;
	private float currentYRotation = 0f;

	// Joystick sim
	public Vector2 moveStick = new Vector2(0,0);// 1 and -1 are max intensity
    public Vector2 headStick = new Vector2(0,0);// rotation
    public Vector2 MaxHeadRotationMagnitude = new Vector2(10,10);// cannot be negative
    //public float moveReturnSpeed = 0.1f; // This may be a bad idea. This is done automatically through the real controller, no need to sim


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
		// move.y = z movement
		moveStick.Normalize();
        _characterController.Move(new Vector3(moveStick.x * moveSpeed, 0, moveStick.y * moveSpeed));

		headStick.Normalize();
		//rotate based off x,y
		// make sure final rotation x,y is within bounds

		// y is actually x and x is actually y.
		// to rotate in a direction we rotate on the other axis
		currentYRotation += headStick.x * headSpeed;
		currentXRotation += headStick.y * headSpeed;

		//Debug.Log("PREnewRotation: " + newRotation);
		currentXRotation = Mathf.Clamp(currentXRotation, -MaxHeadRotationMagnitude.y, MaxHeadRotationMagnitude.y);
		currentYRotation = Mathf.Clamp(currentYRotation, -MaxHeadRotationMagnitude.x, MaxHeadRotationMagnitude.x);
		//Debug.Log("POSTnewRotation: " + newRotation);
		headTransform.localEulerAngles = new Vector3(currentXRotation, currentYRotation, 0f);
	}

	private void FixedUpdate()
	{
        // hold shift for camera
        // only used for testing with keyboard

        if(Input.GetKey(KeyCode.RightShift))
        {
			if(Input.GetKey(KeyCode.UpArrow))
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
			}

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
        else
        {
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
