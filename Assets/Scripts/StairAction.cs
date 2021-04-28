using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StairAction : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D _rigidBody;
    private float _startGravityScale;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _startGravityScale = _rigidBody.gravityScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Stair>() != null)
        {
            _rigidBody.gravityScale = 0;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Stair>() != null)
        {

            _rigidBody.gravityScale = _startGravityScale;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Stair>() != null)
        {
            float inputVertical = Input.GetAxis("Vertical");
            _rigidBody.gravityScale = 0;
            transform.position += Vector3.up * inputVertical * Speed * Time.deltaTime;

        }

    }

}
