using System;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Current {get; private set;}
    public TextMeshProUGUI text;
    public bool drawDialogueTree = false;
    public GameObject[] answerChoices;

    private bool dialogueActive = false;
    private float textDuration = 20f;
    private float time;
    private int loadedDialogueIndex = 0;
    private DialogueNode LoadedDialogueNode;
    
    public DialogueNode startingDialogueNode;
    public Typewriter typewriter;
    private void Awake()
    {
        if (Current != null && Current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Current = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        StartDialogue(startingDialogueNode);
    }

    public void PathSelected(int buttonClicked)
    {
        for (int i = 0; i < answerChoices.Length; i++)
        {
            answerChoices[i].SetActive(false);
        }
        StartNextNode(buttonClicked);
    }

    public void StartDialogue(DialogueNode dialogueNode)
    {
        loadedDialogueIndex = 0;
        dialogueActive = true;
        LoadedDialogueNode = dialogueNode;
        dialogueNode.StartNodeFromStart();
    }

    public void StartNextNode(int pathIndex)
    {
        
        switch (pathIndex)
        {
            case 0:
                LoadedDialogueNode.questionNodes[0].NextDialogue.StartNodeFromStart();
                return;
            case 1:
                LoadedDialogueNode.questionNodes[1].NextDialogue.StartNodeFromStart();
                return;
            case 2:
                LoadedDialogueNode.questionNodes[2].NextDialogue.StartNodeFromStart();
                return;
            case 3:
                LoadedDialogueNode.questionNodes[3].NextDialogue.StartNodeFromStart();
                return;
        }
    }
    
    public void OptionSelected()
    {
        
    }

    public void SetLoadedDialogue(DialogueNode dialogueNode)
    {
        LoadedDialogueNode = dialogueNode;
        loadedDialogueIndex = 0;
    }

    public void DisplayNextNode(DialogueSubNode subNode, int index)
    {
        typewriter.NewLine(subNode.text);
        dialogueActive = true;
        loadedDialogueIndex = index;
        textDuration = subNode.dialogueDuration;
    }

    public void ClickDetected()
    {
        if (time < textDuration)
        {
            time = textDuration;
        }
        else if (!dialogueActive && !LoadedDialogueNode.subNodes[loadedDialogueIndex].endOfDialogue)
        {
            LoadedDialogueNode.NextDialogue();
        }
    }
    private void Update()
    {
        if (!dialogueActive) return;
        {
            time += Time.deltaTime;
            if (time >= textDuration)
            {
                if (LoadedDialogueNode.subNodes[loadedDialogueIndex].endOfDialogue)
                {
                    SetQuestions();
                    loadedDialogueIndex = 0;
                }
                dialogueActive = false;
            }
        }
    }

    private void SetQuestions()
    {
        for (int i = 0; i < LoadedDialogueNode.questionNodes.Length; i++)
        {
            answerChoices[i].SetActive(true);
            answerChoices[i].GetComponentInChildren<TextMeshProUGUI>().text = LoadedDialogueNode.ReturnTextFromNode(i);
        }
    }
}
