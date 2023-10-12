using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{

    public Animator animator;
    public GameObject player;
    public UnityEvent intertactAction;
    public KeyCode interactKey;
    private bool isInRange;
    public GameObject target;  // Salida del ascensor
    private Vector2 targetPos; // Posición de la salida

    // Start is called before the first frame update
    private void Start()
    {
        targetPos = target.transform.position;
    }
    // Update is called once per frame
    void Update()
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

    public void EnterObject() //Entrar
    {

        if (Input.GetKeyDown(interactKey)) //Si apreta la tecla asignada y está fuera ddel objeto, entra al objeto
        {           
            player.transform.position = targetPos;
            FindObjectOfType<AudioController>().PlayAudio(AudioType.SFX_ELEVATOR);

        }
    }
}
