using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public string zoneTag;
    public int restoredAmount = 1;

    private void Start()
    {
        ItemsManager.Instance.OnHeartDiscovered(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!PlayerManager.Instance.playerStats.IsFullLife())
            {
                PlayerManager.Instance.PlayerPickupHeart(restoredAmount);
                ItemsManager.Instance.OnHeartPickedUp(this);
                Destroy(gameObject);
            }
        }
    }
}
