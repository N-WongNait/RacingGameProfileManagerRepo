using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmDelete : MonoBehaviour
{
    [SerializeField]private GameObject _deleteConfirmationCanvas;

    public void SwitchCanvas()
    {
        if (_deleteConfirmationCanvas.activeInHierarchy)
        {
            _deleteConfirmationCanvas.SetActive(false);
        }
        else
        {
            _deleteConfirmationCanvas.SetActive(true);
        }
    }
}
