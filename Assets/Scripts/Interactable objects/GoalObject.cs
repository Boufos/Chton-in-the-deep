using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : InteractableObject
{
    [SerializeField]
    private List<InteractableObject> _activators;
    override public void Look()
    {
        if (!IsActive)
        {
            _reactions.Reaction(_reactions.LookingPhrase);
        }
    }
    override public void Interact()
    {
        bool isActive = true;
        for (int i = 0; i < _activators.Count; i++)
        {
            isActive = isActive && _activators[i].IsActive;
        }
        if(!isActive)
        {
            _reactions.Reaction(_reactions.BeforeInteractionPhrase);
        }
        else 
        {
            IsActive = true;
            GetComponentInParent<Tile>().SetIsActive(true);
            _reactions.Reaction(_reactions.InteractionPhrase);
            
        }
   
    }
}
