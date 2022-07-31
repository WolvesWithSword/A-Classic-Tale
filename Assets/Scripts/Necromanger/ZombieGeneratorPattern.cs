using System.Collections;
using UnityEngine;

public class ZombieGeneratorPattern : IZombiePattern
{
    public GameObject zombiePrefab;
    public float timeBetweenInvocation;
    public bool StopInvoking = false;

    public override IEnumerator RunPattern(int phase)
    {
        Debug.Log("LAUNCH"+phase);
        while(!StopInvoking)
        {
            InvokeZombie(zombiePrefab);
            yield return new WaitForSeconds(timeBetweenInvocation);
        }
        Debug.Log("EXIT LAUNCH" + phase);
    }

    public override void CleanPattern(int phase)
    {
        base.CleanPattern(phase);
        Debug.Log("CLEAN" +phase);
        StopInvoking = true;
    }
}
