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
    private Camera _camera;
    private CharacterController _characterController;

    public float speed = 0.0001f;

    // Joystick sim
    public Vector2 joystick = new Vector2(0,0);// 1 and -1 are max intensity
    //public float joystickReturnSpeed = 0.1f; // This may be a bad idea. This is done automatically through the real controller, no need to sim


    // Start is called before the first frame update
    void Start()
    {
        // Never fails
		_characterController = GetComponent<CharacterController>();

	}
    
    // Revert back to calling fixed update
    void Update()
    {
        // joystick.y = z movement
        joystick.Normalize();
        _characterController.Move(new Vector3(joystick.x * speed, 0, joystick.y * speed));

    }

	private void FixedUpdate()
	{
        if(Input.GetKey(KeyCode.UpArrow))
        {
            joystick.y += 1;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
		{
			joystick.y -= 1;
		}
        else
        {
            joystick.y = 0;
		}

		if(Input.GetKey(KeyCode.LeftArrow))
		{
			joystick.x -= 1;
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			joystick.x += 1;
		}
        else
        {
			joystick.x = 0;
		}
	}
}
