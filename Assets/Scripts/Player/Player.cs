using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour

{
    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0f; 
    bool jump = false;
    bool crouch = false;
    public float runSpeed = 40f;
    private bool death = false;
    public GameObject deathScene;
    public GameObject particulasMuerte;
    public AudioSource audioData;

    // Este metodo es llamado una vez por cada frame, obtiene el la entrada del jugador de una forma cresciente/decresciente y controla los saltos
    void Update()
    {
        Move();
        
        if (Input.GetButtonDown("Jump"))
        {

                animator.SetBool("IsJumping", true);
                jump = true;
        }
      
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (death)
        {
            Death();
            
        }
    }
    //Metodo llamado por el GameObject Player, coordina el animador con el aterrizaje del jugador.
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    //Metodo que obtiene los datos obtenidos por el teclado y los traduce a la velocidad del jugador de acuerdo a la velocidad establecida en el inspector,
    private void Move()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    //Metodo que controla la animacion de agacharse.
    public void OnCrouching(bool isCrouching)
    {

        animator.SetBool("IsCrouching", isCrouching);
    }
    //Control general del estado del movimiento del jugador.
    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
       
    }

    public void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Daño"))
        {
            death = true;
        }
    }

    public void Death()
    {
        
        audioData.Play();
        deathScene.SetActive(true);
        gameObject.SetActive(false);
        //FindObjectOfType<AudioController>().PlayAudio(AudioType.SFX_DEATH);
        Instantiate(particulasMuerte, transform.position,Quaternion.identity);

    }


}

