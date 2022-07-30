using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMotor : MonoBehaviour
{
    public float speed = 0.2f;
    private PathFlags pathFlags;
    public Animator animator;

    private int flagIndex = 0;

    void Start()
    {
        pathFlags = transform.parent.Find("Path").gameObject.GetComponent<PathFlags>();
    }

    void Update()
    {
        Vector3 flagPosition = pathFlags.flags[flagIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, flagPosition, speed * Time.deltaTime);
        Vector3 direction = (transform.position - flagPosition).normalized;

        animator.SetFloat("Horizontal", -direction.x);
        animator.SetFloat("Vertical", -direction.y);

        if (Vector2.Distance(transform.position, flagPosition) < 0.05f)
        {
            flagIndex++;// Move to next position

            if(flagIndex > pathFlags.flags.Length -1)
            {
                flagIndex = 0;// Loop
            }
        }
    }
}
