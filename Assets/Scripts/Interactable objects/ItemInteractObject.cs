using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractObject : InteractableObject
{

    override public void Look()
    {
        if(!IsActive)
        {
            _reactions.Reaction(_reactions.LookingPhraseBefore);
        }
        else
        {
            _reactions.Reaction(_reactions.LookingPhraseAfter);
        }
    }
    override public void Interact()
    {
        bool isInteracted = true;
        foreach (var item in Items)
        {
            if (_invetory.IsConaineItem(item))
                _invetory.RemoveItem(item);
            else
            {
                isInteracted = false;
                _reactions.Reaction(_reactions.InteractionPhrase);
            }
        }
        IsActive = isInteracted;
        if(IsActive)
        {
            _reactions.Reaction(_reactions.InteractionPhraseAfter);
        }
        else
        {
            _reactions.Reaction(_reactions.InteractionPhraseBefore);
        }
    }

}
