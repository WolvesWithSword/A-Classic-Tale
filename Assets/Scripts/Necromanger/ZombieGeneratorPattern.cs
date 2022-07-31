using System.Collections;
using UnityEngine;

public class ZombieGeneratorPattern : IZombiePattern
{
    public GameObject zombiePrefab;
    public float timeBetweenInvocation;
    public bool StopInvoking = false;

    public override IEnumerator RunPattern(int phase)
    {
        while(!StopInvoking)
        {
            InvokeZombie(zombiePrefab);
            yield return new WaitForSeconds(timeBetweenInvocation);
        }
    }

    public override void CleanPattern(int phase)
    {
        base.CleanPattern(phase);
        StopInvoking = true;
    }
}
