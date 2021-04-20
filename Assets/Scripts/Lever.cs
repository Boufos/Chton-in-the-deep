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
        IsActive = !IsActive;
        if(IsActive)
        {
            OnActive?.Invoke();
        }
    }
    public void EnabledObjects()
    {
        foreach(var target in _targets)
        {
            EnabledObject(target);
        }
    }
    public void DesabledObjects()
    {
        foreach (var target in _targets)
        {
            DisabledObject(target);
        }
    }
    private void EnabledObject(GameObject gameObject)
    {
        gameObject.SetActive(true);

        var animation = gameObject.GetComponent<Animation>();

        animation.Play(); 


    }
    private void DisabledObject(GameObject gameObject)
    {

        if(_onDestroyBlock == null)
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
