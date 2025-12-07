using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerClickedBackground : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        DialogueManager.Current?.ClickDetected();
    }
}
