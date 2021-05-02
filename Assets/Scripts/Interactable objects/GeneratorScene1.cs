using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScene1 : ItemInteractObject
{
    private puzzle _puzzle;

    private void Awake()
    {
        _puzzle = gameObject.GetComponent<puzzle>();
    }

    public override void Interact()
    {
        base.Interact();

        if (isInteracted)
        {
            _puzzle.ActivatePuzzle();
        }
    }
}
