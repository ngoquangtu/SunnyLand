using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnClick_StartGame()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void OnClick_QuitGame()
    {
        // #if UNITY_EDITOR
        // UnityEditor.EditorApplication.isPlaying = false;
        // Debug.Log("Quitting");
        // #else
        Application.Quit();
        // #endif
    }
    public void OnClick_Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
