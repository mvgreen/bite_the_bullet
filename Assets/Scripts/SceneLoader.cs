using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour { 



    private void Start()
    {
        
    }

    public void LoadScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
