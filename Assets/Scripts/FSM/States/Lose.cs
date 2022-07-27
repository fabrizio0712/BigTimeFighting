using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : IState
{
    private readonly AudioManager _audioManager;
    private readonly GameObject _entity;
    private readonly Animator _anim;
    private readonly Rigidbody2D _rb;
    private readonly Fighter _fighter;
    private float _timer;

    public Lose(GameObject entity)
    {
        _entity = entity;
        _rb = _entity.GetComponent<Rigidbody2D>();
        _anim = _entity.GetComponent<Animator>();
        _fighter = _entity.GetComponent<Fighter>();
    
    }

    public void OnEnter()
    {
        _anim.Play("lose");
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        if (Mathf.Abs(_rb.velocity.x) > 0.25)
        {
            _rb.velocity = new Vector2(_rb.velocity.x - _rb.velocity.x / 150, _rb.velocity.y);
        }
        else _rb.velocity = Vector2.zero;
    }

}
