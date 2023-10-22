using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseContinue : MonoBehaviour
{
    [SerializeField] public GameObject PausePanel;
    // Start is called before the first frame update
    void Start()
    {
        PausePanel.SetActive(false);
         DontDestroyOnLoad(gameObject);
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale=0;
    }
    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale=1;
    }
    public void Quit_Game()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene("StartScene");    
    }
}
