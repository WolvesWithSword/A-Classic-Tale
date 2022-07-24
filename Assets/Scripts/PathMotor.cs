using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMotor : MonoBehaviour
{
    public float speed = 0.2f;
    private PathFlags pathFlags;

    private int flagIndex = 0;

    void Start()
    {
        pathFlags = transform.parent.Find("Path").gameObject.GetComponent<PathFlags>();
    }

    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, pathFlags.flags[flagIndex].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, pathFlags.flags[flagIndex].position) < 0.05f)
        {
            flagIndex++;

            if(flagIndex > pathFlags.flags.Length -1)
            {
                flagIndex = 0;
            }
        }
    }
}
