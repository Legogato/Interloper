using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;


public class Chat : MonoBehaviour
{
    public UnityEvent intertactAction;
    public KeyCode interactKey;
    public GameObject escena1;
    public GameObject escena2;
    public GameObject escena3;
    public GameObject escena4;
    public GameObject escena5;
    public TextMeshProUGUI dialogue;


    private void Start(){
        StartCoroutine (cambiarEscena());
    }
    private void Update()
    {
    }
    IEnumerator cambiarEscena(){
        yield return new WaitForSeconds(10);
        escena1.SetActive(false);
        escena3.SetActive(true);
        dialogue.text = "You are one of the last artificial humans.";
        yield return new WaitForSeconds(10);
        escena3.SetActive(false);
        escena2.SetActive(true);
        dialogue.text = "Through artificial intelligence, something terrible took over.";
        yield return new WaitForSeconds(10);
        escena2.SetActive(false);
        escena4.SetActive(true);
        dialogue.text = "You are on a mission to turn off the supercomputer that started everything.";
        yield return new WaitForSeconds(10);
        escena4.SetActive(false);
        escena5.SetActive(true);
        dialogue.text = "Wake up now. Initializing program, replicant body materialized.";
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("CinematicaPrologo");

    }
}
