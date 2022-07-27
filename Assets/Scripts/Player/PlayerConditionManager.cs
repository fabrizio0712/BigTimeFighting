using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditionManager : IConditionManager
{
    private readonly Fighter _fighter;

    public PlayerConditionManager(Fighter fighter) => _fighter = fighter;

    public Func<bool> move() => () =>
    {
        return _fighter.Direction != 0;
    };
    public Func<bool> still() => () =>
    {
        return _fighter.Direction == 0;
    };
    public Func<bool> jump() => () =>
    {
        if (!_fighter.Jumping) return Input.GetKeyDown(KeyCode.Space);
        else return false;
    };
    public Func<bool> kick() => () =>
    {
        return Input.GetKeyDown(KeyCode.J);
    };
    public Func<bool> punch() => () =>
    {
        return Input.GetKeyDown(KeyCode.H);
    };
    public Func<bool> hitted() => () =>
    {
        return _fighter.Hit;
    };
    public Func<bool> falseReturn() => () =>
    {
        return false;
    };
    public Func<bool> trueReturn() => () =>
    {
        return true;
    };
    public Func<bool> grounded() => () =>
    {
        return !_fighter.Jumping;
    };
    public Func<bool> win() => () =>
    {
        return _fighter.Win;
    };
    public Func<bool> lose() => () =>
    {
        return _fighter.Lose;
    };
}
