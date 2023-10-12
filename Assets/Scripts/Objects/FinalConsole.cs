using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class FinalConsole : MonoBehaviour
{
    public Animator animator;
    public bool isInRange;
    public UnityEvent intertactAction;
    public KeyCode interactKey;
    public GameObject luz1;
    public GameObject luz2;
    public PlayableDirector playableDirector;

    private void Update()
    {
        if (isInRange) //Si está en el rango o dentro del objeto es verdadero
        {
            if (Input.GetKeyDown(interactKey))
            {
                intertactAction.Invoke();     //Si apreta la tecla asignada en el unity events realizará la acción asignada
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision) //Verifica si el objeto con la tag player está en el rango del objeto
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            animator.SetBool("isInRange", isInRange); //Se cambia la animación del objeto a la misma imagen pero con un contorno verde
        }
    }

    public void OnTriggerExit2D(Collider2D collision)//Verifica si el objeto con la tag player se fue del rango del objeto
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            animator.SetBool("isInRange", isInRange);
        }
    }

    public void PlayCinematic()
    {
        playableDirector.Play();
    }
}
