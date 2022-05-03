using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    private bool active = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            active = !active;
        }

        menu.SetActive(active);
    }
}
