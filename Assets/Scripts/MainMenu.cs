using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        // Debug.Log($"High Score {highScore}");
    }
    public void Play()
    {
        // PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
}
