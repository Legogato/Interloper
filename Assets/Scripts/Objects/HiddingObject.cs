using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HiddingObject : MonoBehaviour
{
    private bool isOpen = false;
    public Animator animator;
    private bool isInside;
    public GameObject player;
    public UnityEvent intertactAction;
    public KeyCode interactKey;
    private bool isInRange;
    public Transform salida;
    

    //INTERACTOR

    public void Update()
    {

        if (isInRange || isInside) //Si está en el rango o dentro del objeto es verdadero
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
    
    public void EnterObject() //Entrar y salir del objeto
    {
        
        if (Input.GetKeyDown(interactKey) && isInside == false) //Si apreta la tecla asignada y está fuera ddel objeto, entra al objeto
        {
            isOpen = true;
            Debug.Log("You are now inside");
            animator.SetBool("isInside", isOpen);   //se cambia a la animacion dentro del objeto
            isInside = true;
            player.SetActive(false);                // El objeto player se desactiva
            FindObjectOfType<AudioController>().PlayAudio(AudioType.SFX_GARBAGE); //Se reproduce un efecto de sonido
                            
        }
        else if (Input.GetKeyDown(KeyCode.E) && isInside == true) // si apreta la tecla asignada y está dentro del objeto, sale del objeto
        {
            isOpen = false;
            Debug.Log("You are now outside");
            animator.SetBool("isInside", isOpen);
            isInside = false;
            FindObjectOfType<AudioController>().PlayAudio(AudioType.SFX_GARBAGE); //Se reproduce un efecto de sonido
            player.SetActive(true);      // El objeto player se activa
            player.transform.position = salida.position;



        }
    }
    


}

