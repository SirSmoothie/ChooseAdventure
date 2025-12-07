using System;
using System.Collections;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Typewriter : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string line;
    public float textSpeed;
    private Coroutine _coroutine;

    private void Start()
    {
        text.text = "";
    }

    public void NewLineFull(string newLine)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        text.text = newLine;
    }
    public void NewLine(string newLine)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        if(line == newLine) return;
        text.text = string.Empty;
        line = string.Empty;
        line = newLine;
        _coroutine = StartCoroutine(typewriter());
    }
    IEnumerator typewriter()
    {
        foreach (char c in line.ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
