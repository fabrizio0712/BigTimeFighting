using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private float _life;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _inmunityTimer;
    [SerializeField] private bool _fliped;
    [SerializeField] private GameObject _kickCollider;
    [SerializeField] private GameObject _punchCollider;
    [SerializeField] private GameObject _fighterHitbox;

    private float _maxLife = 100;
    private float _direction;
    private float _timer;
    private bool _jumping = false;
    private bool _hit = false;
    private bool _kickColliderActive = false;
    private bool _punchColliderActive = false;
    private bool _lose = false;
    private bool _win = false;

    public float Life { get => _life; set => _life = value; }
    public float JumpForce => _jumpForce;
    public float Speed => _speed;
    public float Direction { get => _direction; set => _direction = value; }
    public bool Jumping { get => _jumping; set => _jumping = value; }
    public bool Fliped { get => _fliped; set => _fliped = value; }
    public bool KickColliderActive { get => _kickColliderActive; set => _kickColliderActive = value; }
    public bool PunchColliderActive { get => _punchColliderActive; set => _punchColliderActive = value; }
    public bool Hit { get => _hit; set => _hit = value; }
    public GameObject FigterHitbox { get => _fighterHitbox; set => _fighterHitbox = value; }
    public bool Win { get => _win; set => _win = value; }
    public bool Lose { get => _lose; set => _lose = value; }
    public float MaxLife { get => _maxLife; }

    // Only AI Variables
    private bool _punchRequest;
    private bool _kickRequest;
    private bool _jumpRequest;

    public bool PunchRequest { get => _punchRequest; set => _punchRequest = value; }
    public bool KickRequest { get => _kickRequest; set => _kickRequest = value; }
    public bool JumpRequest { get => _jumpRequest; set => _jumpRequest = value; }
    

    private void Awake()
    {
        _life = _maxLife;
        _timer = 0;
        _kickCollider.SetActive(false);
        _punchCollider.SetActive(false);
    }
    private void Update()
    {
        _kickCollider.SetActive(_kickColliderActive);
        _punchCollider.SetActive(_punchColliderActive);
        if (!_fighterHitbox.active)
        {
            if (_timer >= _inmunityTimer)
            {
                _timer = 0;
                _fighterHitbox.SetActive(true);
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }
    }
    private void CheckColliders()
    {
        _kickCollider.SetActive(_kickColliderActive);
        _punchCollider.SetActive(_punchColliderActive);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 11)
        {
            other.gameObject.GetComponentInParent<Fighter>().Hit = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Environment")) _jumping = false;
    }
}
