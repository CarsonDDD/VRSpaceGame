using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Monster : MonoBehaviour
{
    [SerializeField] private List<Transform> pathNodes;
    [SerializeField] private float moveSpeed;
    [HideInInspector] public float MoveSpeedMultiplier = 1f;
	public float DistanceThreshhold = 0.25f;

    private int currentTargetIndex = 0;

    private Rigidbody rb;
	private AudioSource soundEffect;

	// SCALING
	public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);
	public Vector3 maxScale = new Vector3(1.5f, 1.5f, 1.5f);
	public float scaleSpeed = 2f;
	private bool scalingUp = true;

	// SOUNDS
	[SerializeField] public AudioClip[] slimeSounds;
	public float minSoundInterval = 2f;// seconds
	public float maxSoundInterval = 10f;
	private float soundTimer;


	// Start is called before the first frame update
	void Start()
    {
        //always works
        rb = GetComponent<Rigidbody>();
		soundEffect = GetComponent<AudioSource>();

        MoveToNextNode();
    }

	// Update is called once per frame
	void Update()
	{
		if(pathNodes.Count == 0) return;

		float distance = Vector3.Distance(transform.position, pathNodes[currentTargetIndex].position);

		if(distance < DistanceThreshhold)
		{
			MoveToNextNode();
		}
		else
		{
			Vector3 direction = (pathNodes[currentTargetIndex].position - transform.position).normalized;
			rb.MovePosition(transform.position + direction * moveSpeed * MoveSpeedMultiplier * Time.deltaTime);
		}

		DoScale();

		// SOUND
		soundTimer -= Time.deltaTime;
		if(soundTimer <= 0)
		{
			int randomIndex = Random.Range(0, slimeSounds.Length);
			soundEffect.PlayOneShot(slimeSounds[randomIndex]);
			soundTimer = Random.Range(minSoundInterval, maxSoundInterval);
		}
	}

	void DoScale()
	{
		if(scalingUp)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, maxScale, scaleSpeed * Time.deltaTime);
			if(Vector3.Distance(transform.localScale, maxScale) < 0.01f)
			{
				scalingUp = false;
			}
		}
		else
		{
			transform.localScale = Vector3.Lerp(transform.localScale, minScale, scaleSpeed * Time.deltaTime);
			if(Vector3.Distance(transform.localScale, minScale) < 0.01f)
			{
				scalingUp = true;
			}
		}
		
	}

	void MoveToNextNode()
	{
		currentTargetIndex = (currentTargetIndex + 1) % pathNodes.Count;
	}

	void OnDrawGizmos()
	{
		if(pathNodes != null && pathNodes.Count > 0)
		{
			for(int i = 0; i < pathNodes.Count; i++)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(pathNodes[i].position, 0.3f);
				if(i < pathNodes.Count - 1)
				{
					Gizmos.color = Color.yellow;
					Gizmos.DrawLine(pathNodes[i].position, pathNodes[i + 1].position);
				}
				else
				{
					Gizmos.color = Color.yellow;
					Gizmos.DrawLine(pathNodes[i].position, pathNodes[0].position);
				}
			}
		}
	}
}
