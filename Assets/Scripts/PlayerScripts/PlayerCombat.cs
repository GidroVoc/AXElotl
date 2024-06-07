using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject attackSys;

    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && cooldownTimer > attackCooldown) 
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    void Attack()
    {
        anim.Play("Attack");
        cooldownTimer = 0;
    } 
    
    void AnimAttack()
    {
        attackSys.transform.position = firePoint.position;
        attackSys.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
