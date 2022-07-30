using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCirclePattern : MonoBehaviour
{
    public GameObject[] circle4;
    public GameObject[] circle3;
    public GameObject[] circle2;
    public GameObject[] circle1;

    private List<GameObject> zombiesInstantiate = new List<GameObject>();

    public IEnumerator RunPattern()
    {
        foreach (GameObject zombie in circle4)
        {
            InvokeZombie(zombie);
        }
        yield return new WaitForSeconds(1);

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
