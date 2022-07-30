using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromanger : MonoBehaviour
{
    public GameObject zombieShield;
    public ZombieCirclePattern zombieCirclePattern;
    public ZombieGeneratorPattern zombieSpiralPattern;
    public ZombieGeneratorPattern zombieSlalomPattern;

    private float time = 0;
    private bool havePatternRunning = false;
    private int phase = 4;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*if (phase == 1)
        {
            RunPhase(zombieCirclePattern, 5f);
        }*/

        /*if (phase == 2)
        {
            RunPhase(zombieSpiralPattern, 3f);
        }

        if (phase == 3)
        {
            RunPhase(zombieSlalomPattern, 3f);
        }*/
        if (phase > 3)
        {
            int choice = Random.Range(1, 4);
            switch (choice)
            {
                case 1:
                    RunPhase(zombieCirclePattern, 3f);
                    break;
                case 2:
                    RunPhase(zombieSpiralPattern, 3f);
                    break;
                case 3:
                    RunPhase(zombieSlalomPattern, 3f);
                    break;
                default:
                    break;
            }
        }
    }

    private void RunPhase(IZombiePattern pattern,float runningTime)
    {
        if (!havePatternRunning)
        {
            time = 0;
            if (pattern is ZombieGeneratorPattern)
            {
                (pattern as ZombieGeneratorPattern).StopInvoking = false;
            }
            StartCoroutine(pattern.RunPattern(phase));
            havePatternRunning = true;
        }
        time += Time.deltaTime;
        if (time >= runningTime)
        {
            time = 0;
            pattern.CleanPattern(phase);
            StopAllCoroutines();
            havePatternRunning = false;
            phase++;
        }
    }
}
