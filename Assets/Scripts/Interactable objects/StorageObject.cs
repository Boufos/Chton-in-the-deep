using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageObject : InteractableObject
{

    public override void Look()
    {
        if (Items.Count > 0)
        {
            _reactions.Reaction(_reactions.LookingPhraseBefore);
        }
        else
        {
            _reactions.Reaction(_reactions.LookingPhraseAfter);
        }
    }
    public override void Interact()
    {
        if (Items.Count > 0)
        {
            for(int i = 0; i < Items.Count; i++)
            {
                _invetory.AddItem(Items[i]);
                Items.Remove(Items[i]);
                _reactions.Reaction(_reactions.InteractionPhrase);
            }
        }
        if(Items.Count > 0)
        {
            _reactions.Reaction(_reactions.InteractionPhraseAfter);
        }
        else
        {
            _reactions.Reaction(_reactions.InteractionPhraseBefore);
        }
    }
}

