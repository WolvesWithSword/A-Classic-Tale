using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region SINGLETON
    private static DialogueManager instance;
    public static DialogueManager Instance { get { return instance; } } // Accessor

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);// To delete previous instance if exist
            return;
        }
		instance = this;
    }
	#endregion

	public Image dialogueBox;
	public Image PNJPicture;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;

	private Queue<string> sentences;
	private bool hasDialogueRunning = false;

	public delegate void OnDialogueEnd();
	public OnDialogueEnd onDialogueEnd;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
	}

	private void Update()
	{
		if (!hasDialogueRunning) return; 

		if (Input.GetKeyDown(KeyCode.Space))
		{
			DisplayNextSentence();
		}
	}

	public void StartDialogue(Dialogue dialogue, OnDialogueEnd onDialogueEndCallback)
	{
		dialogueBox.gameObject.SetActive(true);
		onDialogueEnd = onDialogueEndCallback;
		nameText.text = dialogue.name;
		PNJPicture.sprite = dialogue.characterPicture;
		sentences.Clear();
		hasDialogueRunning = true;

		PlayerManager.Instance.BlockPlayerMovement(true);

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		onDialogueEnd();
		hasDialogueRunning = false;

		PlayerManager.Instance.BlockPlayerMovement(false);

		dialogueBox.gameObject.SetActive(false);
		onDialogueEnd = null;
	}
}
