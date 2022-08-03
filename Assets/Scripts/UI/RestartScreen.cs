using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScreen : MonoBehaviour
{
    public void Show(bool show)
    {
        this.gameObject.SetActive(show);
    }

    public void RestartButton()
    {
        GameManager.Instance.RetryLevel();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameManager.Instance.RetryLevel();
        }
    }
}
