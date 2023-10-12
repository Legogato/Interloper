using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLaser : MonoBehaviour
{

    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;

    public float distance;
    public float direction = 1f; // initial direction
    public float speed = 20f; // speed of rotation
    public float anguloLimiteDerecho;
    public float anguloLimiteIzquierdo;
    public Transform player;
    public GameObject laser;
    public float timer;
    public float intTimer;
    public bool cooldown;
    public bool attackMode;
    public bool activeTurret = true;

    private void Start()
    {
        intTimer = timer;
    }


    void Update()
    {
        if(!activeTurret){
            return;
        }
        EnemyLogic();
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up, distance);
        RaycastHit2D hitInfo2 = Physics2D.Raycast(firePoint2.position, firePoint2.up, distance);
        RaycastHit2D hitInfo3 = Physics2D.Raycast(firePoint3.position, firePoint3.up, distance);
        if (hitInfo.collider != null)
        {
            Debug.DrawLine(firePoint.position, hitInfo.point, Color.red);
            if(hitInfo.collider.CompareTag("Player"))
            {
                attackMode = true;   
            }
        }
        else
        {
            Debug.DrawLine(firePoint.position, firePoint.position + firePoint.up * distance, Color.green);
        }
        if (hitInfo2.collider != null)
        {
            Debug.DrawLine(firePoint2.position, hitInfo2.point, Color.red);
            if (hitInfo2.collider.CompareTag("Player"))
            {
                attackMode = true;
                
            }

        }
        else
        {
            Debug.DrawLine(firePoint2.position, firePoint2.position + firePoint2.up * distance, Color.green);
        }
        if (hitInfo3.collider != null)
        {
            Debug.DrawLine(firePoint3.position, hitInfo3.point, Color.red);
            if (hitInfo3.collider.CompareTag("Player"))
            {
                attackMode = true;
            }
        }
        else
        {
            Debug.DrawLine(firePoint3.position, firePoint3.position + firePoint3.up * distance, Color.green);
        }     
    }
    public void EnemyLogic()
    {
        if (!attackMode)
        {
            Move();

        }else if (attackMode)
        {
            RotateTowards(player.position);
            WaitTime();
            if (!cooldown)
            {
               Shoot();
            }
        }
    }
    public void Shoot()
    {
        attackMode = true;
        laser.SetActive(true);
    }
    private void RotateTowards(Vector2 target)
    {
        var offset = 90f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

    }
    void WaitTime()
    {
        timer -= Time.deltaTime;
        cooldown = true;
        if (timer <= 0)
        {
            timer = intTimer;
            cooldown = false;
        }
    }
    private void Move()
    {
        attackMode = false;
        float angle = transform.eulerAngles.z;
        if (angle >= 180f) angle -= 360f;

        if ((angle <= -anguloLimiteIzquierdo) || (angle >= anguloLimiteDerecho)) direction *= -1f; // reverse direction (toggles between 1 & -1)

        transform.Rotate(0, 0, speed * direction * Time.deltaTime);
    }
    public void DisableTurret() {
        {
            activeTurret = false;
            Debug.Log("disabled");
        }
    }
}
