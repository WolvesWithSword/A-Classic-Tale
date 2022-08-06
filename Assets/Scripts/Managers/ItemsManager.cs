using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    #region SINGLETON
    private static ItemsManager instance;
    public static ItemsManager Instance { get { return instance; } } // Accessor

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);// To delete previous instance if exist
            return;
        }
        instance = this;
    }
    #endregion

    // Contain all heart of the game and their state : ie pickup or not.
    private Dictionary<string, bool> pickedUpGameHearts = new Dictionary<string, bool>();

    public bool HasTakeAxe
    {
        get;
        set;
    }

    public void OnHeartDiscovered(Heart discoveredHeart)
    {
        if (pickedUpGameHearts.ContainsKey(discoveredHeart.zoneTag))
        {
            discoveredHeart.gameObject.SetActive(!pickedUpGameHearts[discoveredHeart.zoneTag]);
        }
        else
        {
            pickedUpGameHearts.Add(discoveredHeart.zoneTag, false);
        }
    }

    public void OnHeartPickedUp(Heart pickedUpHeart)
    {
        if (pickedUpGameHearts.ContainsKey(pickedUpHeart.zoneTag))
        {
            pickedUpGameHearts[pickedUpHeart.zoneTag] = true;
        }
    }

    public void ResetHeartsInMap()
    {
        pickedUpGameHearts.Clear();
    }

}
