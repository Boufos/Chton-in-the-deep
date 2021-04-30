using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : Character
{
    [SerializeField] private float _accelerate;
    [SerializeField] private float _stunTime;

    private float _timer;

    private void Awake()
    {
        _timer = _stunTime + 1;
    }

    private void FixedUpdate()
    {
        if (_timer > _stunTime)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Move(Input.GetAxis("Horizontal") * _accelerate);
                _rb.velocity = new Vector2(_rb.velocity.x, Input.GetAxis("Vertical") * _speed * _accelerate);
            }
            else
            {
                Move(Input.GetAxis("Horizontal"));
                _rb.velocity = new Vector2(_rb.velocity.x, Input.GetAxis("Vertical") * _speed);
            }
        }

        _timer += Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MovableWall")
        {
            _timer = 0;
        }
    }
}
