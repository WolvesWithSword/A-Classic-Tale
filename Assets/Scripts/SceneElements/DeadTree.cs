using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DeadTree : MonoBehaviour, IInteractable
{
    public Tilemap foreground;
    public Tile blockingTile;

    private SlashEffect slashEffect;

    // Start is called before the first frame update
    void Start()
    {
        slashEffect = GetComponentInChildren<SlashEffect>();
        foreground.SetTile(foreground.WorldToCell(transform.position), blockingTile);
    }

    public void Interact()
    {
        if (PlayerManager.Instance.playerStats.HasAxe)
        {
            slashEffect.PlaySlashAnimation();
            StartCoroutine(CutTree());
        }
        
    }

    private IEnumerator CutTree()
    {
        yield return new WaitForSeconds(0.3f);
        foreground.SetTile(foreground.WorldToCell(transform.position), null);
        Destroy(gameObject);
    }
}
