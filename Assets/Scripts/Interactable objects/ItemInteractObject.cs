using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractObject : InteractableObject
{
    public List<AssetItem> Items;
    override public void Look()
    {
        _reactions.Reaction(_reactions.LookingPhrase);

    }
    override public void Interact()
    {
 
        if (!IsActive)
            _reactions.Reaction(_reactions.BeforeInteractionPhrase);
        if (Items.Count > 0)
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
        }

    }

}
