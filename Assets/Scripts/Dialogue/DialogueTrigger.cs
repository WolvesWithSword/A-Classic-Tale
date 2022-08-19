using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public Dialogue dialogue;

	public delegate void OnDialogueEnd();
	public OnDialogueEnd onDialogueEnd;

	public void TriggerDialogue()
	{
		DialogueManager.Instance.StartDialogue(dialogue, OnDialogueEndCallback);
	}

	public void OnDialogueEndCallback()
	{
		onDialogueEnd?.Invoke();
	}
}

[System.Serializable]
public class Dialogue
{

	public string name;

	[TextArea(3, 10)]
	public string[] sentences;

	public Sprite characterPicture;
}
