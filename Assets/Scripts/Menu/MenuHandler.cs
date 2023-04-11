using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
   public void StartHandler()
    {
        SceneManager.LoadScene(1);
    }
   public void SettingsHandler()
    {

    }
   public void ExitHandler()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
