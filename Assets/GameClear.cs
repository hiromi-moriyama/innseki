using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public float survivalTime = 30f;
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= survivalTime)
        {
            LoadGameClearScene();
        }
    }

    void LoadGameClearScene()
    {
        SceneManager.LoadScene("GameClear"); // Replace "GameClearSceneName" with the actual name of your game clear scene
    }
}
