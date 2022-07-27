using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : IState
{
    private readonly AudioManager _audioManager;

    private readonly GameObject _entity;
    private readonly Animator _anim;
    private readonly Rigidbody2D _rb;
    private readonly Fighter _fighter;
    private float _timer;

    public Kick(GameObject entity, AudioManager audio)
    {
        _entity = entity;
        _rb = _entity.GetComponent<Rigidbody2D>();
        _anim = _entity.GetComponent<Animator>();
        _fighter = _entity.GetComponent<Fighter>();
        _audioManager = audio;
    }
    public void OnEnter()
    {
        _anim.Play("kick");
        _audioManager.Play("kick");
        _fighter.KickRequest = false;
        _rb.velocity = Vector2.zero;
    }
    public void Tick()
    {
        if (_timer < _anim.GetCurrentAnimatorStateInfo(0).length)
        {
            if (_timer > 0.16f && _timer < 0.24f)
            {
                _fighter.KickColliderActive = true;
            }
            else if (_timer >= 0.24f)
            {
                _fighter.KickColliderActive = false;
            }
            _timer += Time.deltaTime;
        }
        else
        {
            if (_entity.CompareTag("Player"))
            {
                _entity.GetComponent<Player>().ResetState();
            }
            else if (_entity.CompareTag("Enemy"))
            {
                _entity.GetComponent<AILogicManager>().ResetState();
            }
        }
    }

    public void OnExit()
    {
        _timer = 0;
        _fighter.KickColliderActive = false;
    }

}
