using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCirclePattern : IZombiePattern
{
    public GameObject[] circle3;
    public GameObject[] circle2;
    public GameObject[] circle1;

    public override IEnumerator RunPattern(int phase)
    {
        foreach (GameObject zombie in circle3)
        {
            InvokeZombie(zombie);
        }
        yield return new WaitForSeconds(1);

        foreach (GameObject zombie in circle2)
        {
            InvokeZombie(zombie);
        }
        yield return new WaitForSeconds(1);

        foreach (GameObject zombie in circle1)
        {
            InvokeZombie(zombie);
        }
    }
}
