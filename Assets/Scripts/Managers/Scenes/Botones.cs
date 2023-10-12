using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    private AudioController audioController;

    #region Unity functions
    private void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
    }
    #endregion

    #region public functions
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string nombre)
    {
        Debug.Log(nombre);
        SceneManager.LoadScene(nombre);
        Time.timeScale = 1f;
        string[] nombres = nombre.Split();
        //solucion parche
        Debug.Log(nombre[0]);
        string nivel = nombres[0];
        for(int i=0; i<nombres.Length;i++){
             nivel = nivel + nombres[i];
        }
        //string nivel = nombres[0] + nombres[1] + nombres[2] + nombres[3] + nombres[4];
        if (nivel.Equals("Nivel"))
        {
            int numero = int.Parse(nombres[nombres.Length - 1]);
            numero = numero + 2;
            AudioType ubicar = (AudioType)numero;
            audioController.PlayAudio(ubicar);
        }
       

    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Menu principal");
        audioController.PlayAudio(AudioType.ST_00);
        Time.timeScale = 1f;
    }
    #endregion




}
