using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptObject : InteractableObject
{
    [SerializeField]
    private InteractableObject _targetObject;

    protected override bool isLookable => true;

    protected override bool isInteractable => false;
    override public void Look()
    {
            _reactions.SetReaction(_reactions.LookingPhrase);
    }
    override protected void OnMouseDown()
    {
        if(!_targetObject.IsActivated)
        {
            base.OnMouseDown();
        }
    }

}
