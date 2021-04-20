using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : InteractableObject
{
    [SerializeField]
    private List<ItemInteractObject> _activators;
    override public void Look()
    {
        if (!IsActive)
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
        bool isActive = true;
        for (int i = 0; i < _activators.Count; i++)
        {
            isActive = isActive && _activators[i].IsActive;
        }
        if(isActive && !IsActive)
        {
            IsActive = true;

            GetComponentInParent<Tile>().SetIsActive(true);
            _reactions.Reaction(_reactions.InteractionPhrase);
        }

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
