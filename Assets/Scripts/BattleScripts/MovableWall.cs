using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableWall : MonoBehaviour
{
    [SerializeField] private string _nameAnimationCrash;

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            _anim.Play(_nameAnimationCrash);
        }
    }
}
