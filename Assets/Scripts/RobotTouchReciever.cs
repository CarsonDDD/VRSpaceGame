using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class RobotTouchReciever : MonoBehaviour
{
	[SerializeField] public UnityEvent<GameObject> OnPressed;
	[SerializeField] public UnityEvent<GameObject> OnUnPressed;
	[SerializeField] public float b = 1;
	[SerializeField] public float a = 1;

	// Start is called before the first frame update
	void Start()
    {
        
    }


    // NEVER STAY ONLY ENTER AND LEAVE
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Arm")
        {
			OnPressed?.Invoke(this.gameObject);
        }
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Arm")
		{
			OnUnPressed?.Invoke(this.gameObject);
		}
	}
}
