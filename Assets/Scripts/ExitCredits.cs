﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCredits : MonoBehaviour
{
    float timer = 3;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<0)
        {
            
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
