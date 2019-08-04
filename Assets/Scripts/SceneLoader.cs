using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string LevelName;

    public void LoadScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LevelName == "Exit")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(LevelName);
        }
    }
}
