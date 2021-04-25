using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : InteractableObject
{
    [SerializeField]
    private List<InteractableObject> _activators;
    override public void Look()
    {
        if (!IsActivated)
        {
            _reactions.SetReaction(_reactions.LookingPhrase);
        }
    }
    override public void Interact()
    {
        bool isActive = true;
        for (int i = 0; i < _activators.Count; i++)
        {
            isActive = isActive && _activators[i].IsActivated;
        }
        if(!isActive)
        {
            _reactions.SetReaction(_reactions.BeforeInteractionPhrase);
        }
        else 
        {
            IsActivated = true;
            GetComponentInParent<Tile>().SetIsActive(true);
            _reactions.SetReaction(_reactions.InteractionPhrase);
            
        }
   
    }
}
