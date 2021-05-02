using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractObject : InteractableObject
{
    public List<AssetItem> Items;
    protected override bool isLookable => true;
    protected override bool isInteractable => true;
    override public void Interact()
    {
        bool isInteracted = true;
        if (Items.Count > 0 && !IsActivated)
        {

            foreach (var item in Items)
            {
                if (_invetory.IsContaineItem(item))
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
        _isActivated = isInteracted;
    }

}
