using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromanger : MonoBehaviour, IInteractable 
{
    public GameObject zombieShield;
    public float phase1Time = 20f;
    public float phase2Time = 30f;
    public float phase3Time = 35f;
    public float weakTime = 5f;
    public int life = 3;
    public Transform pushPoint;

    public ZombieCirclePattern zombieCirclePattern;
    public ZombieGeneratorPattern zombieSpiralPattern;
    public ZombieGeneratorPattern zombieSlalomPattern;

    private bool havePatternRunning = false;
    private int phase = 4;
    private bool isNecroMangerWeak = false;
    private IEnumerator currentCoroutine;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isNecroMangerWeak || havePatternRunning) return;

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
        currentCoroutine = pattern.RunPattern();
        StartCoroutine(currentCoroutine);
        havePatternRunning = true;

        yield return new WaitForSeconds(runningTime);

        pattern.CleanPattern();
        StopCoroutine(currentCoroutine);
        havePatternRunning = false;
        phase++;
        StartCoroutine(NecromangerWeakPhase());
    }

    private IEnumerator NecromangerWeakPhase()
    {
        isNecroMangerWeak = true;
        zombieShield.SetActive(false);
        yield return new WaitForSeconds(weakTime);

        //PlayerManager.Instance.MovePlayerTo(pushPoint.position, 17f);
        yield return new WaitForSeconds(0.5f);

        zombieShield.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        isNecroMangerWeak = false;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
