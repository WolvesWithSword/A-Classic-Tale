using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour, IInteractable
{
    public Sprite withAxe;
    public Sprite withoutAxe;

    private bool hasAxe = true;
    private SpriteRenderer spriteRenderer;

    public void Interact()
    {
        if (hasAxe)
        {
            ItemsManager.Instance.HasTakeAxe = true;
            spriteRenderer.sprite = withoutAxe;
            PlayerManager.Instance.playerStats.HasAxe = true;
            hasAxe = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hasAxe = !ItemsManager.Instance.HasTakeAxe;
        if (hasAxe)
        {
            spriteRenderer.sprite = withAxe;
        }
        else
        {
            spriteRenderer.sprite = withoutAxe;
        }
    }
}
