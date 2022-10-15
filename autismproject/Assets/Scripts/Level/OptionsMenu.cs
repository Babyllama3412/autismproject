using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    bool toggle;
    void Update()
    {
        optionsMenu.SetActive(toggle);
    }

    public void ToggleMenu()
    {
        toggle = !toggle;
    }
}
