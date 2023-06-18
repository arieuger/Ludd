using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private float damagePoints;
    [SerializeField] private LayerMask enemyLayers;

    private Animator animator;
    private Conscience conscience;
    private bool damageToken = false;

    void Start() {
        animator = GetComponent<Animator>();
        conscience = GetComponent<Conscience>();
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            bool conscienceAllowsMovement = Random.value > conscience.currentConscience / 10;
            if (conscienceAllowsMovement)
                animator.SetTrigger("attack");
            else
                Debug.Log("Ataque anulado");
        }
    }

    private void AttackTriggered() {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies) {
            enemy.transform.GetComponent<Health>().TakeDamage(damagePoints);
            conscience.AddConscience(1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy") && !damageToken) {
            GetComponent<Health>().TakeDamage(1f);
            damageToken = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy") && damageToken) {
            damageToken = false;
        }
    }
}
