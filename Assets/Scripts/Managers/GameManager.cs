using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool _isGameRunning;
    
    private void OnEnable() {
        Enemy.onPlayerKill += GameOver;
    }

    private void OnDisable() {
        Enemy.onPlayerKill -= GameOver;
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
