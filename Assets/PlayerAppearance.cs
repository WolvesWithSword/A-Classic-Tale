using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{

    private SpriteRenderer playerSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void MakePlayerFlash()
    {
        StartCoroutine(PlayerFlash());
    }
    public void StopPlayerFlash()
    {
        playerSpriteRenderer.color = new Color(1, 1, 1);
        StopAllCoroutines();
    }

    private IEnumerator PlayerFlash()
    {
        bool isRed = true;
        while (true)
        {
            if (isRed)
            {
                playerSpriteRenderer.color = Color.red;
            }
            else
            {
                playerSpriteRenderer.color = new Color(1,1,1);
            }
            isRed = !isRed;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
