using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSound : MonoBehaviour
{
    

    public void OnTriggerEnter2D(Collider2D collision) //Verifica si el objeto con la tag player está en el rango del objeto
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            FindObjectOfType<AudioController>().PlayAudio(AudioType.SFX_BIGLASER);

        }
    }

}

