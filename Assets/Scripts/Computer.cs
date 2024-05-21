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

	public bool SwitchCamera { 
        get { return true; } 
        set { CycleDisplay(); } }

    void Start()
    {
        InitalizeCameras();//maybe needs to call in OnAwake()?!?!?
        if(cameras != null  && cameras.Length > 0)
        {
            SetCamera(0);
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

    private void SetCamera(int index)
    {
        if(index < 0 || index >= cameras.Length) return;

        if(currentDisplayIndex != -1)
        {
            cameras[currentDisplayIndex].targetTexture = null;
            cameras[currentDisplayIndex].enabled = false;
        }

		currentDisplayIndex = index;
		cameras[currentDisplayIndex].targetTexture = cameraTargetTexture;
		cameras[currentDisplayIndex].enabled = true;

	}

    void InitalizeCameras()
    {
        GameObject[] cams = GameObject.FindGameObjectsWithTag("SecurityCamera");
        cameras = new Camera[cams.Length];
        for(int i=0; i < cams.Length; i++)
        {
            cameras[i] = cams[i].GetComponentInChildren<Camera>();
            cameras[i].targetTexture = null;
            //cameras[i].enabled = false;
        
            Debug.Log($"Added {cams[i].name} to cameras!");
        }
    }
}
