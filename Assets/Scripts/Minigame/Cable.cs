using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public SpriteRenderer cabeza;
    private Vector2 posicionOriginal;
    private Vector2 tamanoOriginal;
    public GameObject luz;
    private TareaCables tareaCables;
    private Cable cable;

    void Start()
    {
        posicionOriginal = transform.position;
        tamanoOriginal = cabeza.size;
        tareaCables = transform.root.gameObject.GetComponent<TareaCables>();
        cable = GetComponent<Cable>();

    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Reiniciar();
        }
    }

    private void OnMouseDrag()
    {
        ActualizarPosicion();
        ActualizarRotacion();
        ActualizarTamano();
        ComprobarConexion();

    }

    private void ActualizarPosicion()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y);
    }
    private void ActualizarRotacion()
    {
        Vector2 posicionActual = transform.position;
        Vector2 puntoOrigen = transform.parent.position;

        Vector2 direccion = posicionActual - puntoOrigen;

        float angulo = Vector2.SignedAngle(Vector2.right * transform.lossyScale, direccion);

        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }
    private void ActualizarTamano()
    {
        Vector2 posicionActual = transform.position;
        Vector2 puntoOrigen = transform.parent.position;

        float distancia = Vector2.Distance(posicionActual, puntoOrigen);

        cabeza.size = new Vector2(distancia, cabeza.size.y);
    }

    private void Reiniciar()
    {
        transform.position = posicionOriginal;
        transform.rotation = Quaternion.identity;
        cabeza.size = new Vector2(tamanoOriginal.x, tamanoOriginal.y);
    }

    private void ComprobarConexion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (Collider2D col in colliders)
        {
            if(col.gameObject != gameObject)
            {
                transform.position = col.transform.position;

                Cable otroCable = col.gameObject.GetComponent<Cable>();

                if(cabeza.color == otroCable.cabeza.color)
                {
                    Conectar();
                    otroCable.Conectar();

                    tareaCables.conexionesActuales++;
                    tareaCables.ComprobarVictoria();
                }
            }
        }
    }
    
    public void Conectar()
    {
        luz.SetActive(true);

        Destroy(this);
        //cable.enabled = false;
        
    }
}
