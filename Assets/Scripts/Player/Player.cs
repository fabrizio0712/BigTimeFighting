using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fighter))]
public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;
    private PlayerConditionManager _conditions;
    private Fighter _fighter;
    private IState _idle;
    private IState _move;
    private IState _jump;
    private IState _hit;
    private IState _kick;
    private IState _punch;
    private IState _win;
    private IState _lose;

    private void Awake()
    {
        var _audioManager = FindObjectOfType<AudioManager>();



        _fighter = this.gameObject.GetComponent<Fighter>();
        _stateMachine = new StateMachine();
        _conditions = new PlayerConditionManager(_fighter);
        _idle = new Idle(gameObject);
        _move = new Move(gameObject);
        _jump = new Jump(gameObject);
        _hit = new Hit(gameObject, _audioManager);
        _kick = new Kick(gameObject, _audioManager);
        _punch = new Punch(gameObject, _audioManager);
        _win = new Win(gameObject);
        _lose = new Lose(gameObject);


        //idle Transitions
        At(_idle, _move, _conditions.move());
        At(_idle, _jump, _conditions.jump());
        At(_idle, _hit, _conditions.hitted());
        At(_idle, _kick, _conditions.kick());
        At(_idle, _punch, _conditions.punch());
        At(_idle, _win, _conditions.win());

        //Move transitions
        At(_move, _idle, _conditions.still());
        At(_move, _jump, _conditions.jump());
        At(_move, _hit, _conditions.hitted());
        At(_move, _kick, _conditions.kick());
        At(_move, _punch, _conditions.punch());
        At(_move, _win, _conditions.win());

        //Jump Transitions
        At(_jump, _idle, _conditions.grounded());
        At(_jump, _hit, _conditions.hitted());

        //kick Transitions
        At(_kick, _hit, _conditions.hitted());

        //punch Transitions
        At(_punch, _hit, _conditions.hitted());

        //hit Transitions
        At(_hit, _lose, _conditions.lose());

        //AddTransition alias
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        _stateMachine.SetState(_idle);
    }

    private void Update()
    {
        _fighter.Direction = Input.GetAxisRaw("Horizontal");
        _stateMachine.Tick();
    }

    public void ResetState()
    {
        _stateMachine.SetState(_idle);
    }
}