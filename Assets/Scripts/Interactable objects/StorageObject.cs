using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageObject : InteractableObject
{
    public List<AssetItem> Items;
    public override void Look()
    {
        if (Items.Count > 0)
        {
            _reactions.SetReaction(_reactions.LookingPhrase);
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
            }
            _reactions.SetReaction(_reactions.InteractionPhrase);
        }
        
    }
}

