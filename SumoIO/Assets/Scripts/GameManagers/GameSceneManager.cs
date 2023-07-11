using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    //Bu class sahne i�erisindeki UIlar� ve durdurma, devam etme ,
    //��kma gibi i�lemleri ger�ekle�tirmek i�in yaz�lm��t�r.
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject gameComplatePanel;
    [SerializeField] GameObject gameStartedText;
    [SerializeField] GameObject gamePausePanel;

    private void Start()
    {
        Time.timeScale = 1;
    }
    public void GamePause()
    {
        Time.timeScale = 0;
        gamePanel.SetActive(false);
        gamePausePanel.SetActive(true);
    }
    public void GameContiunePanel()
    {
        Time.timeScale = 1;
        gamePausePanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameComplatePanel()
    {
        gameComplatePanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
