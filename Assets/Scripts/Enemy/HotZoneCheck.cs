using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private EnemyRobot_behaviour enemyParent;
    private bool inRange;
    private Animator animator;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyRobot_behaviour>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyRobot_Attack"))
        {
            enemyParent.Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
        }
    }













}
