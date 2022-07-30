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
        if (!hasRun)// No move anymore
        {
            if (runIntoPlayer)// When ennemy have seen the player
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
        if (!CanSeePlayer()) return;// No need to go further if can't see player

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
    
    private void RunIntoPlayer()
    {
        AudioManager.Instance.HauntedTreeGrowl();
        runIntoPlayer = true;
    }

    private bool CanSeePlayer()
    {
        float playerDist = Vector3.Distance(player.transform.position, transform.position);
        // Stop point is like vision range of the ennemy
        float visionRange = Vector3.Distance(stopPoint.position, transform.position);

        if (playerDist <= visionRange) return true;
        return false;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        runIntoPlayer = false;
        hasRun = false;
    }
}
