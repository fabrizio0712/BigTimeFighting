using System;
using UnityEngine;

public class AIConditionManager : IConditionManager
{
    private readonly Fighter _fighter;

    public AIConditionManager(Fighter fighter)
    {
        _fighter = fighter;
    }

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
        if (!_fighter.Jumping) return _fighter.JumpRequest;
        else return false;
    };
    public Func<bool> kick() => () =>
    {
        return _fighter.KickRequest;
    };
    public Func<bool> punch() => () =>
    {
        return _fighter.PunchRequest;
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
