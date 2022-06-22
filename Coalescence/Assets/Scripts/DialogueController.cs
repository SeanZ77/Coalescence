using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public Dialogue[] lines;
    public float delay;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Image dialogueSprite;
    private int lineIndex;
    private bool canContinue;

    void OnEnable()
    {
        lineIndex = 0;
        canContinue = true;
        dialoguePanel.SetActive(true);
        StartCoroutine(drawText());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (lineIndex + 1 < lines.Length)
            {
                if (canContinue)
                {
                    lineIndex++;
                    StartCoroutine(drawText());
                }
            }
            else
            {
                dialoguePanel.SetActive(false);
                GetComponent<DialogueActivator>().enabled = true;
                GetComponent<DialogueController>().enabled = false;
            }
        }
    }

    IEnumerator drawText()
    {
        canContinue = false;
        dialogueText.text = "";
        dialogueSprite.sprite = lines[lineIndex].sprite;
        for (int i = 0; i < lines[lineIndex].text.Length; i++)
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += lines[lineIndex].text[i];
        }
        canContinue = true;
    }

}
