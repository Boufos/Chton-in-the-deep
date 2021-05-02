using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageObject : InteractableObject
{
    public List<AssetItem> Items;
    protected override bool isLookable => false;
    protected override bool isInteractable => true;
    public override void Interact()
    {
        if (Items.Count > 0)
        {
            for(int i = 0; i < Items.Count; i++)
            {
                _invetory.AddItem(Items[i]);
                Items.Remove(Items[i]);
            }
            _reactions.SetReaction(_reactions.InteractionPhrase);
            _isActivated = true;
        }
    }
}

