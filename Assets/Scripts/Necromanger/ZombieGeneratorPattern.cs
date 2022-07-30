using System.Collections;
using UnityEngine;

public class ZombieGeneratorPattern : IZombiePattern
{
    public GameObject zombiePrefab;
    public float timeBetweenInvocation;
    public bool StopInvoking = false;

    public override IEnumerator RunPattern()
    {
        while(!StopInvoking)
        {
            InvokeZombie(zombiePrefab);
            yield return new WaitForSeconds(timeBetweenInvocation);
        }
    }

    public override void CleanPattern()
    {
        base.CleanPattern();
        StopInvoking = true;
    }
}
