using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Method to load a specific level
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // Method to load the next level based on the build index
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // Method to restart the current level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
