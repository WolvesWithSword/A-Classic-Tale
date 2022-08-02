using System.Collections;
using UnityEngine;

public class ZombieGeneratorPattern : IZombiePattern
{
    public GameObject zombiePrefab;
    public float timeBetweenInvocation;
    public bool StopInvoking = false;
    public GameObject spawnPrefab;

    private GameObject spawnObject;
        
    public override IEnumerator RunPattern()
    {
        spawnObject = Instantiate(spawnPrefab, zombiePrefab.transform.position, Quaternion.identity, gameObject.transform);
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
        Destroy(spawnObject);
    }
}
