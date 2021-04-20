using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractoinButton : MonoBehaviour
{
    private RectMenu _menu;
    private void Start()
    {
        _menu = GetComponentInParent<RectMenu>();

    }
    private void OnMouseDown()
    {
        _menu.Parent.Interact();
    }
}
