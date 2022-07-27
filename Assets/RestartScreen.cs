using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScreen : MonoBehaviour
{
    public delegate void OnRestart();

    public void Show(bool show)
    {
        this.gameObject.SetActive(show);
    }

    public void RestartButton()
    {
        GameManager.Instance.RetryLevel();
    }
}
