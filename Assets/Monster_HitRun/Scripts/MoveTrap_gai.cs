using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap_gai : MonoBehaviour
{
    private float speed=2.3f;
    private Vector3[] positions;
    private int index = 0;
    private Vector3 from;
    private Vector3 to;
    void Start()
    {
        from =new Vector3(-4, transform.localPosition.y, transform.localPosition.z);
        to = new Vector3(4, transform.localPosition.y, transform.localPosition.z);
        positions[0] = from;
        positions[1] = to;
        Debug.Log(transform.position.y);
        Debug.Log(transform.position.z);

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
