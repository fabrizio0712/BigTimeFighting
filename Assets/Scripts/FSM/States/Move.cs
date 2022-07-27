using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    const string STATE_ANIMATION = "move";
    private readonly GameObject _entity;
    private readonly Rigidbody2D _rb;
    private readonly Fighter _fighter;
    private readonly Animator _anim;
    private float _direction;
    private readonly AILogicManager _aiLogic;
    private readonly bool _isPlayer = true;

    public Move(GameObject entity)
    {
        _entity = entity;
        _anim = _entity.GetComponent<Animator>();
        _rb = _entity.GetComponent<Rigidbody2D>();
        _fighter = _entity.GetComponent<Fighter>();
        if (_entity.CompareTag("Enemy"))
        {
            _aiLogic = _entity.GetComponent<AILogicManager>();
            _isPlayer = false;
        }
    }

    public void OnEnter()
    {
        _direction = _fighter.Direction;

    }

    public void Tick()
    {
        if (_direction != 0)
        {
            _rb.velocity = new Vector2(_fighter.Speed * _direction, _rb.velocity.y);
            _anim.Play(STATE_ANIMATION);
        }
        if (_fighter.Fliped)
        {
            _entity.gameObject.transform.localScale = new Vector3(-Mathf.Abs(_entity.gameObject.transform.localScale.x), _entity.gameObject.transform.localScale.y, _entity.gameObject.transform.localScale.z);
        }
        else
        {
            _entity.gameObject.transform.localScale = new Vector3(Mathf.Abs(_entity.gameObject.transform.localScale.x), _entity.gameObject.transform.localScale.y, _entity.gameObject.transform.localScale.z);
        }
        if (!_isPlayer) _aiLogic.MoveQuestion.Execute();
    }

    public void OnExit()
    {
        _anim.StopPlayback();
    }
}
