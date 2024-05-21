using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class CameraControlButton : MonoBehaviour
{
    [SerializeField] public Computer computerReference;
    private Toggle toggle;
    public int cameraId = -1;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if(toggle.isOn)
        {
            computerReference.SetCamera(cameraId);
        }
    }
}
