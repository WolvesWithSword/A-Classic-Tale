using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SauleGarde : MonoBehaviour, IInteractable
{
    public string respawnScene;
    public Tilemap foreground;
    public Tile blockingTile;
    public Image flashImage;


    private void Start()
    {
        foreground.SetTile(foreground.WorldToCell(transform.position), blockingTile);
    }

    public void Interact()
    {
        GameManager.Instance.ChangeRespawnScene(respawnScene);
        MakeFlash();
    }

    public void MakeFlash()
    {
        StartCoroutine(MakeFlashCoroutine());
    }

    public IEnumerator MakeFlashCoroutine()
    {
        float flashTime = 1.2f;
        float flashOpacity = 0f;
        float fadeTime = 1 / (flashTime/2);
        bool reachMax = false;

        PlayerManager.Instance.BlockPlayerMovementFor(flashTime);
        while (flashTime > 0)
        {
            flashTime -= Time.deltaTime;
            if (flashOpacity >= 0.99f)
            {
                reachMax = true;
                yield return new WaitForSeconds(0.3f);
            }

            if (reachMax)
            {
                flashOpacity = Mathf.MoveTowards(flashOpacity, 0, fadeTime * Time.deltaTime);
            }
            else
            {
                flashOpacity = Mathf.MoveTowards(flashOpacity, 1f, fadeTime * Time.deltaTime);
            }
            flashImage.color = new Color(1, 1, 1, flashOpacity);

            yield return null;
        }
    }

}
