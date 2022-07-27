using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IState
{
    private readonly GameObject _entity;
    private readonly Animator _anim;
    private readonly Rigidbody2D _rb;
    private readonly Fighter _fighter;
    private float _timer;

    public Jump(GameObject entity)
    {
        _entity = entity;
        _anim = _entity.GetComponent<Animator>();
        _rb = _entity.GetComponent<Rigidbody2D>();
        _fighter = _entity.GetComponent<Fighter>();
    }

    public void OnEnter()
    {
        _rb.velocity = new Vector2(_rb.velocity.x * 1.5f, _rb.velocity.y);
        _rb.AddForce(new Vector2(0, _fighter.JumpForce), ForceMode2D.Impulse);
        _fighter.JumpRequest = false;
        _fighter.Jumping = true;
        _fighter.FigterHitbox.SetActive(false);
        _anim.Play("jump");
    }

    public void OnExit()
    {
        _timer = 0;
    }

    public void Tick()
    {
        if (_timer < _anim.GetCurrentAnimatorStateInfo(0).length / 2)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _fighter.FigterHitbox.SetActive(true);
        }
    }
}
