using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public void Show(bool show)
    {
        this.gameObject.SetActive(show);
    }

    public void TryAgainButton()
    {
        GameManager.Instance.RestartGame();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameManager.Instance.RestartGame();
        }
    }
}
