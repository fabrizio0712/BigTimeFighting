using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneAdmin : MonoBehaviour
{
    private GameManager _gm;
    private AudioManager _am;
    [SerializeField] private Slider _playerSlider;
    [SerializeField] private Slider _aiSlider;
    [SerializeField] private Text _youWin;
    [SerializeField] private Text _youLose;
    [SerializeField] private Button _backToMenu;

    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
        _am = FindObjectOfType<AudioManager>();

    }
    private void Start()
    {
        _gm.Player = Instantiate(_gm.Player, new Vector3(-5, 0, 0), Quaternion.identity);
        _gm.Player.tag = "Player";

        _gm.Ia = Instantiate(_gm.Ia, new Vector3(5, 0, 0), Quaternion.identity);
        _gm.Ia.GetComponent<AILogicManager>().Player = _gm.Player;
        _gm.Ia.tag = "Enemy";
    }
    void Update()
    {
        _playerSlider.value = _gm.Player.GetComponent<Fighter>().Life / _gm.Player.GetComponent<Fighter>().MaxLife;
        _aiSlider.value = _gm.Ia.GetComponent<AIFighter>().Fighter.Life / _gm.Ia.GetComponent<AIFighter>().Fighter.MaxLife;

        if (_gm.Player.GetComponent<Fighter>().Life <= 0)
        {
            _gm.Player.GetComponent<Fighter>().Lose = true;
            _youLose.gameObject.SetActive(true);
            _gm.Ia.GetComponent<AIFighter>().Fighter.Win = true;
            _backToMenu.gameObject.SetActive(true);
        }

        else if (_gm.Ia.GetComponent<AIFighter>().Fighter.Life <= 0)
        {
            _gm.Player.GetComponent<Fighter>().Win = true;
            _youWin.gameObject.SetActive(true);
            _gm.Ia.GetComponent<AIFighter>().Fighter.Lose = true;
            _backToMenu.gameObject.SetActive(true);
        }
    }

    public void Back()
    {
        _gm.OpenLevel("menu");
    }
}
