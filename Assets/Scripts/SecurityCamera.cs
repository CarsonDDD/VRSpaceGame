using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SecurityCamera : MonoBehaviour
{
	[SerializeField] private float rotateMagnitude = 5f;
	[SerializeField] private float rotateSpeed = 1f;
	public TextMeshProUGUI Text;

	public bool EnableRotation = true;

	private bool rotatingRight = true;// controls direction. The camera always rotates
	private float currentAngle = 0f;
	// Default rotation 0,0,0

	private void Start()
	{
		Text.text = this.transform.parent.name; // This works because the structure allows it to work 
	}

	private void OnEnable()
	{
		transform.localRotation = Quaternion.Euler(0, 0, 0);
		currentAngle = 0f;
	}

	// Update is called once per frame
	void Update()
    {
		if(!EnableRotation) return;

		if(rotatingRight)
		{
			currentAngle += rotateSpeed * Time.deltaTime;
			if(currentAngle >= rotateMagnitude)
			{
				rotatingRight = false;
			}
		}
		else
		{
			currentAngle -= rotateSpeed * Time.deltaTime;
			if(currentAngle <= -rotateMagnitude)
			{
				rotatingRight = true;
			}
		}

		transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
	}
}
