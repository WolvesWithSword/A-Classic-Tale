using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Necromanger : MonoBehaviour, IInteractable 
{
    public float phase1Time = 20f;
    public float phase2Time = 30f;
    public float phase3Time = 35f;
    public float weakTime = 5f;
    public int life = 3;

    public GameObject zombieShield;
    public ZombieCirclePattern zombieCirclePattern;
    public ZombieGeneratorPattern zombieSpiralPattern;
    public ZombieGeneratorPattern zombieSlalomPattern;

    public Tilemap foreground;
    public Transform pushPoint;
    public Transform doorPoint;
    public Tile doorTile;
    private Vector3Int doorTilePos;

    private bool havePatternRunning = false;
    private int phase = 4;
    private bool isNecromangerWeak = false;
    private IEnumerator currentPhase;

    void Start()
    {
        doorTilePos = foreground.WorldToCell(doorPoint.position);
        foreground.SetTile(doorTilePos, doorTile);
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            foreground.SetTile(doorTilePos, null);
            Destroy(gameObject);
        }

        if (isNecromangerWeak || havePatternRunning) return;

        if (phase == 1)
        {
            StartCoroutine(RunPhase(zombieCirclePattern, phase1Time));
        }

        if (phase == 2)
        {
            StartCoroutine(RunPhase(zombieSpiralPattern, phase2Time));
        }

        if (phase == 3)
        {
            StartCoroutine(RunPhase(zombieSlalomPattern, phase3Time));
        }
        if (phase > 3)
        {
            int choice = Random.Range(1, 4);
            switch (choice)
            {
                case 1:
                    StartCoroutine(RunPhase(zombieCirclePattern, phase1Time));
                    break;
                case 2:
                    StartCoroutine(RunPhase(zombieSpiralPattern, phase2Time));
                    break;
                case 3:
                    StartCoroutine(RunPhase(zombieSlalomPattern, phase3Time));
                    break;
                default:
                    break;
            }
        }
    }

    private IEnumerator RunPhase(IZombiePattern pattern,float runningTime)
    {
        if (pattern is ZombieGeneratorPattern)
        {
            (pattern as ZombieGeneratorPattern).StopInvoking = false;
        }
        currentPhase = pattern.RunPattern();
        StartCoroutine(currentPhase);
        havePatternRunning = true;

        yield return new WaitForSeconds(runningTime);

        pattern.CleanPattern();
        StopCoroutine(currentPhase);
        havePatternRunning = false;
        phase++;

        currentPhase = NecromangerWeakPhase();
        StartCoroutine(currentPhase);
    }

    private IEnumerator NecromangerWeakPhase()
    {
        isNecromangerWeak = true;
        zombieShield.SetActive(false);
        yield return new WaitForSeconds(weakTime);
        StartCoroutine(RestoreShield());
    }

    private IEnumerator RestoreShield()
    {
        StopCoroutine(currentPhase);
        PlayerManager.Instance.SetPlayerPosition(EPlayerPosition.UP);
        PlayerManager.Instance.MovePlayerTo(pushPoint.position, 17f);
        yield return new WaitForSeconds(0.5f);

        zombieShield.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        isNecromangerWeak = false;
    }

    public void Interact()
    {
        if (isNecromangerWeak)
        {
            StartCoroutine(RestoreShield());
            life--;
        }
    }
}
