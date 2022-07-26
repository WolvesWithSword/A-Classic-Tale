using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health;

    public Image[] heartImages;

    private void Update()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if(i < health)
            {
                heartImages[i].enabled = true;
            }
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }
}
