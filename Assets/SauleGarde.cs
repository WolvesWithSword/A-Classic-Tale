using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SauleGarde : MonoBehaviour, IInteractable
{
    public string respawnScene;
    public Tilemap foreground;
    public Tile blockingTile;

    private void Start()
    {
        foreground.SetTile(foreground.WorldToCell(transform.position), blockingTile);
    }

    public void Interact()
    {
        GameManager.Instance.ChangeRespawnScene(respawnScene);
    }
}
