using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : IState
{
    private readonly AudioManager _audioManager;
    private readonly GameObject _entity;
    private readonly Animator _anim;
    private readonly Rigidbody2D _rb;
    private readonly Fighter _fighter;
    private float _timer;

    public Win(GameObject entity)
    {
        _entity = entity;
        _rb = _entity.GetComponent<Rigidbody2D>();
        _anim = _entity.GetComponent<Animator>();
        _fighter = _entity.GetComponent<Fighter>();

    }

    public void OnEnter()
    {
        _anim.Play("win");
        _rb.velocity = Vector2.zero;
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }

}
