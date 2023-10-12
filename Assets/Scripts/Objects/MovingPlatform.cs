using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 target;
    private Vector3 startPosition;
    [Range(0f,10f)]
    public float movespeed;
    private bool moving=false;
    private bool isInRange;
    public UnityEvent intertactAction;
    public KeyCode interactKey;

    private void Start()
    {
        startPosition = transform.position;
    }
    // Update is called once per frame
    private void Update()
    {
        if(isInRange && !moving) //Si está en el rango y no se mueve
        {
            if (Input.GetKeyDown(interactKey))
            {
                intertactAction.Invoke();     //Si apreta la tecla asignada en el unity events realizará la acción asignada
            }
        }
    }
    void FixedUpdate()
    {
        if (moving)
        {
            transform.position = new Vector3((target+startPosition).x*movespeed*Time.deltaTime,(target + startPosition).y * movespeed * Time.deltaTime,0);
            if (transform.position == target)
            {
                moving = false;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision) //Verifica si el objeto con la tag player está en el rango del objeto
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            collision.gameObject.GetComponent<Collider2D>().transform.SetParent(transform);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)//Verifica si el objeto con la tag player se fue del rango del objeto
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            collision.gameObject.GetComponent<Collider2D>().transform.SetParent(null);

        }
    }
    public void activate()
    {
        if(Input.GetKeyDown(interactKey) && isInRange && !moving)
        {
            moving = true;
        }
    }
}
