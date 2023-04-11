using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private AudioSource _pauseClickSound;
    [SerializeField] private AudioSource _pauseSound;
    public void PauseButton()
    {
        _pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        _pauseClickSound.Play();
        _pauseSound.Play();
    }
    public void ContinueButton()
    {
        _pauseSound.Stop();
        _pauseClickSound.Play();
        _pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void RestartButton()
    {
        _pauseSound.Stop();
        _pauseClickSound.Play();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    public void SettingsButton()
    {
        _pauseSound.Stop();
        _pauseClickSound.Play();
    }
    public void ExitButton()
    {
        _pauseSound.Stop();
        _pauseClickSound.Play();
        SceneManager.LoadScene(0);
    }
}
