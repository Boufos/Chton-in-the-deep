using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractObject : InteractableObject
{
    public List<AssetItem> Items;
    override public void Look()
    {
        _reactions.SetReaction(_reactions.LookingPhrase);

    }
    override public void Interact()
    {
        bool isInteracted = true;
        if (Items.Count > 0 && !IsActivated)
        {

            foreach (var item in Items)
            {
                if (_invetory.IsConaineItem(item))
                {
                    _invetory.RemoveItem(item);
                }
                else
                {
                    isInteracted = false;
                    break;
                }
            }

        }
        else
        {
            isInteracted = false;
        }
        if (isInteracted)
        {
            _reactions.SetReaction(_reactions.InteractionPhrase);
        }
        else
        {
            _reactions.SetReaction(_reactions.BeforeInteractionPhrase);

        }
        IsActivated = isInteracted;
    }

}
