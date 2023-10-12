using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenDoorTerminal : MonoBehaviour
{
    public Animator animator;
    public bool isActivated;
    public GameObject player;
    public GameObject puerta;
    public GameObject taskMenu;

    public UnityEvent intertactAction;
    public KeyCode interactKey;
    public bool isInRange;

    public new Transform camera;
    public Transform task;

    private bool tareaActive;
    //INTERACTOR

    public void Update()
    {

        if (isInRange) //Si est� en el rango o dentro del objeto es verdadero
        {
            if (Input.GetKeyDown(interactKey))
            {         
                intertactAction.Invoke();     //Si apreta la tecla asignada en el unity events realizar� la acci�n asignada
                taskMenu.SetActive(true);
            }
        }
        if (tareaActive)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                tareaActive = false;
                Time.timeScale = 1f;
                taskMenu.SetActive(false);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) //Verifica si el objeto con la tag player est� en el rango del objeto
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            animator.SetBool("isInRange", isInRange); //Se cambia la animaci�n del objeto a la misma imagen pero con un contorno verde
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

    public void ActivarTerminal()
    {
        if (Input.GetKeyDown(interactKey) && !isActivated){
            
            isActivated = true;
            AbrirPuerta();
        }
    }
    public void AbrirPuerta()
    {
        if (isActivated)
        {
            puerta.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Switch");
        }
    }
    public void AbrirTarea()
    {
        Time.timeScale = 0f;
        camera.position = task.position;
        tareaActive = true;
    }
}
