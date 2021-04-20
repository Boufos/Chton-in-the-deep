using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatueHead : StorageObject
{
    private Coroutine _onDestroyBlock;
    public override void Look()
    {
        base.Look();
    }
    public override void Interact()
    {
        base.Interact();
        if (IsActive)
        {
            DisabledObject(gameObject);
        }
    }

    private void DisabledObject(GameObject gameObject)
    {
        if (_onDestroyBlock == null)
        {
            var animation = gameObject.GetComponent<Animation>();
            animation.Play();
            _onDestroyBlock = StartCoroutine(DestroyTarge(gameObject, animation.clip.length));
        }
    }
    private IEnumerator DestroyTarge(GameObject gameObject, float time)
    {
        int i = 0;
        do
        {
            i++;
            yield return new WaitForSeconds(time);
        } while (i < 2);
        _onDestroyBlock = null;
        Destroy(gameObject);
    }
}
