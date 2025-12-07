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

    public void NewLine(string newLine)
    {
        if(line == newLine) return;
        StopCoroutine(typewriter());
        text.text = string.Empty;
        line = string.Empty;
        line = newLine;
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
