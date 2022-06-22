using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueActivator : MonoBehaviour
{
    public GameObject dialogueIndicator;
    private bool inCollider = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<DialogueController>().enabled = true;
            GetComponent<DialogueActivator>().enabled = false;
        }

        dialogueIndicator.SetActive(inCollider);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            inCollider = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            inCollider = false;
        }
    }
}
