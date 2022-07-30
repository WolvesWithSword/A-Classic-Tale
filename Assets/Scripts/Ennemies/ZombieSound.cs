using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    private float time = 0.0f;
    private float interpolationPeriod;

    private void Start()
    {
        interpolationPeriod = Random.Range(5, 10);
    }
    void Update()
    {
        time += Time.deltaTime;

        if (time >= interpolationPeriod)
        {
            time = time - interpolationPeriod;
            interpolationPeriod = Random.Range(8, 15);
            AudioManager.Instance.ZombieGrowl();
        }
    }
}
