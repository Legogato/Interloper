using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TareaCables : MonoBehaviour
{
    public int conexionesActuales;
    public GameObject puerta;
    public GameObject taskMenu;
    public UnityEvent action;




    public void ComprobarVictoria()
    {
        if (conexionesActuales == 4)
        {

            Time.timeScale = 1f;
            taskMenu.SetActive(false);
            AbrirPuerta();
            action.Invoke();
        }
    }
    public void AbrirPuerta()
    {

        puerta.SetActive(false);
        FindObjectOfType<AudioController>().PlayAudio(AudioType.SFX_SWITCH);
    

    }
}
