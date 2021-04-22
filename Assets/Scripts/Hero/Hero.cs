using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    enum StateAnimation
    {
        interraction,
        stay,
        move,
    }

    [SerializeField] private LayerMask _ground;
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _groundCheckPosition;

    [SerializeField] private float _radiusGroundCheck;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _speedJumpUp;
    [SerializeField] private float _speedFall;

    [SerializeField] private string _nameAnimationInteraction;
    [SerializeField] private string _nameAnimationStay;
    [SerializeField] private string _nameAnimationMove;


    private StateAnimation _stateAnimation = StateAnimation.stay;

    private float _direction;
    private float _currentJumpTime = 0;

    private bool _canJump = false;
    
    void Update()
    {
        _direction = Input.GetAxis("Horizontal");
        Move(_direction);
        if(_direction != 0)
        {
            _stateAnimation = StateAnimation.move;
        }
        else
        {
            _stateAnimation = StateAnimation.stay;
        }

        //Добавить условие для вызова Interaction()

        #region Jump
        if (Physics2D.OverlapCircle(_groundCheckPosition.position,_radiusGroundCheck, _ground))
        {
            _canJump = true;
        }
        else
        {
            _canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            _currentJumpTime = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if(_rb.velocity.y >= 0)
            _rb.velocity = new Vector2(_rb.velocity.x, -1 * (Mathf.Pow(_currentJumpTime, 2) * _speedJumpUp) + _jumpHeight);
        }

        if(_rb.velocity.y < 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * _speedFall);
        }

        _currentJumpTime += Time.deltaTime;
        #endregion

        PlayAnimation();
    }

    private void Interaction()
    {
        _stateAnimation = StateAnimation.interraction;
    }
    
    private void PlayAnimation()
    {
        switch (_stateAnimation)
        {
            case StateAnimation.interraction:
                _anim.Play(_nameAnimationInteraction);
                break;
            case StateAnimation.stay:
                _anim.Play(_nameAnimationStay);
                break;
            case StateAnimation.move:
                _anim.Play(_nameAnimationMove);
                break;
            default:
                break;
        }
    }

}
