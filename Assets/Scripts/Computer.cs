using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
	// Start is called before the first frame update

	[SerializeField] private int currentDisplayIndex = -1;
    [SerializeField] private Camera[] cameras;

    [SerializeField] private RenderTexture cameraTargetTexture;

    [SerializeField] private GameObject CameraControlButtonPrefab;
    [SerializeField] private GameObject CameraControlButtonRoot;

	public bool SwitchCamera { 
        get { return true; } 
        set { CycleDisplay(); } }

    void Start()
    {
        InitalizeCameras();//maybe needs to call in OnAwake()?!?!?
        if(cameras != null  && cameras.Length > 0)
        {
            SetCamera(3);
		}
        else
        {
            Debug.Log("There are 0 cameras in the scene for the computer to initalize");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CycleDisplay()
    {
        if(cameras.Length == 0) return;

        SetCamera((currentDisplayIndex + 1) % cameras.Length);

    }

    public void SetCamera(int index)
    {
        if(index < 0 || index >= cameras.Length) return;

        if(currentDisplayIndex != -1)
        {
            cameras[currentDisplayIndex].targetTexture = null;
            cameras[currentDisplayIndex].gameObject.transform.parent.gameObject.SetActive(false);
        }

		currentDisplayIndex = index;
		cameras[currentDisplayIndex].targetTexture = cameraTargetTexture;
		cameras[currentDisplayIndex].gameObject.transform.parent.gameObject.SetActive(true);

	}

    void InitalizeCameras()
    {
        GameObject[] cams = GameObject.FindGameObjectsWithTag("SecurityCamera");
		//cameras = new Camera[cams.Length];
		List<(Camera, int)> cameraTemp = new List<(Camera, int)>();
		Debug.Log("Total Security Cameras in Scene: " + cams.Length);
        for(int i=0; i < cams.Length; i++)
        {
            Camera camera = cams[i].GetComponentInChildren<Camera>();
            camera.targetTexture = null;
            //camera.enabled = false;
            int id = cams[i].GetComponentInChildren<SecurityCamera>().ID;
            cameraTemp.Add((camera, id));
			//cameras[i] = cams[i].GetComponentInChildren<Camera>();
            //cameras[i].targetTexture = null;
            //cameras[i].enabled = false;
        
            Debug.Log($"Added {cams[i].name} to cameras!");
            cams[i].gameObject.SetActive(false);
 
		}

		cameraTemp.Sort((x,y) => x.Item2.CompareTo(y.Item2));// modern csharp moment
		 // this function is called once at start up, I dont care that it is slow and sucks
		cameras = new Camera[cameraTemp.Count];
		for(int i =0; i< cameraTemp.Count; i++)
        {
            cameras[i] = cameraTemp[i].Item1;
        }
		Debug.Log("Initalized all cameras: " + cameras.Length);

		/*for(int i = 0; i < cameras.Length; i++)
        {
			Debug.Log($"({cameras.Length}:{i})Creating button for camera {i}.");

			GameObject controlButton = Instantiate(CameraControlButtonPrefab, CameraControlButtonRoot.transform);


			CameraControlButton but = controlButton.GetComponent<CameraControlButton>();
			but.cameraId = i;
			but.computerReference = this;
			Debug.Log($"Button {i} created successfully.");

			Toggle toggle = controlButton.GetComponent<Toggle>();
			//toggle.isOn = (i == 0);
			toggle.group = CameraControlButtonRoot.GetComponent<ToggleGroup>();
		}*/


	}
}
