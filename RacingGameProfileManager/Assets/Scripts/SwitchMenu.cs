using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMenu : MonoBehaviour
{
    [SerializeField]private Canvas _mainMenu, _profileMenu;
    void Start()
    {
        _mainMenu.enabled = true;
        _profileMenu.enabled = false;
    }

    public void PlayButton()
    {
        _mainMenu.enabled = false;
        _profileMenu.enabled = true;
    }
}
