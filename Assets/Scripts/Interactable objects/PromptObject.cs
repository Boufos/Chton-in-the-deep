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
        if (!_targetObject.IsActivated)
            _reactions.SetReaction(_reactions.InteractionPhrase);
    }

}
