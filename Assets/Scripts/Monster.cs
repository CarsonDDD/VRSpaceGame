using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Monster : MonoBehaviour
{
    [SerializeField] private List<Transform> pathnodes;
    [SerializeField] private float movespeed;
    public float MoveSpeedMultiplier = 1f;

    private Transform currentTarget;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //always works
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //go to next path
        // if path is reached, go to next
        // use unity task?!?!?!?!?!
        // What if something is blocking??!?!?!
    }

    IEnumerator WalkToTarget()
    {
        Vector3 distance = rb.position - currentTarget.position;

        if(distance. < BUFFERLK)
        {
            //switch target
            yield return 0;
        }

        distance = distance.normalized;// for direction
        rb.MovePosition(distance * movespeed * MoveSpeedMultiplier);
        yield return 1;
    }
}
