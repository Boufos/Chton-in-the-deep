using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueWihtoutHead : ItemInteractObject
{
    override public void Interact()
    {
        base.Interact();
        if(IsActivated)
        {

            Level.Instance.CurrentTile.SetIsActive(true);
        }
    }
}
