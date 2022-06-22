using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public string text;
    public GameObject journalPopup;
    public Text journalText;
    
    void OnTriggerStay2D(Collider2D collider)
    {
        if (!GameObject.Find("MenuManager").GetComponent<MenuController>().active)
        {
            journalText.text = text;
            journalPopup.SetActive(true);
            GameObject.Find("MenuManager").GetComponent<MenuController>().journalActive = true;
            Destroy(gameObject);
        }
    }
}
