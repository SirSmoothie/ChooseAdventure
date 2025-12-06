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

    private void Start()
    {
        text.text = string.Empty;
        StartTypeWriter();
    }

    void StartTypeWriter()
    {
        StartCoroutine(typewriter());
    }

    public void NewLine(string newLine)
    {
        if(line == newLine) return;
        StopCoroutine(typewriter());
        line = newLine;
        text.text = String.Empty;
        StartCoroutine(typewriter());
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
