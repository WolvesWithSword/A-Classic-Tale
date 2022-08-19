using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogueTrigger : MonoBehaviour
{
    public Transform triggerPosition;
    public DialogueTrigger dialogueTrigger;

    private GameObject player;
    private bool hasTrigger = false;

    void Start()
    {
        if (GameEventManager.Instance.hasDefeatedNecromanger)
        {
            Destroy(gameObject);
            return;
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!hasTrigger)
        {
            if (Vector3.Distance(player.transform.position, triggerPosition.position) <= 0.5)
            {
                dialogueTrigger.TriggerDialogue();
                hasTrigger = true;
            }
        }
    }
}
