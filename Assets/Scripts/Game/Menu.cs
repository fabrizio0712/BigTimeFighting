using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject _main;
    [SerializeField] private GameObject _select;
    [SerializeField] private GameManager _gm;
    public void PlayButton()
    {
        _main.SetActive(false);
        _select.SetActive(true);
    }
    public void BackButton()
    {
        _main.SetActive(true);
        _select.SetActive(false);
    }
    public void QuitGame()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void SelectCharacter(RuntimeAnimatorController selected)
    {
        _gm.Player.GetComponent<Animator>().runtimeAnimatorController = selected;
        _gm.StartFight();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
