using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairAction : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D _rigidBody;
    private float _startMass;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _startMass = _rigidBody.mass;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Stair>() != null)
        {
            _rigidBody.mass = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Stair>() != null)
        {
            _rigidBody.mass = _startMass;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Stair>() != null)
        {
            transform.position += new Vector3(0, Input.GetAxis("Vertical"), 0) * Speed * Time.deltaTime;
        }

    }

}
