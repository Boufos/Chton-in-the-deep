using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatueHead : StorageObject
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject BattleWindow;

    public override void Interact()
    {
        base.Interact();
        if (_isActivated)
        {
            DisabledObject(gameObject);
        }
    }

    protected override void OnMouseDown()
    {
        Player.SetActive(false);
        BattleWindow.SetActive(true);
        base.OnMouseDown();
    }

    private void DisabledObject(GameObject gameObject)
    {
        //    var animation = gameObject.GetComponent<Animation>();
        //    animation.Play();
        Destroy(gameObject);//, animation.clip.length);
    }
}
