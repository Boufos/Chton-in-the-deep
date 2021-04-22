using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float _speed;
    //[SerializeField] private float health;

    protected Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected void Move(float direction = 0)
    {
        _rb.velocity = new Vector2(direction*_speed, _rb.velocity.y);
    }
}
