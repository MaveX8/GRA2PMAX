using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class MenuButton : MonoBehaviour
  
{    

    private Button BackToMenu;

    public UnityEvent OnStart;

    public void BackButton()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

}