using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Button quitButton;
    private Button CreditsButton;


    public UnityEvent OnStart;

    private void Awake()
    {
        quitButton.onClick.AddListener(Quit);
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    public void Credits()
    {
        SceneManager.LoadSceneAsync("Credits");
    }

    public void SetScore(Vector3 pos)
    {

    }

    private void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}