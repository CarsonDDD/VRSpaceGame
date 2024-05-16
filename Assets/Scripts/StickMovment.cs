using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StickMovment : MonoBehaviour
{
	public bool EnableLinearMovement = true;
	public bool EnableRotation = true;
	public bool HMDRotatesPlayer = true;
	public bool RotationEitherThumbstick = false;
	public float RotationAngle = 45.0f;
	public float Speed = 0.0f;
	public OVRCameraRig CameraRig;

	private bool doSnapTurn;
	private Rigidbody _rigidbody;

	public event Action CameraUpdated;
	public event Action PreCharacterMove;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		if(CameraRig == null) CameraRig = GetComponentInChildren<OVRCameraRig>();
	}

	void Start()
	{
	}

	// fixed is physics
	private void FixedUpdate()
	{
		//if(HMDRotatesPlayer) RotatePlayerToHMD();
		if(EnableLinearMovement) StickMovement();
		if(EnableRotation) SnapTurn();
	}

	/*void RotatePlayerToHMD()
	{
		Transform root = CameraRig.trackingSpace;
		Transform centerEye = CameraRig.centerEyeAnchor;

		Vector3 prevPos = root.position;
		Quaternion prevRot = root.rotation;

		transform.rotation = Quaternion.Euler(0.0f, centerEye.rotation.eulerAngles.y, 0.0f);

		root.position = prevPos;
		root.rotation = prevRot;
	}*/

	void StickMovement()
	{
		Quaternion ort = CameraRig.centerEyeAnchor.rotation;
		Vector3 ortEuler = ort.eulerAngles;
		ortEuler.z = ortEuler.x = 0f;
		ort = Quaternion.Euler(ortEuler);

		Vector3 moveDir = Vector3.zero;
		Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
		moveDir += ort * (primaryAxis.x * Vector3.right);
		moveDir += ort * (primaryAxis.y * Vector3.forward);
		_rigidbody.MovePosition(_rigidbody.position + moveDir * Speed * Time.fixedDeltaTime);
	}

	// TODO: see if I can use new input system instead of hardcoding
	void SnapTurn()
	{
		if(OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft) ||
			(RotationEitherThumbstick && OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft)))
		{
			if(doSnapTurn)
			{
				doSnapTurn = false;
				transform.RotateAround(CameraRig.centerEyeAnchor.position, Vector3.up, -RotationAngle);
			}
		}
		else if(OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight) ||
				 (RotationEitherThumbstick && OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight)))
		{
			if(doSnapTurn)
			{
				doSnapTurn = false;
				transform.RotateAround(CameraRig.centerEyeAnchor.position, Vector3.up, RotationAngle);
			}
		}
		else
		{
			doSnapTurn = true;
		}
	}
}
