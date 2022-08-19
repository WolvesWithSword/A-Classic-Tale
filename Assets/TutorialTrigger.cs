using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    DialogueTrigger dialogueTrigger;

    private bool hasTrigger = false;
    void Start()
    {
        if (GameEventManager.Instance.hasSeenTutorial)
        {
            Destroy(gameObject);
            return;
        }
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueTrigger.onDialogueEnd = OnTutorialEnd;
    }

    private void Update()
    {
        if (hasTrigger) return;
        StartCoroutine(StartTutorial());
        hasTrigger = true;

    }

    private void OnTutorialEnd()
    {
        PlayerManager.Instance.BlockPlayerMovement(false);
        GameEventManager.Instance.hasSeenTutorial = true;
    }

    private IEnumerator StartTutorial()
    {
        PlayerManager.Instance.BlockPlayerMovement(true);
        yield return new WaitForSeconds(0.5f);
        dialogueTrigger.TriggerDialogue();
    }
}
