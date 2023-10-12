using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Cantidad de fuerza añadida cuando el jugador salta.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Cantidad de velocidad maxima aplicada al movimiento agachado. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	//  Cuanto suavizar el movimiento
	[SerializeField] private bool m_AirControl = false;							// Si un jugador o no se puede controlar al saltar
	[SerializeField] private LayerMask m_WhatIsGround;							// Una mascara determinando que es un suelo para el jugador
	[SerializeField] private Transform m_GroundCheck;							// Una posicion marcando donde chequear si el jugador esta en el suelo
	[SerializeField] private Transform m_CeilingCheck;							// Una posicion marcando donde buscar techos
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// Un colisionador que sera desactivado cuando se este agachando

	const float k_GroundedRadius = .1f; //Radio del circulo de la sobreposicion para determinar si esta en el suelo
	private bool m_Grounded;            // Si el jugador o no esta en el suelo.
	const float k_CeilingRadius = .2f; //  Radio del circulo de sobreposicion para determinar si el jugador se puede parar.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // Para determinar hacia donde esta mirando el jugador
	private Vector3 m_Velocity = Vector3.zero;
	private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;
	public Animator animator;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{

		if (!m_Grounded)
		{
			sw.Start();
		}

		m_Grounded = false;


		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				//sw.Stop();
				m_Grounded = true;
				if (sw.ElapsedMilliseconds > (long)60)
					OnLandEvent.Invoke();
				sw.Reset();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		// Si esta agachado, comprobar que el personaje se puede parar
		if (!crouch)
		{
			// Si el jugador tiene un techo preeviniendo que se pare, mantenerlo agachado
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				if (Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround))
				{
					crouch = true;
					jump = false;
					animator.SetBool("IsJumping", false);
					animator.SetBool("IsCrouching", true);
				}
			}
		}

		//Solo controlar el jugador si grounded o airControl estan activados
		if (m_Grounded || m_AirControl)
		{

			// Si esta agachado
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reducir la velocidad por el multiplicador crouchSpeed
				move *= m_CrouchSpeed;

				// Desactivar uno de los colliders cuando se esta agachando
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} 
			else
			{
				// Activar el collider cuando no se está agachando
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;				
				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);					
				}
			}

			// Mover el personaje obteniendo la variable target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// Y luego suavizandolo y aplicandolo al personaje
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// Si el input se esta moviendo al jugador a la derecha y este esta mirando a la izquierda
			if (move > 0 && !m_FacingRight)
			{
				// ... girar al jugador.
				Flip();
			}
			// Si se esta moviendo al jugador hacia la izquierda y el jugador esta mirando a la derecha
			else if (move < 0 && m_FacingRight)
			{
				// ... girar al jugador.
				Flip();
			}
		}
		// Si el jugador salta...
		if (m_Grounded && jump)
		{
			// Añadir una fuerza vertical al jugador.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		// Cambiar la orientacion en que un jugador esta declarado como mirando.
		m_FacingRight = !m_FacingRight;

		// Multiplicar la escala local x del jugador por -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
