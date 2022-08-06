using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlaySlashAnimation()
    {
        animator.Play("Slash");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            PlaySlashAnimation();
        }
    }
}
