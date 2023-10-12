using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatosVolumen : MonoBehaviour
{
    [SerializeField]
    private float volumenMusica = 1;
    [SerializeField]
    private float volumenEfectos = 1;
    public GameObject sliderMusica;
    public GameObject sliderEfectos;


    private void Start()
    {
        CargarDatos();
    }
    public void GuardarDatos()
    {
        PlayerPrefs.SetFloat("Musica", volumenMusica);
        PlayerPrefs.SetFloat("Efectos", volumenEfectos);

    }
    public void CargarDatos()
    {

        volumenMusica = PlayerPrefs.GetFloat("Musica");
        sliderMusica.GetComponent<Slider>().value = volumenMusica;

        volumenEfectos = PlayerPrefs.GetFloat("Efectos");
        sliderEfectos.GetComponent<Slider>().value = volumenEfectos;
       
        
    }
    public void OnValueChanged()
    {
        volumenMusica = sliderMusica.GetComponent<Slider>().value;
        volumenEfectos = sliderEfectos.GetComponent<Slider>().value;
    }




}
