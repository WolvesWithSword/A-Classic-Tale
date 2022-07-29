using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RunForwardMotor : MonoBehaviour
{
    public Tilemap baseTilemap;
    public EDirection direction;
    public int speed = 5;

    private GameObject player;
    private Transform stopPoint;
    private bool runIntoPlayer = false;
    private bool hasRun = false;
    private Vector3 startPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stopPoint = transform.parent.GetComponentsInChildren<Transform>()[2];
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasRun)
        {
            if (runIntoPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, stopPoint.position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, stopPoint.position) < 0.05f)
                {
                    transform.position = stopPoint.position;
                    runIntoPlayer = false;
                    hasRun = true;
                }
            }
            else
            {
                DetectPlayer(direction);
            }
        }
    }

    private void DetectPlayer(EDirection direction)
    {
        Vector3Int playerCell = baseTilemap.WorldToCell(player.transform.position);
        Vector3Int myCell = baseTilemap.WorldToCell(transform.position);

        switch (direction)
        {
            case EDirection.RIGHT:
                if (playerCell.y == myCell.y && playerCell.x > myCell.x)
                    RunIntoPlayer();
                break;
            case EDirection.LEFT:
                if (playerCell.y == myCell.y && playerCell.x < myCell.x)
                    RunIntoPlayer();
                break;
            case EDirection.UP:
                if (playerCell.y > myCell.y && playerCell.x == myCell.x)
                    RunIntoPlayer();
                break;
            case EDirection.DOWN:
                if (playerCell.y < myCell.y && playerCell.x == myCell.x)
                    RunIntoPlayer();
                break;
            default:
                break;
        }
    }
    
    public void RunIntoPlayer()
    {
        AudioManager.Instance.HauntedTreeGrowl();
        runIntoPlayer = true;
    }
    public void ResetPosition()
    {
        transform.position = startPosition;
        runIntoPlayer = false;
        hasRun = false;
    }
}
