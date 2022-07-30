using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMotor : MonoBehaviour
{
    public float speed = 0.2f;
    private PathFlags pathFlags;
    private PathFlags startPathFlags;
    public Animator animator;
    public bool loop = true;

    private int flagIndex = 0;

    void Start()
    {
        pathFlags = transform.parent.Find("Path").gameObject.GetComponent<PathFlags>();
        startPathFlags = transform.parent.Find("StartPath")?.gameObject.GetComponent<PathFlags>();
    }

    void Update()
    {

        if (startPathFlags == null) LoopPath();
        else StartPath();
    }

    private void LoopPath()
    {
        Vector3 flagPosition = pathFlags.flags[flagIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, flagPosition, speed * Time.deltaTime);
        Vector3 direction = (transform.position - flagPosition).normalized;

        animator.SetFloat("Horizontal", -direction.x);
        animator.SetFloat("Vertical", -direction.y);

        if (Vector2.Distance(transform.position, flagPosition) < 0.05f)
        {
            flagIndex++;// Move to next position

            if (flagIndex > pathFlags.flags.Length - 1)
            {
                if (loop)
                {
                    flagIndex = 0;// Loop
                    return;
                }
                Destroy(gameObject);
            }
        }
    }

    private void StartPath()
    {
        Vector3 flagPosition = startPathFlags.flags[flagIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, flagPosition, speed * Time.deltaTime);
        Vector3 direction = (transform.position - flagPosition).normalized;

        animator.SetFloat("Horizontal", -direction.x);
        animator.SetFloat("Vertical", -direction.y);

        if (Vector2.Distance(transform.position, flagPosition) < 0.05f)
        {
            flagIndex++;// Move to next position
            if (flagIndex > startPathFlags.flags.Length - 1)
            {
                startPathFlags = null;
                flagIndex = 0;
            }
        }
    }
}
