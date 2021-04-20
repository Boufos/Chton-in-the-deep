using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed = 0;
    [SerializeField] public float _helth = 100;
    [SerializeField] public float _maxHelth;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _rangeAttack;
    [SerializeField] protected float _reloadAttackTime;

    [SerializeField] protected string _stateAnimatorAttack;
    [SerializeField] protected string _stateAnimatorStay;
    [SerializeField] protected string _stateAnimatorMove;

    protected float _unicK; //Коеффициент разноса величин характеристик персонажа 
    protected float _distanceEnemyToPlayer;
    private float _timer = 0;
    private float _playerHelth = 100;

    [SerializeField] public Transform _player;

    [SerializeField] protected Rigidbody2D _rb;

    [SerializeField] protected Animator _animator;

    [SerializeField] private GameObject _enemySpawn;

    private void Start()
    {
        _maxHelth = _helth;
        _playerHelth = _player.GetComponent<Hero>().GetHelth();
    }   

    protected void ActionEnemy()
    {
        _animator.Play(_stateAnimatorStay);
        if(_playerHelth > 0)
        {
            Move();

            _timer += Time.deltaTime;
            if (Mathf.Abs(_distanceEnemyToPlayer) <= _rangeAttack && _timer > _reloadAttackTime)
            {
                _timer = 0;
                Attack();
            }
        }
    }

    protected virtual void Move()
    {
        _distanceEnemyToPlayer = transform.position.x - _player.position.x;
        if (Mathf.Abs(_distanceEnemyToPlayer) > _rangeAttack)
        {
            _animator.Play(_stateAnimatorMove);
            _rb.velocity = new Vector2((Mathf.Abs(_distanceEnemyToPlayer) / _distanceEnemyToPlayer) * _moveSpeed * -1, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
    }

    virtual protected void Attack()
    {
        _animator.Play(_stateAnimatorAttack);
        _player.gameObject.GetComponent<Hero>().GetDamage(_damage);
        _playerHelth = _player.GetComponent<Hero>().GetHelth();
    }

    public void GetDamage(float damage)
    {
        _helth -= damage;
        if (_helth < 0)
        {
            foreach (var elem in _enemySpawn.GetComponents<EnemySpawn>())
            {
                Debug.Log("--" + elem);
            }
            Destroy(gameObject);
            //_enemySpawn.GetComponent<EnemySpawn>().ReturnEnemyOnSpawn(gameObject.name);
        }
    }

}
