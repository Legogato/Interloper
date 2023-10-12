using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    private bool isInRange = false;
    public GameObject winScene;

    public void FixedUpdate()
    {
        if (isInRange)
        {
            StopGame();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision) //Verifica si el objeto con la tag player está en el rango del objeto
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
    public void StopGame()
    {
        winScene.SetActive(true);
        Time.timeScale = 0f;       
    }




}
