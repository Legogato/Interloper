using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot_behaviour : MonoBehaviour
{
    #region Public Variables



    public float attackDistance;
    public float actualMoveSpeed;
    public float moveSpeedInRange;
    public float timer;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool attackMode;
    public Transform leftLimit;
    public Transform rigthLimit;
    [HideInInspector] public Transform target;
    public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    public float waitTime;
    
   


    #endregion

    #region  Private Variables


    private Animator anim;
    private float distance;
    private float moveSpeed;
    private bool cooling;
    private float intTimer;
    private bool stillMode;
    private float startWaitTime;





    #endregion

    void Awake() 
    {

        target = rigthLimit;
        intTimer = timer;
        anim = GetComponent<Animator>();
        startWaitTime = waitTime;
    }
   
    void Update()
    {
        if (!attackMode && !stillMode)
        {
            Move();
        }
        //Si el enemigo (no esta en el limite && no esta en el rango del player && no esta activa la animacion de "EnemyRobot_Attack")
        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRobot_Attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyRobotLogic();
            moveSpeed = moveSpeedInRange;
        }
        else
        {
            StopAttack();
            moveSpeed = actualMoveSpeed;
        }      
    }    
    void EnemyRobotLogic() {

        distance = Vector2.Distance(transform.position, target.position);
        
        if (distance > attackDistance)
        {
            StopAttack();         
        }
        else if (attackDistance >= distance && !cooling)
        {
            Attack();

        }
        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }     
    }
        
    void Move() {
        anim.SetBool("canWalk", true);
        
        if (!anim.GetAnimatorTransitionInfo(0).IsName("EnemyRobot_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);          
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);  
    }
     void LaserSound()
    {
        FindObjectOfType<AudioController>().PlayAudio(AudioType.SFX_LASER);
    }
    public void Attack()
    {
        timer = intTimer;
        attackMode = true;
        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Still()
    {
        stillMode = true;
        anim.SetBool("canWalk", false);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }     
    }
    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    //Funcion que retorna un bool si el enemigo se encuentra en los limites condinados
    private bool InsideOfLimits()
    {
        //lo que verifica si el enemy se encontra dentro de los puntos de limites
        return transform.position.x > leftLimit.position.x && transform.position.x < rigthLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);//guarda el calulo  la distancia  del enemigo  desde el limite izquierdo 
        float distanceToRigth = Vector2.Distance(transform.position, rigthLimit.position);//guarda el calcuo de la distancia del enemigo desde el limite derecho
     
        if (distanceToLeft > distanceToRigth)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {            
                target = leftLimit;
                waitTime = startWaitTime;
            }
            Flip();
        }
        else
        {       
            waitTime -= Time.deltaTime;
            
            if (waitTime <= 0)
            {
                
                target = rigthLimit;
                waitTime = startWaitTime;
                Flip();
            }
        }
        Still();
        stillMode = false;
    }
    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }
        transform.eulerAngles = rotation;
    }
}
