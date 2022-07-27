using System.Collections.Generic;
using UnityEngine;

public class AILogicManager : MonoBehaviour
{
    //Attacks && actions 
    private Dictionary<string, float> _attacks;
    private Dictionary<string, float> _idleActions;
    private Dictionary<string, float> _moveActions;

    //Private References
    private GameObject _player;
    private AIFighter _aiFighter;
    private float _resetDecision = 0.25f;
    private float _decisionTimer = 0;
    [SerializeField] private float _minAttackDistance;

    //STATES
    private StateMachine _stateMachine;
    private IState _idle;
    private IState _move;

    //Question Nodes
    private QuestionNode _idleQuestion;
    private QuestionNode _moveQuestion;

    //ActionNodes
    private ActionNode _attackActionNode;
    private ActionNode _punchActionNode;
    private ActionNode _kickActionNode;
    private ActionNode _moveActionNode;

    //Public References
    public GameObject Player { set => _player = value; }
    public QuestionNode IdleQuestion { get => _idleQuestion; }
    public QuestionNode MoveQuestion { get => _moveQuestion; }

    void Start()
    {
        _aiFighter = this.gameObject.GetComponent<AIFighter>();
        _stateMachine = _aiFighter.StateMachine;
        _idle = _aiFighter.IdleState;
        _move = _aiFighter.MoveState;

        //Nodes
        _attackActionNode = new ActionNode(Attack);
        _punchActionNode = new ActionNode(Punch);
        _kickActionNode = new ActionNode(Kick);
        _moveActionNode = new ActionNode(Move);

        _idleQuestion = new QuestionNode(IsInRange, _attackActionNode, _moveActionNode);
        _moveQuestion = new QuestionNode(IsInRange, _attackActionNode, _moveActionNode);

        //Dictionary 
        _attacks = new Dictionary<string, float>();
        _idleActions = new Dictionary<string, float>();
        _moveActions = new Dictionary<string, float>();

        _attacks.Add("punch", 20);
        _attacks.Add("kick", 10);
        _attacks.Add("back", 15);

        _idleActions.Add("forward", 100);
        _idleActions.Add("backward", 70);
        _idleActions.Add("jump", 10);

        _moveActions.Add("stop", 50);
        _moveActions.Add("continue", 70);
        _moveActions.Add("jump", 15);
    }
    bool IsInRange()
    {
        Vector2 _difference = _player.gameObject.transform.position - transform.position;
        _difference.y = 0;
        float _distance = _difference.magnitude;
        if (_distance > _minAttackDistance) return false;
        else return true;
    }
    void Attack()
    {
        if (!_aiFighter.Fighter.Jumping)
        {
            string attack = Roulette(_attacks);
            switch (attack)
            {
                case "punch":
                    _punchActionNode.Execute();
                    break;
                case "kick":
                    _kickActionNode.Execute();
                    break;
                case "back":
                    if (_aiFighter.Fighter.Fliped) _aiFighter.Fighter.Direction = 1;
                    else _aiFighter.Fighter.Direction = -1;
                    break;
            }
        }
    }
    void Punch()
    {
        _aiFighter.Fighter.PunchRequest = true;
    }
    void Kick()
    {
        _aiFighter.Fighter.KickRequest = true;
    }
    void Move()
    {
        Debug.Log(_stateMachine.CurrentState);
        if (_stateMachine.CurrentState == _idle)
        {
            string decision = Roulette(_idleActions);
            switch (decision)
            {
                case "forward":
                    if (_aiFighter.Fighter.Fliped) _aiFighter.Fighter.Direction = -1;
                    else _aiFighter.Fighter.Direction = 1;
                    break;
                case "backward":
                    if (_aiFighter.Fighter.Fliped) _aiFighter.Fighter.Direction = 1;
                    else _aiFighter.Fighter.Direction = -1;
                    break;
                case "jump":
                    _aiFighter.Fighter.JumpRequest = true;
                    break;
            }
        }
        else if (_stateMachine.CurrentState == _move)
        {
            if (_decisionTimer > _resetDecision)
            {
                string decision = Roulette(_moveActions);
                switch (decision)
                {
                    case "stop":
                        _aiFighter.Fighter.Direction = 0;
                        break;
                    case "continue":
                        break;
                    case "jump":
                        _aiFighter.Fighter.JumpRequest = true;
                        break;
                }
                _decisionTimer = 0;
            }
            else
            {
                _decisionTimer += Time.deltaTime;
            }
        }
    }
    string Roulette(Dictionary<string, float> dic)
    {
        float _totalProbabilities = 0;
        foreach (var option in dic)
        {
            _totalProbabilities += option.Value;
        }
        float random = UnityEngine.Random.Range(0, _totalProbabilities);
        foreach (var option in dic)
        {
            random -= option.Value;
            if (random <= 0)
            {
                return option.Key;
            }
        }
        return null;
    }

    public void ResetState()
    {
        _stateMachine.SetState(_idle);
    }
}
