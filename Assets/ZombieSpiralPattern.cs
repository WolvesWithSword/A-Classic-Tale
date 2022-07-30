using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpiralPattern : MonoBehaviour
{
    public GameObject zombiePrefab;
    private List<GameObject> zombiesInstantiate = new List<GameObject>();
    public int numberOfInvocation;

    public IEnumerator RunPattern()
    {
        for (int i = 0; i < numberOfInvocation; i++)
        {
            InvokeZombie(zombiePrefab);
            yield return new WaitForSeconds(2);
        }
    }

    private void InvokeZombie(GameObject zombie)
    {
        GameObject zombieInstantiate = Instantiate(zombie, zombie.transform.position, Quaternion.identity, gameObject.transform);
        zombieInstantiate.SetActive(true);
        zombiesInstantiate.Add(zombieInstantiate);
    }

    public void CleanPattern()
    {
        foreach (GameObject zombie in zombiesInstantiate)
        {
            Destroy(zombie);
        }
    }
}
