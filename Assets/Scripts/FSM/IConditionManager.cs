using System;

public interface IConditionManager
{
    Func<bool> trueReturn();
    Func<bool> falseReturn();
}