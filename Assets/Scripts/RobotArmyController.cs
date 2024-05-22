using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotArmyController : MonoBehaviour
{
    public List<RemoteControlledObject> robots;// I must maintain order on my own.
    public ToggleGroup toggleGroup;

    private RemoteControlledObject activeRobot;

	private List<Toggle> toggles;//haha sneaky

	public AudioClip badSelection;
	public AudioSource soundEffects;

	// Start is called before the first frame update
	void Start()
    {
		toggles = new List<Toggle>(toggleGroup.GetComponentsInChildren<Toggle>());
		foreach(Toggle toggle in toggles)
		{
			toggle.onValueChanged.AddListener(delegate { OnToggleClicked(toggle); });
		}

		foreach(RemoteControlledObject robot in robots)
		{
			robot.enabled = false;
			robot.KeyboardControls = false;
		}


		if(robots.Count > 0 && toggles.Count > 0)
		{
			SetCurrentRobot(robots[0]);
			toggles[0].isOn = true;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnToggleClicked(Toggle selectedToggle)
	{
		if(selectedToggle.isOn)
		{
			int i = toggles.IndexOf(selectedToggle);
			if(i >= 0 && i < robots.Count)
			{
				if(robots[i].isAlive)
				{
					SetCurrentRobot(robots[i]);
				}
				else
				{
					selectedToggle.SetIsOnWithoutNotify(false);
					soundEffects.PlayOneShot(badSelection);
				}
			}
		}
	}

	void SetCurrentRobot(RemoteControlledObject newRobot)
	{
		if(activeRobot != null)
		{
			activeRobot.enabled = false;
		}

		activeRobot = newRobot;

		if(activeRobot != null)
		{
			activeRobot.enabled = true;
		}
	}
}
