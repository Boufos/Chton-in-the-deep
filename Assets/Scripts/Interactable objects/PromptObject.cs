using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptObject : InteractableObject
{
    [SerializeField]
    private InteractableObject _targetObject;
    protected override void EnableRectMenu()
    {
        if (_targetObject.IsActive)
        {
            Destroy(this);

        }
        else
        {
            _menu = Instantiate(MenuPrefab, transform.position, MenuPrefab.transform.rotation);
            _menu.Parent = this;
            Collider.enabled = false;
             
        }
    }
    override public void Look()
    {
        _reactions.Reaction(_reactions.InteractionPhrase);
    }
    override public void Interact()
    {
    }
}
