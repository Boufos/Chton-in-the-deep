using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    [SerializeField] Transform _pointSpawn;
    public float Damage;
    private GameObject[] _spitArray;
    private float _timer;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _timer = 0;
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if(_timer > 6)
        {
            ReturnSpit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            collision.GetComponent<Hero>().GetDamage(Damage);
            _rb.velocity = Vector2.zero;
            transform.position = _pointSpawn.position;
        }
        ReturnSpit();
    }

    private void ReturnSpit()
    {
        _spitArray = _pointSpawn.gameObject.GetComponent<SpitSpawn>()._spitArray;
        for (int i = 0; i < _spitArray.Length; i++)
        {
            if (gameObject.name == _spitArray[i].name)
            {
                _pointSpawn.gameObject.GetComponent<SpitSpawn>()._readySpit[i] = true;
            }
        }
    }
}
