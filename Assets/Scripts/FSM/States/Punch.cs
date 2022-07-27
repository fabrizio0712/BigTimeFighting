using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : IState
{
    private AudioManager _audioManager;
    private readonly GameObject _entity;
    private readonly Animator _anim;
    private readonly Rigidbody2D _rb;
    private readonly Fighter _fighter;
    private float _timer;

    public Punch(GameObject entity, AudioManager audio)
    {
        _entity = entity;
        _rb = _entity.GetComponent<Rigidbody2D>();
        _anim = _entity.GetComponent<Animator>();
        _fighter = _entity.GetComponent<Fighter>();
        _audioManager = audio;
    }
    public void OnEnter()
    {
        _anim.Play("punch");
        _audioManager.Play("punch");
        _fighter.PunchRequest = false;
        _rb.velocity = Vector2.zero;
    }
    public void Tick()
    {
        if (_timer < _anim.GetCurrentAnimatorStateInfo(0).length)
        {
            if (_timer > 0.05f && _timer < 0.15f)
            {
                _fighter.PunchColliderActive = true;
            }
            else if (_timer >= 0.15f)
            {
                _fighter.PunchColliderActive = false;
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
        _fighter.PunchColliderActive = false;
        _timer = 0;
    }
}
