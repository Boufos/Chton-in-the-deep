using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Statue : Lever
{
    [SerializeField][TextArea]
    private string _LongReaction;

    override protected void EnableRectMenu()
    {
        base.EnableRectMenu();

    }
    override public void Interact()
    {
        OnActive?.Invoke();
    }


}
