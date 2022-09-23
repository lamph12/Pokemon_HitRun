using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap_gai : MonoBehaviour
{
    private float speed = 2.5f;
    private Vector3[] positions = new Vector3[2];
    private int index = 0;
    private Vector3 from;
    private Vector3 to;
    void Awake()
    {   

        from =new Vector3(-4, transform.localPosition.y, transform.localPosition.z);
        to = new Vector3(4, transform.localPosition.y, transform.localPosition.z);
        positions[0] = from;
        positions[1] = to;

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, positions[index], Time.deltaTime * speed);
        if (transform.localPosition == positions[index])
        {
            if (index == positions.Length - 1)
                index = 0;
            else
                index++;
        }
    }
}
