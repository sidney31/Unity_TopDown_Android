using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private GameObject dialog;
    [TextArea(3, 10)]
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;
    [SerializeField] private int index;
    private void Start()
    {
        dialog.SetActive(false);
    }
    public void StartDialog()
    {
        dialog.SetActive(true);
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialog.SetActive(false);
        }
    }
    public void ButtonNext()
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }
}
