using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossDoorSystem : MonoBehaviour 
{
    public Tilemap foreground;
    public Transform doorPos;
    public Tile doorTile;
    public Transform closeTriggerPos;

    private Vector3Int doorTilePos;
    private GameObject player;
    private bool hasTrigger = false;// To be sure to open/close one time

    public delegate void OnDoorClosing();
    public OnDoorClosing onDoorClosing;

    void Start()
    {
        doorTilePos = foreground.WorldToCell(doorPos.position);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!hasTrigger)
        {
            if (Vector3.Distance(player.transform.position, closeTriggerPos.position) <= 0.5)
            {
                CloseDoor();
            }
        }
    }

    public void OpenDoor()
    {
        foreground.SetTile(doorTilePos, null);
    }

    public void CloseDoor()
    {
        foreground.SetTile(doorTilePos, doorTile);
        hasTrigger = true;
        onDoorClosing();
    }
}
