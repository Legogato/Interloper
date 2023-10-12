using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    

    public void ReturnMenu(string nombre)
    {
        SceneManager.LoadScene(nombre);
        Time.timeScale = 1f;
    }
    public void RestartLevel(string nombre)
    {
        SceneManager.LoadScene(nombre);
        Time.timeScale = 1f;
    }
   

}
