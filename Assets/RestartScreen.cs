using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScreen : MonoBehaviour
{
    public delegate void OnRestart();
    public OnRestart onRestart;

    public void Setup(OnRestart onRestartDel)
    {
        onRestart = onRestartDel;
    }
    public void Show(bool show)
    {
        this.gameObject.SetActive(show);
    }

    public void RestartButton()
    {
        onRestart.Invoke();
    }
}
