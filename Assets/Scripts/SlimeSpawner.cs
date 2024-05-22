using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public List<GameObject> slimes;
	public float minSwitch = 45f; // seconds
	public float maxSwitch = 120f;
	public float inactiveTime = 30f;
	private bool isInactive = false;

	private float timer;

	private GameObject currentSlime;

	// Start is called before the first frame update
	void Start()
    {
        foreach (var slime in slimes)
		{
			slime.SetActive(false);	
		}

		timer = 10;//hard coded 10 second head start
    }

	// Update is called once per frame
	void Update()
	{
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			if(isInactive)
			{
				timer = Random.Range(minSwitch, maxSwitch);
				ActivateRandomSlime();
				isInactive = false;
			}
			else
			{
				DeactivateCurrentSlime();
				timer = inactiveTime;
				isInactive = true;
			}
		}
	}

	void ActivateRandomSlime()
	{
		int randomIndex = Random.Range(0, slimes.Count);
		currentSlime = slimes[randomIndex];
		currentSlime.SetActive(true);
		Debug.Log($"ADD {currentSlime.name}");
	}

	void DeactivateCurrentSlime()
	{
		if(currentSlime != null)
		{
			currentSlime.SetActive(false);
			Debug.Log($"REMOVE {currentSlime.name}");
			currentSlime = null;
		}
	}
}
