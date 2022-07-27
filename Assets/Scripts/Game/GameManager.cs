using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _ia;
    [SerializeField] private Scene _currentScene = Scene.menu;
    private float difference;
    private AudioManager _am;

    public GameObject Ia { get => _ia; set => _ia = value; }
    public GameObject Player { get => _player; set => _player = value; }
    private enum Scene { menu, fight }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null) Destroy(this.gameObject);

        _am = FindObjectOfType<AudioManager>();
    }
    void Start()
    {
        if (_currentScene == Scene.menu) _am.Play("theme");
    }
    private void Update()
    {
        if (_currentScene == Scene.fight)
        {
            difference = _ia.gameObject.transform.position.x - _player.gameObject.transform.position.x;
            if (difference > 0)
            {
                _player.GetComponent<Fighter>().Fliped = false;
                _ia.GetComponent<Fighter>().Fliped = true;
            }
            else
            {
                _player.GetComponent<Fighter>().Fliped = true;
                _ia.GetComponent<Fighter>().Fliped = false;
            }
        }
    }
    public void StartFight()
    {
        Debug.Log("state Change");
        _currentScene = Scene.fight;
    }

    public void OpenLevel(string level)
    {
        _currentScene = Scene.menu;
        SceneManager.LoadScene(level);
    }
}
