using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Lever : InteractableObject
{
    [SerializeField]
    protected UnityEvent OnActive;
    [SerializeField]
    protected List<GameObject> _targets;

    private Coroutine _onDestroyBlock;
    public override void Look()
    {
        base.Look();
    }
    public override void Interact()
    {
        base.Interact();
        IsActivated = true;
        if(IsActivated)
        {
            OnActive?.Invoke();
        }
    }
    public void EnabledObjects()
    {
        foreach(var target in _targets)
        {
            if (target != null)
            {   

                EnabledObject(target);
            }
        }
        _targets = new List<GameObject>();
    }
    public void DesabledObjects()
    {
        foreach (var target in _targets)
        {
            if(target != null && target.activeSelf)
                DisabledObject(target);
        }
        _targets = new List<GameObject>();
    }
    private void EnabledObject(GameObject gameObject)
    {
       

        var animation = gameObject.GetComponent<Animation>();
        if (!animation.isPlaying)
        {
            gameObject.SetActive(true);
            animation.Play();
        }    
            


    }
    private void DisabledObject(GameObject gameObject)
    {
        var animation = gameObject.GetComponent<Animation>();
        if (!animation.isPlaying)
        {
          
            animation.Play();
            Destroy(gameObject, animation.clip.length);
        }
    }
}
