using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject journalPopup;
    public bool active = false;
    public bool journalActive = false;
    public bool dialogueActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (journalActive)
            {
                journalActive = false;
                journalPopup.SetActive(false);
            }
            else
            {
                if (!dialogueActive)
                {
                    active = !active;
                }
                
            }
        }

        menu.SetActive(active);
    }

}
