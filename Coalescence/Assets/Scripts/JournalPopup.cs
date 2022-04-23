using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalPopup : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            gameObject.SetActive(false);
        }
    }
}
