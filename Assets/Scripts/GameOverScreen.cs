using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Show(bool show)
    {
        this.gameObject.SetActive(show);
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene("Scene1");
    }
}
