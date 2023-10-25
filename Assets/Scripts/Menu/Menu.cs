using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject instructions;

    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShowInstructions()
    {
        instructions.SetActive(!instructions.activeSelf);
    }
}
