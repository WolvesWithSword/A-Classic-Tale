using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromanger : MonoBehaviour
{
    public GameObject zombieShield;
    public ZombieCirclePattern zombieCirclePattern;
    public ZombieSpiralPattern zombieSpiralPattern;

    private float time = 0;

    void Start()
    {
        StartCoroutine(zombieSpiralPattern.RunPattern());
    }

    // Update is called once per frame
    void Update()
    {

       /* time += Time.deltaTime;

        if (time >= 10f)
        {
            time = 0;
            zombieSpiralPattern.CleanPattern();
        }*/
    }
}
