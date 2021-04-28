using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatueHead : StorageObject
{
    public override void Interact()
    {
        base.Interact();
        if (_isActivated)
        {
            DisabledObject(gameObject);
        }
    }

    private void DisabledObject(GameObject gameObject)
    {
        //    var animation = gameObject.GetComponent<Animation>();
        //    animation.Play();
        Destroy(gameObject);//, animation.clip.length);
    }
}
