using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public string text;
    public GameObject journalPopup;
    public Text journalText;
    
    void OnCollisionEnter2D(Collision2D collider)
    {
        journalText.text = text;
        journalPopup.SetActive(true);
        Destroy(gameObject);
    }
}
