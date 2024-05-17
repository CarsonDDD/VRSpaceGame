using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WasdMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rb.Move(new Vector3(speed, 0, 0),Quaternion.identity);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.Move(new Vector3(0, 0, -speed), Quaternion.identity);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.Move(new Vector3(speed, 0, 0), Quaternion.identity);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.Move(new Vector3(0, 0, speed), Quaternion.identity);
        }
    }
}
