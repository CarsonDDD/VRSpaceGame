using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform doorTransform;
    public float speed = 1f;

    private bool isClosing = false;
    private bool isOpening = false;

	// local positions
	private Vector3 openPosition = new Vector3 (1, 0, 0);// close position is always 0,0,0
	private Vector3 closedPosition = new Vector3(0, 0, 0);

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpening)
        {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, openPosition, speed * Time.deltaTime);
			
            if(transform.localPosition == openPosition) isOpening = false;
		}
        else if(isClosing)
        {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, closedPosition, speed * Time.deltaTime);
			
            if(transform.localPosition == closedPosition) isClosing = false;
		}
    }

    public void Open()
    {
        isOpening = true;
        isClosing = false;
    }

	public void Close()
	{
		isOpening = false;
		isClosing = true;
	}

    public void ToggleDoor()
    {
        if(isClosing && isOpening)
        {
            Debug.Log("Something is wrong with door script");
            return;
        }

        if(!isOpening && !isClosing)
        {
            // door is either fully open or fully closed
            
            //determine which one, then act accordingly
            if(doorTransform.localPosition == closedPosition )
            {
                Open();
            }
            else
            {
                Close();
            }
        }
        else
        {
            // either one is true, however NEVER both
            if(isClosing) Open();
            else if(isOpening) Close();
        }
    }
}
