using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool gameIsPaused = false;
    private int numberOfEnemies;

    private void Awake()
    {
        Instance = this;
    }

    public void SetGamePauseState(bool state)
    {
        gameIsPaused = state;
    }

    public void SubtractEnemy()
    {
        numberOfEnemies--;
        UIEventsManager.instance.UpdateNumberOfEnemiesContainer(numberOfEnemies);
        if (numberOfEnemies == 0)
        {
            EventManager.Instance.OnGameWon.Raise();
            gameIsPaused = true;
            return;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateNumberOfEnemies(int newAddition)
    {
        numberOfEnemies += newAddition;
        UIEventsManager.instance.UpdateNumberOfEnemiesContainer(numberOfEnemies);
    }
}