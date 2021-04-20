using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    public float Level;
    private GameObject _spawnSpit;
    private GameObject _spit;
    private float _timer = 0;


    private Vector3 _spitForward;

    // Start is called before the first frame update
    void Start()
    {
        _spawnSpit = GameObject.FindGameObjectWithTag("SpitSpawn");
        Debug.Log(_spawnSpit.name);
        _moveSpeed += Random.Range(0.2f, 0.8f);
        _helth *= Level;
        _damage *= Level;
        _reloadAttackTime *= Level / 2;
        transform.localScale = Vector3.one * Random.Range(Level - 0.2f, Level + 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        ActionEnemy();
    }

    protected override void Move()
    {
        _distanceEnemyToPlayer = transform.position.x - _player.position.x;
        if (Mathf.Abs(_distanceEnemyToPlayer) > _rangeAttack)
        {
            _animator.Play(_stateAnimatorMove);
            _rb.velocity = new Vector2((Mathf.Abs(_distanceEnemyToPlayer) / _distanceEnemyToPlayer) * _moveSpeed * -1, _rb.velocity.y);
        }
        else if (Mathf.Abs(_distanceEnemyToPlayer) < _rangeAttack && Mathf.Abs(_distanceEnemyToPlayer) > _rangeAttack / 2)
        {
            _animator.Play(_stateAnimatorStay);
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
        else
        {
            _animator.Play(_stateAnimatorMove);
            _rb.velocity = new Vector2((Mathf.Abs(_distanceEnemyToPlayer) / _distanceEnemyToPlayer) * _moveSpeed, _rb.velocity.y);
        }
    }

    protected override void Attack()
    {
        _spit = _spawnSpit.GetComponent<SpitSpawn>().TakeSpit();
        Debug.Log(_spit);
        if (_spit != null && _timer > _reloadAttackTime)
        {
            _spit.transform.position = transform.position + Vector3.up * 0.7f;
            _spitForward = new Vector3(_spit.transform.position.x - _player.transform.position.x, _spit.transform.position.y - _player.transform.position.y, transform.position.z);
            _spitForward.Normalize();
            _spitForward *= _rangeAttack *-1;
            _spit.GetComponent<Rigidbody2D>().velocity = _spitForward;
            _spit.GetComponent<Spit>().Damage = _damage;
            _timer += 0;
        }
        _animator.Play(_stateAnimatorAttack);
    }
}
