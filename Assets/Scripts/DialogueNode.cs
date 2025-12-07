using UnityEngine;

[System.Serializable]
public class DialogueSubNode
{
    public string text;
    public bool endOfDialogue = false;
    public float dialogueDuration = 20f;
}
[System.Serializable]
public class QuestionNode
{
    public string text;
    public DialogueNode NextDialogue;
}
public class DialogueNode : MonoBehaviour
{
    public DialogueSubNode[] subNodes;
    public QuestionNode[] questionNodes;
    public DialogueNode nextDialogueNodeAtEndOfDialogue;
    private int _index = 0;


    public void StartNodeFromStart()
    {
        _index = 0;
        DialogueManager.Current?.SetLoadedDialogue(this);
        StartDialogue();
    }

    private void StartDialogue()
    {
        DialogueManager.Current.DisplayNextNode(subNodes[_index], _index);
    }

    public string ReturnTextFromNode(int index)
    {
        return questionNodes[index].text;
    }

    public void DisplayFullText()
    {
        DialogueManager.Current.DisplayCurrentNodeFull(subNodes[_index]);
    }
    public void NextDialogue()
    {
        if (!subNodes[_index].endOfDialogue)
        {
            _index++;
            StartDialogue();
        }
        else
        {
            if (questionNodes.Length == 0)
            {
                DialogueManager.Current.StartDialogue(nextDialogueNodeAtEndOfDialogue);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        if (!DialogueManager.Current.drawDialogueTree) return;
            for (int q = 0; q < questionNodes.Length; q++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position,
                    questionNodes[q].NextDialogue.transform.position);

            }
    }
}
