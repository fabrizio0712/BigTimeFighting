using System.Linq;
using UnityEngine;

public class Idle : IState
{
    private readonly GameObject _entity;
    private readonly Animator _anim;
    private readonly Rigidbody2D _rb;
    private readonly Fighter _fighter;
    private readonly AILogicManager _aiLogic;
    private readonly bool _isPlayer = true;

    public Idle(GameObject entity)
    {
        _entity = entity;
        _rb = _entity.GetComponent<Rigidbody2D>();
        _anim = _entity.GetComponent<Animator>();
        _fighter = _entity.GetComponent<Fighter>();
        if (_entity.CompareTag("Enemy")) 
        { 
            _aiLogic = _entity.GetComponent<AILogicManager>();
            _isPlayer = false;
        }
    }

    public void OnEnter()
    {
        _anim.Play("idle");
    }
    public void OnExit()
    {
        _anim.StopPlayback();
    }
    public void Tick()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        if (_fighter.Fliped)
        {
            _entity.gameObject.transform.localScale = new Vector3(-Mathf.Abs(_entity.gameObject.transform.localScale.x), _entity.gameObject.transform.localScale.y, _entity.gameObject.transform.localScale.z);
        }
        else 
        {
            _entity.gameObject.transform.localScale = new Vector3(Mathf.Abs(_entity.gameObject.transform.localScale.x), _entity.gameObject.transform.localScale.y, _entity.gameObject.transform.localScale.z);
        }
        if (!_isPlayer) _aiLogic.IdleQuestion.Execute();
    }

}